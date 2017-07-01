using System;
using System.Collections;
using System.Web;
using FluorineFx.Util;
using FluorineFx.Json;
using FluorineFx.Json.Services;

namespace ServiceLibrary
{
    public class JQueryProxyGenerator : IJsonRpcProxyGenerator
    {
        #region IJsonRpcProxyGenerator Members

        public void WriteProxy(ServiceClass service, IndentedTextWriter writer, HttpRequest request)
        {
            writer.Write("function ");
            writer.Write(service.Name);
            writer.WriteLine("(url)");
            writer.WriteLine("{");
            writer.Indent++;
            writer.WriteLine("var self = this;");

            ICollection methodCollection = service.GetMethods();
            Method[] methods = new Method[methodCollection.Count];
            string[] methodNames = new string[methodCollection.Count];
            int i = 0;
            foreach (Method method in methodCollection)
            {
                methods[i] = method;
                methodNames[i] = method.Name;
                i++;
            }


            writer.Write("var m = ");
            //JsonConvert.Export(methodNames, writer);
            writer.Write(JavaScriptConvert.SerializeObject(methodNames));
            writer.WriteLine(';');
            writer.WriteLine();

            for (i = 0; i < methods.Length; i++)
            {
                Method method = methods[i];
                string index = i.ToString(System.Globalization.CultureInfo.InvariantCulture);

                if (method.Description != null && method.Description.Length > 0)
                {
                    writer.Write("// ");
                    writer.WriteLine(method.Description);
                    writer.WriteLine();
                }

                writer.Write("this[m[");
                writer.Write(index);
                writer.Write("]] = function /* ");
                writer.Write(method.Name);
                writer.Write(" */ (");

                Parameter[] parameters = method.GetParameters();

                foreach (Parameter parameter in parameters)
                {
                    writer.Write(parameter.Name);
                    writer.Write(", ");
                }

                writer.WriteLine("callback)");
                writer.WriteLine("{");
                writer.Indent++;

                writer.Write("if (self.kwargs) return rpc(new Call(");
                writer.Write(index);
                writer.Write(", {");

                foreach (Parameter parameter in parameters)
                {
                    if (parameter.Position > 0)
                        writer.Write(',');
                    writer.Write(' ');
                    writer.Write(parameter.Name);
                    writer.Write(": ");
                    writer.Write(parameter.Name);
                }

                writer.WriteLine(" }, callback));");

                writer.Write("return rpc(new Call(");
                writer.Write(index);
                writer.Write(", [");

                foreach (Parameter parameter in parameters)
                {
                    if (parameter.Position > 0)
                        writer.Write(',');
                    writer.Write(' ');
                    writer.Write(parameter.Name);
                }

                writer.WriteLine(" ], callback));");

                writer.Indent--;
                writer.WriteLine("}");
                writer.WriteLine();
            }

            writer.Write("var url = typeof(url) === 'string' ? url : '");
            writer.Write(request.Url);
            writer.WriteLine("';");
            writer.WriteLine(@"var nextId = 0;

    function Call(method, params, callback)
    {
        this.url = url;
        this.callback = callback;
        this.proxy = self;
        this.request = 
        { 
            id     : ++nextId, 
            method : m[method], 
            params : params 
        };
    }

    function rpc(call)
    {
        return self.channel != null && typeof(self.channel.rpc) === 'function' ?
            self.channel.rpc(call) : call;
    }

    this.kwargs = false;
    this.channel = new JayrockChannel();

    function JayrockChannel()
    {
        this.rpc = function(call)
        {
            var async = typeof(call.callback) === 'function';
            var xhr = newXHR();
            xhr.open('POST', call.url, async, this.httpUserName, this.httpPassword);
            xhr.setRequestHeader('Content-Type', this.contentType || 'application/json; charset=utf-8');
            xhr.setRequestHeader('X-JSON-RPC', call.request.method);
            if (async) xhr.onreadystatechange = function() { xhr_onreadystatechange(xhr, call.callback); }
            xhr.send(JSON.stringify(call.request));
            call.handler = xhr;
            if (async) return call;
            if (xhr.status != 200) throw new Error(xhr.status + ' ' + xhr.statusText);
            var response = JSON.eval(xhr.responseText);
            if (response.error != null) throw response.error;
            return response.result;
        }

        function xhr_onreadystatechange(sender, callback)
        {
            if (sender.readyState == /* complete */ 4)
            {
                sender.onreadystatechange = null; // Avoid IE leak

                var response = sender.status == 200 ? 
                    JSON.eval(sender.responseText) : {};
                
                callback(response, sender);
            }
        }

        function newXHR()
        {
            if (typeof(window) !== 'undefined' && window.XMLHttpRequest)
                return new XMLHttpRequest(); /* IE7, Safari 1.2, Mozilla 1.0/Firefox, and Netscape 7 */
            else
                return new ActiveXObject('Microsoft.XMLHTTP'); /* WSH and IE 5 to IE 6 */
        }
    }");

            writer.Indent--;
            writer.WriteLine("}");

            writer.WriteLine();
            writer.Write(service.Name);
            writer.Write(".rpcMethods = ");
            //JsonConvert.Export(methodNames, writer);
            writer.Write(JavaScriptConvert.SerializeObject(methodNames));
            writer.WriteLine(";");
        }

        #endregion
    }
}
