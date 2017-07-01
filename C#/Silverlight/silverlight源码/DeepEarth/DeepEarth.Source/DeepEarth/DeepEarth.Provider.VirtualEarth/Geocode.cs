/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using DeepEarth.Provider.VirtualEarth.VEGeocodeService;

namespace DeepEarth.Provider.VirtualEarth
{
    public class Geocode
    {
        private string token;

        public Geocode()
        {
            InitVeService();
        }

        public bool IsInitialized { get; set; }

        public void Find(string location, EventHandler onResults)
        {
            if (IsInitialized)
            {
                var geocodeRequest = new GeocodeRequest
                                         {
                                             Credentials = new Credentials {Token = token},
                                             Query = location
                                         };

                var geocodeService = new GeocodeServiceClient();
                geocodeService.GeocodeCompleted += (o, e) =>
                                                       {
                                                           GeocodeResponse geocodeResponse = e.Result;
                                                           onResults(this, new GeocodeResultArgs {Results = geocodeResponse.Results});
                                                       };
                geocodeService.GeocodeAsync(geocodeRequest);
            }
        }

        public void Find(Point location, EventHandler onResults)
        {
            if (IsInitialized)
            {
                var reverseGeocodeRequest = new ReverseGeocodeRequest
                                                {
                                                    Credentials = new Credentials {Token = token},
                                                    Location = new Location {Longitude = location.X, Latitude = location.Y}
                                                };

                var geocodeService = new GeocodeServiceClient();

                geocodeService.ReverseGeocodeCompleted += (o, e) =>
                                                              {
                                                                  GeocodeResponse geocodeResponse = e.Result;
                                                                  onResults(this, new GeocodeResultArgs {Results = geocodeResponse.Results});
                                                              };
                geocodeService.ReverseGeocodeAsync(reverseGeocodeRequest);
            }
        }

        public event EventHandler InitializeCompleted;


        protected virtual void onInitialized()
        {
            IsInitialized = true;
            if (InitializeCompleted != null) InitializeCompleted(this, null);
        }

        private void InitVeService()
        {
            // Test if isDesignTime to display in Blend
            if (HtmlPage.IsEnabled)
            {
                //get token
                Token.GetToken((o, e) =>
                {
                    token = ((TokenResultArgs)e).Result;
                    onInitialized();
                });
            }
        }
    }

    public class GeocodeResultArgs : EventArgs
    {
        public List<GeocodeResult> Results { get; set; }
    }
}