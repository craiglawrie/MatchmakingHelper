using MatchmakingHelper.DAL;
using MatchmakingHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchmakingHelper.Controllers
{
    public class CompanyController : Controller
    {

        ICompanyDAL companyDAL;

        public CompanyController(ICompanyDAL companyDAL)
        {
            this.companyDAL = companyDAL;
     
        }

        public ActionResult Index()
        {
            List<Company> companies = companyDAL.GetAllCompanies();
            return View("Index", companies);
        }

        [HttpPost]
        public ActionResult Index(string companyName, int numberOfTables)
        {
            companyDAL.AddCompanyToDB(companyName, numberOfTables);
            return RedirectToAction("Index");
        }
    }
}