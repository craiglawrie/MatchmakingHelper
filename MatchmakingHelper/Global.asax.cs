using MatchmakingHelper.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MatchmakingHelper
{
    public class MvcApplication : NinjectHttpApplication
    {


        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Map Interfaces to Classes
            kernel.Bind<IStudentDAL>().To<StudentSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ICompanyDAL>().To<CompanySqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IPreferencesDAL>().To<PreferencesSqlDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
