using System.Windows;

namespace DeepEarthPrototype.Controls
{
    public interface IInfoBoxShape
    {
        object InfoBoxContent { get; set; }

        Point? InfoBoxOffset { get; set; }
    }
}