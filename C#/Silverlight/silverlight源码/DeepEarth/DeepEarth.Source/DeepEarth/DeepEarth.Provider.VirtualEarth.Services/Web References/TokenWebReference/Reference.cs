﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
// 
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using DeepEarth.Provider.VirtualEarth.Services.Properties;

#pragma warning disable 1591

namespace DeepEarth.Provider.VirtualEarth.Services.TokenWebReference
{
    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [WebServiceBinding(Name = "CommonServiceSoap", Namespace = "http://s.mappoint.net/mappoint-30/")]
    public partial class CommonService : SoapHttpClientProtocol
    {
        private SendOrPostCallback GetClientTokenOperationCompleted;
        private SendOrPostCallback GetCountryRegionInfoOperationCompleted;

        private SendOrPostCallback GetDataSourceInfoOperationCompleted;
        private SendOrPostCallback GetEntityTypesOperationCompleted;

        private SendOrPostCallback GetGreatCircleDistancesOperationCompleted;
        private SendOrPostCallback GetVersionInfoOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public CommonService()
        {
            Url = Settings.Default.DeepEarth_Provider_VirtualEarth_Services_TokenWebReference_CommonService;
            if (IsLocalFileSystemWebService(Url))
            {
                UseDefaultCredentials = true;
                useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                useDefaultCredentialsSetExplicitly = true;
            }
        }

        public CustomerInfoHeader CustomerInfoHeaderValue { get; set; }

        public UserInfoHeader UserInfoHeaderValue { get; set; }

