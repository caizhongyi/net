﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Microsoft.Windows.Controls.Samples
{
    /// <summary>
    /// Utility to to load shared resources into another ResourceDictionary.
    /// </summary>
    public static class SharedResources
    {
        /// <summary>
        /// Name of the embedded resource containing the shared resources.
        /// </summary>
        private const string ResourceName = "Microsoft.Windows.Controls.Samples.SharedResources.xaml";

        /// <summary>
        /// Prefix of images loaded as resources.
        /// </summary>
        private const string ResourceImagePrefix = "Microsoft.Windows.Controls.Samples.Images.";

        /// <summary>
        /// Prefix of icons loaded as resources.
        /// </summary>
        private const string ResourceIconPrefix = "Microsoft.Windows.Controls.Samples.Icons.";

        /// <summary>
        /// Merge a ResourceDictionary with the shared resources.
        /// </summary>
        /// <param name="resources">ResourceDictionary to populate.</param>
        public static void Merge(ResourceDictionary resources)
        {
            // Get the embedded resource
            string xaml = null;
            Assembly assembly = typeof(SharedResources).Assembly;
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(ResourceName)))
            {
                xaml = reader.ReadToEnd();
            }

            // Process the resources using XLinq (because ResourceDictionary
            // cannot be enumerated, surprisingly)
            XDocument sharedResources = XDocument.Parse(xaml);
            foreach (XElement child in sharedResources.Root.Elements())
            {
                // Get the Key attribute (and then remove it)
                string key = null;
                foreach (XAttribute keyAttribute in 
                    from a in child.Attributes()
                    where string.CompareOrdinal(a.Name.LocalName, "Key") == 0
                    select a)
                {
                    key = keyAttribute.Value;
                    if (!string.IsNullOrEmpty(key))
                    {
                        keyAttribute.Remove();
                        break;
                    }
                }

                // Load the resource
                object value = null;
                try
                {
                    value = XamlReader.Load(child.ToString(SaveOptions.DisableFormatting));
                }
                catch (XamlParseException)
                {
                    continue;
                }

                // Merge the resource into the other dictionary
                if (resources.Contains(key))
                {
                    resources.Remove(key);
                }
                resources.Add(key, value);
            }
        }

        /// <summary>
        /// Get an embedded resource image from the assembly.
        /// </summary>
        /// <param name="name">Name of the image resource.</param>
        /// <returns>
        /// Desired embedded resource image from the assembly.
        /// </returns>
        public static Image GetImage(string name)
        {
            return CreateImage(ResourceImagePrefix, name);
        }

        /// <summary>
        /// Get an embedded resource icon from the assembly.
        /// </summary>
        /// <param name="name">Name of the icon resource.</param>
        /// <returns>
        /// Desired embedded resource icon from the assembly.
        /// </returns>
        public static Image GetIcon(string name)
        {
            return CreateImage(ResourceIconPrefix, name);
        }

        /// <summary>
        /// Get an embedded resource image from the assembly.
        /// </summary>
        /// <param name="prefix">The prefix of the full name of the resource.</param>
        /// <param name="name">Name of the image resource.</param>
        /// <returns>
        /// Desired embedded resource image from the assembly.
        /// </returns>
        public static Image CreateImage(string prefix, string name)
        {
            Image image = new Image { Tag = name };

            Assembly assembly = typeof(SharedResources).Assembly;
            string resourceName = prefix + name;
            using (Stream resource = assembly.GetManifestResourceStream(resourceName))
            {
                if (resource != null)
                {
                    BitmapImage source = new BitmapImage();
                    source.SetSource(resource);
                    image.Source = source;
                }
            }

            return image;
        }

        /// <summary>
        /// Get all of the names of embedded resources images in the assembly.
        /// </summary>
        /// <returns>
        /// All of the names of embedded resources images in the assembly.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Does more work than a property should.")]
        public static IEnumerable<string> GetImageNames()
        {
            return GetResourceNames(ResourceImagePrefix);
        }

        /// <summary>
        /// Get all of the names of embedded resources icons in the assembly.
        /// </summary>
        /// <returns>
        /// All of the names of embedded resources icons in the assembly.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Does more work than a property should.")]
        public static IEnumerable<string> GetIconNames()
        {
            return GetResourceNames(ResourceIconPrefix);
        }

        /// <summary>
        /// Get all of the images in the assembly.
        /// </summary>
        /// <returns>All of the images in the assembly.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Does more work than a property should")]
        public static IEnumerable<Image> GetImages()
        {
            foreach (string name in GetImageNames())
            {
                yield return GetImage(name);
            }
        }

        /// <summary>
        /// Get all of the icons in the assembly.
        /// </summary>
        /// <returns>All of the icons in the assembly.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Does more work than a property should")]
        public static IEnumerable<Image> GetIcons()
        {
            foreach (string name in GetIconNames())
            {
                yield return GetImage(name);
            }
        }

        /// <summary>
        /// Get all the names of embedded resources in the assembly with the 
        /// provided prefix value.
        /// </summary>
        /// <param name="prefix">The prefix for the full resource name.</param>
        /// <returns>Returns an enumerable of all the resource names that match.</returns>
        private static IEnumerable<string> GetResourceNames(string prefix)
        {
            Assembly assembly = typeof(SharedResources).Assembly;
            foreach (string name in assembly.GetManifestResourceNames())
            {
                // Ignore resources that don't share the images prefix
                if (!name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Trim the prefix off of the name
                yield return name.Substring(prefix.Length, name.Length - prefix.Length);
            }
        }
    }
}