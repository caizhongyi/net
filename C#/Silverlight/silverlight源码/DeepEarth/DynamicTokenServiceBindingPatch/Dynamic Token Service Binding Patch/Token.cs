﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using DeepEarth.Provider.VirtualEarth.TokenService;

namespace DeepEarth.Provider.VirtualEarth
{
    public static class Token
    {
        private const string ISOKey = "VEToken.txt";

        private static readonly List<EventHandler> callbacks = new List<EventHandler>();
        private static bool inprogress;
        private static string token;

        // Patch code for "Dynamic Token Service Binding"
        private static BasicHttpBinding _Binding { get; set;}
        private static EndpointAddress _EndPoint{ get; set;}

        // Patch code for "Dynamic Token Service Binding"
        static Token()
        {
            new DynamicTokenServiceBinding();
        }

        // Patch code for "Dynamic Token Service Binding"
        public static void SetBinding(BasicHttpBinding binding, EndpointAddress endPoint)
         {
             _Binding = binding;
             _EndPoint = endPoint;
         }

        public static void GetToken(EventHandler callback)
        {

            if(token != null)
            {
                callback(null, new TokenResultArgs { Result = token });
                return;
            }

            callbacks.Add(callback);

            if(!inprogress)
            {
                inprogress = true;

                //get token
                var tokenservice = new TokenServiceClient(_Binding, _EndPoint);

                tokenservice.GetTokenCompleted += (otokenservice, etokenservice) =>
                                                    {
                                                        try
                                                        {
                                                            token = etokenservice.Result;
                                                            IsolatedStorage.SaveData(token, ISOKey);
                                                            foreach(EventHandler handler in callbacks)
                                                            {
                                                                handler(null, new TokenResultArgs { Result = token });
                                                            }
                                                            inprogress = false;
                                                        }
                                                        catch(Exception)
                                                        {
                                                            //use last good token if available.
                                                            token = IsolatedStorage.LoadStringData(ISOKey);
                                                            if(token.Length > 0)
                                                            {
                                                                foreach(EventHandler handler in callbacks)
                                                                {
                                                                    handler(null, new TokenResultArgs { Result = token });
                                                                }
                                                            }
                                                            //or else wait for next call to try again.
                                                            inprogress = false;
                                                        }
                                                    };
                tokenservice.GetTokenAsync();
            }
        }
    }

    public class TokenResultArgs : EventArgs
    {
        public String Result { get; set; }
    }
}