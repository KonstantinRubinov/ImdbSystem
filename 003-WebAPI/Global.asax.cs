using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;

namespace ImdbSystem
{
	public class WebApiApplication : System.Web.HttpApplication
    {
		private void ConfigureApi()
		{
			var container = new Container();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			container.Register<IImdbRepository, ImdbManager>();

			if (GlobalVariable.logicType == 0)
			{
				container.Register<IUsersRepository, EntityUsersManager>();
				container.Register<IMoviesExtendRepository, EntityMoviesExtendManager>();
				container.Register<IMoviesRepository, EntityMoviesManager>();
			}
			else if (GlobalVariable.logicType == 1)
			{
				container.Register<IUsersRepository, SqlUsersManager>();
				container.Register<IMoviesExtendRepository, SqlMoviesExtendManager>();
				container.Register<IMoviesRepository, SqlMoviesManager>();
			}
			else if (GlobalVariable.logicType == 2)
			{
				container.Register<IUsersRepository, MySqlUsersManager>();
				container.Register<IMoviesExtendRepository, MySqlMoviesExtendManager>();
				container.Register<IMoviesRepository, MySqlMoviesManager>();
			}
			else if (GlobalVariable.logicType == 3)
			{
				container.Register<IUsersRepository, MongoUsersManager>();
				container.Register<IMoviesExtendRepository, MongoMoviesExtendManager>();
				container.Register<IMoviesRepository, MongoMoviesManager>();
			}
			container.Verify();
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
		}

		protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();
			ConfigureApi();

			GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
    }
}