        public new string Url
        {
            get { return base.Url; }
            set
            {
                if (((IsLocalFileSystemWebService(base.Url)
                      && (useDefaultCredentialsSetExplicitly == false))
                     && (IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get { return base.UseDefaultCredentials; }
            set
            {
                base.UseDefaultCredentials = value;
                useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event GetVersionInfoCompletedEventHandler GetVersionInfoCompleted;

        /// <remarks/>
        public event GetCountryRegionInfoCompletedEventHandler GetCountryRegionInfoCompleted;

        /// <remarks/>
        public event GetEntityTypesCompletedEventHandler GetEntityTypesCompleted;

        /// <remarks/>
        public event GetDataSourceInfoCompletedEventHandler GetDataSourceInfoCompleted;

        /// <remarks/>
        public event GetGreatCircleDistancesCompletedEventHandler GetGreatCircleDistancesCompleted;

        /// <remarks/>
        public event GetClientTokenCompletedEventHandler GetClientTokenCompleted;

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetVersionInfo",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public VersionInfo[] GetVersionInfo()
        {
            object[] results = Invoke("GetVersionInfo", new object[0]);
            return ((VersionInfo[]) (results[0]));
        }

        /// <remarks/>
        public void GetVersionInfoAsync()
        {
            GetVersionInfoAsync(null);
        }

        /// <remarks/>
        public void GetVersionInfoAsync(object userState)
        {
            if ((GetVersionInfoOperationCompleted == null))
            {
                GetVersionInfoOperationCompleted = OnGetVersionInfoOperationCompleted;
            }
            InvokeAsync("GetVersionInfo", new object[0], GetVersionInfoOperationCompleted, userState);
        }

        private void OnGetVersionInfoOperationCompleted(object arg)
        {
            if ((GetVersionInfoCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetVersionInfoCompleted(this,
                                        new GetVersionInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error,
                                                                             invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetCountryRegionInfo",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public CountryRegionInfo[] GetCountryRegionInfo(int[] entityIDs)
        {
            object[] results = Invoke("GetCountryRegionInfo", new object[]
                                                                  {
                                                                      entityIDs
                                                                  });
            return ((CountryRegionInfo[]) (results[0]));
        }

        /// <remarks/>
        public void GetCountryRegionInfoAsync(int[] entityIDs)
        {
            GetCountryRegionInfoAsync(entityIDs, null);
        }

        /// <remarks/>
        public void GetCountryRegionInfoAsync(int[] entityIDs, object userState)
        {
            if ((GetCountryRegionInfoOperationCompleted == null))
            {
                GetCountryRegionInfoOperationCompleted = OnGetCountryRegionInfoOperationCompleted;
            }
            InvokeAsync("GetCountryRegionInfo", new object[]
                                                    {
                                                        entityIDs
                                                    }, GetCountryRegionInfoOperationCompleted, userState);
        }

        private void OnGetCountryRegionInfoOperationCompleted(object arg)
        {
            if ((GetCountryRegionInfoCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetCountryRegionInfoCompleted(this,
                                              new GetCountryRegionInfoCompletedEventArgs(invokeArgs.Results,
                                                                                         invokeArgs.Error,
                                                                                         invokeArgs.Cancelled,
                                                                                         invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetEntityTypes",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public EntityType[] GetEntityTypes(string dataSourceName)
        {
            object[] results = Invoke("GetEntityTypes", new object[]
                                                            {
                                                                dataSourceName
                                                            });
            return ((EntityType[]) (results[0]));
        }

        /// <remarks/>
        public void GetEntityTypesAsync(string dataSourceName)
        {
            GetEntityTypesAsync(dataSourceName, null);
        }

        /// <remarks/>
        public void GetEntityTypesAsync(string dataSourceName, object userState)
        {
            if ((GetEntityTypesOperationCompleted == null))
            {
                GetEntityTypesOperationCompleted = OnGetEntityTypesOperationCompleted;
            }
            InvokeAsync("GetEntityTypes", new object[]
                                              {
                                                  dataSourceName
                                              }, GetEntityTypesOperationCompleted, userState);
        }

        private void OnGetEntityTypesOperationCompleted(object arg)
        {
            if ((GetEntityTypesCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetEntityTypesCompleted(this,
                                        new GetEntityTypesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error,
                                                                             invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetDataSourceInfo",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public DataSource[] GetDataSourceInfo(string[] dataSourceNames)
        {
            object[] results = Invoke("GetDataSourceInfo", new object[]
                                                               {
                                                                   dataSourceNames
                                                               });
            return ((DataSource[]) (results[0]));
        }

        /// <remarks/>
        public void GetDataSourceInfoAsync(string[] dataSourceNames)
        {
            GetDataSourceInfoAsync(dataSourceNames, null);
        }

        /// <remarks/>
        public void GetDataSourceInfoAsync(string[] dataSourceNames, object userState)
        {
            if ((GetDataSourceInfoOperationCompleted == null))
            {
                GetDataSourceInfoOperationCompleted = OnGetDataSourceInfoOperationCompleted;
            }
            InvokeAsync("GetDataSourceInfo", new object[]
                                                 {
                                                     dataSourceNames
                                                 }, GetDataSourceInfoOperationCompleted, userState);
        }

        private void OnGetDataSourceInfoOperationCompleted(object arg)
        {
            if ((GetDataSourceInfoCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetDataSourceInfoCompleted(this,
                                           new GetDataSourceInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error,
                                                                                   invokeArgs.Cancelled,
                                                                                   invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetGreatCircleDistances",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public double[] GetGreatCircleDistances(LatLong[] latLongs)
        {
            object[] results = Invoke("GetGreatCircleDistances", new object[]
                                                                     {
                                                                         latLongs
                                                                     });
            return ((double[]) (results[0]));
        }

        /// <remarks/>
        public void GetGreatCircleDistancesAsync(LatLong[] latLongs)
        {
            GetGreatCircleDistancesAsync(latLongs, null);
        }

        /// <remarks/>
        public void GetGreatCircleDistancesAsync(LatLong[] latLongs, object userState)
        {
            if ((GetGreatCircleDistancesOperationCompleted == null))
            {
                GetGreatCircleDistancesOperationCompleted = OnGetGreatCircleDistancesOperationCompleted;
            }
            InvokeAsync("GetGreatCircleDistances", new object[]
                                                       {
                                                           latLongs
                                                       }, GetGreatCircleDistancesOperationCompleted, userState);
        }

        private void OnGetGreatCircleDistancesOperationCompleted(object arg)
        {
            if ((GetGreatCircleDistancesCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetGreatCircleDistancesCompleted(this,
                                                 new GetGreatCircleDistancesCompletedEventArgs(invokeArgs.Results,
                                                                                               invokeArgs.Error,
                                                                                               invokeArgs.Cancelled,
                                                                                               invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [SoapHeader("CustomerInfoHeaderValue")]
        [SoapHeader("UserInfoHeaderValue")]
        [SoapDocumentMethod("http://s.mappoint.net/mappoint-30/GetClientToken",
            RequestNamespace = "http://s.mappoint.net/mappoint-30/",
            ResponseNamespace = "http://s.mappoint.net/mappoint-30/", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        public string GetClientToken(TokenSpecification specification)
        {
            object[] results = Invoke("GetClientToken", new object[]
                                                            {
                                                                specification
                                                            });
            return ((string) (results[0]));
        }

        /// <remarks/>
        public void GetClientTokenAsync(TokenSpecification specification)
        {
            GetClientTokenAsync(specification, null);
        }

        /// <remarks/>
        public void GetClientTokenAsync(TokenSpecification specification, object userState)
        {
            if ((GetClientTokenOperationCompleted == null))
            {
                GetClientTokenOperationCompleted = OnGetClientTokenOperationCompleted;
            }
            InvokeAsync("GetClientToken", new object[]
                                              {
                                                  specification
                                              }, GetClientTokenOperationCompleted, userState);
        }

        private void OnGetClientTokenOperationCompleted(object arg)
        {
            if ((GetClientTokenCompleted != null))
            {
                var invokeArgs = ((InvokeCompletedEventArgs) (arg));
                GetClientTokenCompleted(this,
                                        new GetClientTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error,
                                                                             invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private static bool IsLocalFileSystemWebService(string url)
        {
            if ((string.IsNullOrEmpty(url)))
            {
                return false;
            }
            var wsUri = new Uri(url);
            if (((wsUri.Port >= 1024)
                 && (string.Compare(wsUri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    [XmlRoot(Namespace = "http://s.mappoint.net/mappoint-30/", IsNullable = false)]
    public class CustomerInfoHeader : SoapHeader
    {
        /// <remarks/>
        public short CustomLogEntry { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class TokenSpecification
    {
        /// <remarks/>
        public string ClientIPAddress { get; set; }

        /// <remarks/>
        public int TokenValidityDurationMinutes { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class DataSource
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public string Version { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        /// <remarks/>
        public DataSourceCapability Capability { get; set; }

        /// <remarks/>
        public int[] EntityExtent { get; set; }
    }

    /// <remarks/>
    [Flags]
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public enum DataSourceCapability
    {
        /// <remarks/>
        CanDrawMaps = 1,

        /// <remarks/>
        CanFindPlaces = 2,

        /// <remarks/>
        CanFindNearby = 4,

        /// <remarks/>
        CanRoute = 8,

        /// <remarks/>
        CanFindAddress = 16,

        /// <remarks/>
        HasIcons = 32,

        /// <remarks/>
        DataServiceQuery = 64,
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class EntityProperty
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string DataType { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class EntityType
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string ParentName { get; set; }

        /// <remarks/>
        public string Definition { get; set; }

        /// <remarks/>
        [XmlArrayItem("Property")]
        public EntityProperty[] Properties { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class LatLong
    {
        public LatLong()
        {
            Latitude = 0;
            Longitude = 0;
        }

        /// <remarks/>
        [DefaultValue(0)]
        public double Latitude { get; set; }

        /// <remarks/>
        [DefaultValue(0)]
        public double Longitude { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class CountryRegionInfo
    {
        /// <remarks/>
        public int EntityID { get; set; }

        /// <remarks/>
        public LatLong LatLong { get; set; }

        /// <remarks/>
        public string Iso2 { get; set; }

        /// <remarks/>
        public string Iso3 { get; set; }

        /// <remarks/>
        public string FriendlyName { get; set; }

        /// <remarks/>
        public string OfficialName { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class VersionInfo
    {
        /// <remarks/>
        public string Component { get; set; }

        /// <remarks/>
        public string Version { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class CountryRegionContext
    {
        public CountryRegionContext()
        {
            EntityID = 0;
        }

        /// <remarks/>
        [DefaultValue(0)]
        public int EntityID { get; set; }

        /// <remarks/>
        public string Iso2 { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public class CultureInfo
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public int Lcid { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    [XmlRoot(Namespace = "http://s.mappoint.net/mappoint-30/", IsNullable = false)]
    public class UserInfoHeader : SoapHeader
    {
        /// <remarks/>
        public CultureInfo Culture { get; set; }

        /// <remarks/>
        public DistanceUnit DefaultDistanceUnit { get; set; }

        /// <remarks/>
        public CountryRegionContext Context { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("System.Xml", "2.0.50727.3053")]
    [Serializable]
    [XmlType(Namespace = "http://s.mappoint.net/mappoint-30/")]
    public enum DistanceUnit
    {
        /// <remarks/>
        Kilometer,

        /// <remarks/>
        Mile,
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetVersionInfoCompletedEventHandler(object sender, GetVersionInfoCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetVersionInfoCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetVersionInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                  object userState) :
                                                      base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public VersionInfo[] Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((VersionInfo[]) (results[0]));
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetCountryRegionInfoCompletedEventHandler(
        object sender, GetCountryRegionInfoCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetCountryRegionInfoCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetCountryRegionInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                        object userState) :
                                                            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CountryRegionInfo[] Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((CountryRegionInfo[]) (results[0]));
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetEntityTypesCompletedEventHandler(object sender, GetEntityTypesCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetEntityTypesCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetEntityTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                  object userState) :
                                                      base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public EntityType[] Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((EntityType[]) (results[0]));
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetDataSourceInfoCompletedEventHandler(object sender, GetDataSourceInfoCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetDataSourceInfoCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetDataSourceInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                     object userState) :
                                                         base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DataSource[] Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((DataSource[]) (results[0]));
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetGreatCircleDistancesCompletedEventHandler(
        object sender, GetGreatCircleDistancesCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetGreatCircleDistancesCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetGreatCircleDistancesCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                           object userState) :
                                                               base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public double[] Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((double[]) (results[0]));
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetClientTokenCompletedEventHandler(object sender, GetClientTokenCompletedEventArgs e);

    /// <remarks/>
    [GeneratedCode("System.Web.Services", "2.0.50727.3053")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class GetClientTokenCompletedEventArgs : AsyncCompletedEventArgs
    {
        private readonly object[] results;

        internal GetClientTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled,
                                                  object userState) :
                                                      base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                RaiseExceptionIfNecessary();
                return ((string) (results[0]));
            }
        }
    }
}

#pragma warning restore 1591