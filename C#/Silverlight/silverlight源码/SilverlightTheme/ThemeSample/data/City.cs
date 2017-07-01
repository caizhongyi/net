using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ThemeSample
{
    public partial class City
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the population of the city.
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// Initializes a new instance of the City class.
        /// </summary>
        public City()
        {
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets a collection of cities in the Puget Sound area.
        /// </summary>
        public static List<City> PugetSound
        {
            get
            {
                List<City> pugetSound = new List<City>();
                pugetSound.Add(new City { Name = "Bellevue", Population = 112344 });
                pugetSound.Add(new City { Name = "Issaquah", Population = 11212 });
                pugetSound.Add(new City { Name = "Redmond", Population = 46391 });
                pugetSound.Add(new City { Name = "Seattle", Population = 592800 });
                return pugetSound;
            }
        }
    }
}
