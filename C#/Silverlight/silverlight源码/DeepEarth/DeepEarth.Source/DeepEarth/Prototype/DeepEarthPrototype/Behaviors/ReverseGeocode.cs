using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DeepEarth;
using DeepEarth.Provider.VirtualEarth;
using DeepEarth.Provider.VirtualEarth.VEGeocodeService;

namespace DeepEarthPrototype.Behaviors
{
    public class ReverseGeocode 
    {
        Map _Map;

        public ReverseGeocode(Map map, Geocode geocodeService) 
        {
            GeocodeService = geocodeService;
            _Map = map;
            map.Events.MapMouseDown += this.MouseDown;
        }

        public Geocode GeocodeService { get; set; }

        public void MouseDown(Map map, MouseButtonEventArgs args)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt)
            {
                args.Handled = true;

                Point location = map.CoordHelper.PixelToGeo(args.GetPosition(map));
                GeocodeService.Find(location, (o, ev) =>
                                                  {
                                                      List<GeocodeResult> results = ((GeocodeResultArgs) ev).Results;
                                                      if (results.Count > 0)
                                                      {
                                                          MessageBox.Show(results[0].DisplayName);
                                                      }
                                                  });
            }
        }
    }
}