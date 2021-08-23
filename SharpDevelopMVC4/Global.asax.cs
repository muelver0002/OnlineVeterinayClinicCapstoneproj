﻿/*
 * Created by SharpDevelop.
 * User: Gabs
 * Date: 19/07/2019
 * Time: 2:03 am
 */
 
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Newtonsoft.Json.Serialization;
using Hangfire.MemoryStorage;

namespace SharpDevelopMVC4
{
	public class MvcApplication : HttpApplication
	{
        Hangfire.BackgroundJobServer _backgroundJobServer;
        
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.Ignore("{resource}.axd/{*pathInfo}");
			
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {
					controller = "Home",
					action = "Index",
					id = UrlParameter.Optional
				});
		}

        protected void Application_Start()
		{        	
        	AutoMapperConfig.Initialize();
        	
			RegisterRoutes(RouteTable.Routes);
			
			AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
			           
			// Configure Hangfire www.hangfire.io            
			Hangfire.GlobalConfiguration.Configuration.UseMemoryStorage();
			_backgroundJobServer = new Hangfire.BackgroundJobServer();         

			SimpleLogger.Init();
        }
        
        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }

        protected void  Application_PostAuthenticateRequest(Object sender, EventArgs e)
		{
		    HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
		    if (authCookie == null || authCookie.Value == "")
		        return;
		    	    
		    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

	        FormsIdentity formsIdentity = new FormsIdentity(authTicket);
	
	        ClaimsIdentity claimsIdentity = new ClaimsIdentity(formsIdentity);
	
	        var roles = authTicket.UserData.Split(',');
	             
	        foreach (var role in roles)
	        {
	            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
	        }
	
	        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
	
	        HttpContext.Current.User = claimsPrincipal;
		}

        #region Session
        // Avoid Session at all cost!!!
        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
			HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
        #endregion
        
    }
}
