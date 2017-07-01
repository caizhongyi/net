using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace FromDemo
{
    public partial class Page : UserControl
    {
        TransFormAction tfa = new TransFormAction();
        CanvasActionInGrid ca = new CanvasActionInGrid();
        GridOperation go = new GridOperation();
        MouseDrag md = new MouseDrag();

        StackPanel sp1;
        StackPanel sp2;
        StackPanel sp3;
        TextBlock txtblock1;
        TextBlock txtblock2;
        TextBlock txtblock3;
        TextBox txt1;
        TextBox txt2;
        TextBox txt3;
        TranslateTransform tr1;
        TranslateTransform tr2;
        TranslateTransform tr3;
        ListBoxItem listBoxItem1; 
        ListBoxItem listBoxItem2;
        ListBoxItem listBoxItem3;
        ListBoxItem listBoxItem4;
        Button btn;
        public Page()
        {
            InitializeComponent();
            list_canvas.Width = 250;
            Canvas.SetLeft(save_canvas, Canvas.GetLeft(list_canvas) +list_canvas .Width );
            //MessageBox.Show((Canvas.GetLeft(list_canvas) + list_canvas.Width).ToString());
            sp1 = new StackPanel();
            txtblock1 = new TextBlock();  
            txt1 = new TextBox();

            sp2 = new StackPanel();
            txtblock2 = new TextBlock();
            txt2 = new TextBox();

            sp3 = new StackPanel();
            txtblock3 = new TextBlock();
            txt3 = new TextBox();

            txtblock1.Text = "姓名:";
            txtblock2.Text = "性别:";
            txtblock3.Text = "年龄:";

   

            SetListBoxItem(sp1,txtblock1,txt1);
            SetListBoxItem(sp2, txtblock2, txt2);
            SetListBoxItem(sp3, txtblock3, txt3);

     
            sp1.MouseLeftButtonDown+=new MouseButtonEventHandler(sp1_MouseLeftButtonDown);
            sp1.MouseLeftButtonUp +=new MouseButtonEventHandler(sp1_MouseLeftButtonUp);
            sp1.MouseMove+= new MouseEventHandler(sp1_MouseMove);

            sp2.MouseLeftButtonDown += new MouseButtonEventHandler(sp2_MouseLeftButtonDown);
            sp2.MouseLeftButtonUp += new MouseButtonEventHandler(sp2_MouseLeftButtonUp);
            sp2.MouseMove += new MouseEventHandler(sp2_MouseMove);

            sp3.MouseLeftButtonDown += new MouseButtonEventHandler(sp3_MouseLeftButtonDown);
            sp3.MouseLeftButtonUp += new MouseButtonEventHandler(sp3_MouseLeftButtonUp);
            sp3.MouseMove += new MouseEventHandler(sp3_MouseMove);

            tr1 = new TranslateTransform();
            tr2 = new TranslateTransform();
            tr3 = new TranslateTransform();

            sp1.RenderTransform = tr1;
            sp2.RenderTransform = tr2;
            sp3.RenderTransform = tr3;


            btn = new Button();
            btn.Content = "Save";
            btn.Click += new RoutedEventHandler(Button_Click);

            //listBoxItem1 = new ListBoxItem();
            //listBoxItem2 = new ListBoxItem();
            //listBoxItem3 = new ListBoxItem();
            //listBoxItem4 = new ListBoxItem();

            //listBoxItem1.Content = sp1;
            //listBoxItem2.Content = sp2;
            //listBoxItem3.Content = sp3;
            //listBoxItem4.Content = btn;

            input_list.Items.Add(sp1);
            input_list.Items.Add(sp2);
            input_list.Items.Add(sp3);
            input_list.Items.Add(btn);

           

        }
   

        private void SetListBoxItem(StackPanel sp,TextBlock txtblock,TextBox txt)
        {
            
            txt.Width = 200;
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(txtblock);
            sp.Children.Add(txt);
            

        }

        bool flag = true ;
        StoryBoardOpeartion sp = new StoryBoardOpeartion();
        private void changesize_btn_Click(object sender, RoutedEventArgs e)
        {
          
          
            //win.Alert(op.Width.ToString ());
            if (flag)
            {
                System.Windows.Browser.HtmlPopupWindowOptions op = new System.Windows.Browser.HtmlPopupWindowOptions();

                LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
                Storyboard sb = new Storyboard();
                //sb = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                //tfa.SetScaleTransformCenterX(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 1);
                ////sb.Begin();
                Storyboard sb1 = new Storyboard();

                sb1 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                tfa.SetScaleTransformX(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 3);
                sb1.Begin();

                Storyboard sb2 = new Storyboard();
                sb2 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                tfa.SetScaleTransformY(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 3);
                sb2.Begin();

                input_list.Width = list_canvas.Width;
                input_list.Height = list_canvas.Height;

                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(op.Width);
                LayoutRoot.ColumnDefinitions.Add(col);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(200);
                LayoutRoot.ColumnDefinitions.Add(col1);

                //Storyboard sb3 = new Storyboard();
                //sb3 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                ////ca.SetGridColumn(sp.linearDoubleKeyFrame, save_canvas, sp.doubleKeyFrames, 1);
                //MessageBox.Show((Canvas.GetLeft(list_canvas) + list_canvas.Width).ToString ());
                Canvas.SetLeft(save_canvas, Canvas.GetLeft(list_canvas) + list_canvas.Width*3);
                //Grid.SetColumn(save_canvas, 1);
                flag = false;
                //System.Windows.Browser.HtmlPage.Window.Alert(op.Height.ToString());
            }
            else
            {
                System.Windows.Browser.HtmlPopupWindowOptions op = new System.Windows.Browser.HtmlPopupWindowOptions();

                LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
                Storyboard sbb = new Storyboard();
                //sb = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                //tfa.SetScaleTransformCenterX(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 1);
                ////sb.Begin();
                Storyboard sbb1 = new Storyboard();

                sbb1 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                tfa.SetScaleTransformX(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 1);
                sbb1.Begin();

                Storyboard sbb2 = new Storyboard();
                sbb2 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                tfa.SetScaleTransformY(sp.linearDoubleKeyFrame, list_stf, sp.doubleKeyFrames, 1);
                sbb2.Begin();

                input_list.Width = list_canvas.Width;
                input_list.Height = list_canvas.Height;

                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(op.Width);
                LayoutRoot.ColumnDefinitions.Add(col);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(200);
                LayoutRoot.ColumnDefinitions.Add(col1);

                Canvas.SetLeft(save_canvas, Canvas.GetLeft(list_canvas) + list_canvas.Width);
                flag = true;
                //Storyboard sbb3 = new Storyboard();
                //sbb3 = sp.CreateDoubleStoryBoard(0, 0, 1, 4);
                //ca.SetGridColumn(sp.linearDoubleKeyFrame, save_canvas, sp.doubleKeyFrames, 1);
            }
            


        }
        //Point point = new Point();
        bool onMouseUp=false;//鼠标是否放开
        StackPanel nowsp = new StackPanel();//拖动项的stackPanel
        StackPanel oldsp = new StackPanel();//要换的stackPanel
        ListBoxItem item = new ListBoxItem();
        int Index;//拖动项的Index
        //int toIndex;
        

        private void insertItem(int item)
        {
            if (onMouseUp)
            {
                
                //item1.Content = sp2;
                onMouseUp = false;
            }
        }
        private bool isPoint(TranslateTransform tr,TranslateTransform tsp,StackPanel sp)
        {
            if (tr.X >= tsp.X && tr.X <= tsp.X + sp.Width && tr.Y >= tsp.Y && tr.Y <= tsp.Y + sp.Height)
            { return true; }
            else
            { return false ;}
        }
        private void sp1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            md.MouseLeftButtonDown(sender, e);
            md.img = tr1; 

            //nowsp = sp1;
        }

        private void sp1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {   
            md.MouseLeftButtonUp(sender, e);
            onMouseUp = true;
           
        }

        private void sp1_MouseMove(object sender, MouseEventArgs e)
        {
            md.MouseMove(sender, e);
            Index=input_list.Items.IndexOf(sp1);
            if (onMouseUp)
            {
                input_list.Items.Remove(sp1);
                input_list.Items.Insert(Index, sp1);
                MessageBox.Show(Index.ToString());
                onMouseUp = false;
            }
            //oldsp = sp1;
            //item = listBoxItem1;
        }


        private void sp2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            md.MouseLeftButtonDown(sender, e);
            md.img = tr2;

            //nowsp = sp2;
        }

        private void sp2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            md.MouseLeftButtonUp(sender, e);
            onMouseUp = true;
            //listBoxItem2.Content = oldsp;
            //item.Content = nowsp;
        }

        private void sp2_MouseMove(object sender, MouseEventArgs e)
        { 
            md.MouseMove(sender, e);
            Index=input_list.Items.IndexOf(sp2);
            if (onMouseUp)
            {
                input_list.Items.Remove(sp2);
                input_list.Items.Insert(Index, sp2);
                MessageBox.Show(Index.ToString());
                onMouseUp = false;
            }
            //oldsp = sp2;
            //item = listBoxItem2;
         
        }

        private void sp3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
              
            md.MouseLeftButtonDown(sender, e);

            //md.img = tr3;
            //nowsp = sp3;
        }

        private void sp3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            md.MouseLeftButtonUp(sender, e);
            input_list.Items.Remove(sp3);
            input_list.Items.Insert(Index, sp3);
            //listBoxItem3.Content = oldsp;
            //item.Content = nowsp;
        }

        private void sp3_MouseMove(object sender, MouseEventArgs e)
        {
            md.MouseMove(sender, e);
            Index = input_list .Items .IndexOf(sp3);
            //oldsp = sp3;
            //item = listBoxItem3;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            StackPanel sp=new StackPanel ();
            TextBlock bName=new TextBlock();
            TextBlock bSex=new TextBlock();
            TextBlock bAge=new TextBlock();
            bName.Text ="姓名: "+txt1 .Text;
            bSex.Text = "姓别: " + txt2.Text;
            bAge.Text = "年龄: " + txt3.Text ;
            sp.Children .Add (bName);
            sp.Children .Add (bSex);
            sp.Children .Add (bAge);
            sp.Orientation=Orientation .Horizontal;
            save_list.Items.Add(sp);
        }

       
     
    }
}
