using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ReflectionBehaviour
{
    public class ReflectionShader : ShaderEffect
    {
        public static DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input",
                                                                                            typeof(ReflectionShader),
                                                                                            0);
        public static readonly DependencyProperty ElementHeightProperty =
            DependencyProperty.Register("ElementHeight",
                                        typeof (double), typeof (ReflectionShader),
                                        new PropertyMetadata(100.0, OnElementHeightChanged));

        public ReflectionShader()
        {
            var u = new Uri(@"ReflectionBehaviour;component/Reflection.ps", UriKind.Relative);
            PixelShader = new PixelShader
                              {
                                  UriSource = u
                              };
            // Must initialize each DependencyProperty that's affliated with a shader register
            // Ensures the shader initializes to the proper default value.
            UpdateShaderValue(InputProperty);
        }


        public virtual Brush Input
        {
            get
            {
                return ((Brush)(GetValue(InputProperty)));
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }

        public double ElementHeight
        {
            get { return (double) base.GetValue(ElementHeightProperty); }
            set { base.SetValue(ElementHeightProperty, value); }
        }

        public static void OnElementHeightChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            (d as ReflectionShader).OnElementHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        public double PaddingBottomProp
        {
            get
            {
                return PaddingBottom;
            }
            set
            {
                PaddingBottom = value;
            }
        }

        public virtual void OnElementHeightChanged(double oldValue, double newValue)
        {
            PaddingBottom = newValue;
        }
    }
}