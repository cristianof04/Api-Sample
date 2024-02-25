using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ApiSample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (this.User == null)
            {
                string auth = this.Context.Request.Headers["Authorization"];
                if ((auth != null) && auth.StartsWith("Basic"))
                {
                    byte[] bytes = Convert.FromBase64String(auth.Substring(auth.IndexOf("Basic ") + 6));
                    string s = System.Text.Encoding.UTF8.GetString(bytes);


                    string[] userPass = s.Split(new char[] { ':' });
                    string username = userPass[0];
                    string password = userPass[1];
                    GenericPrincipal user = null;
                    if (ValidarUtilizador(username, password, out user))
                    {
                        Context.User = user;
                    }
                }
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Response.StatusCode == 401)
            {
                string realm = "ApiSample.BasicAuth";
                string val = String.Format("Basic Realm=\"{0}\"", realm);
                Response.AppendHeader("WWW-Authenticate", val);
            }
        }



        private static bool ValidarUtilizador(string username, string password, out GenericPrincipal user)
        {
            NameValueCollection appSettings =
              System.Web.Configuration.WebConfigurationManager.AppSettings;

            user = null;
            if (string.Compare(username, appSettings["username"], true) == 0 &&
                string.Compare(password, appSettings["password"], true) == 0)
            {
                string[] roles = { "" };
                user = new GenericPrincipal(new GenericIdentity(username, "ApiSample.BasicAuth"), roles);
                return true;
            }
            return false;
        }
    }
}
