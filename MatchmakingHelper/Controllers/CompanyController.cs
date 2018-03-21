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
        IPreferencesDAL preferencesDAL;

        public CompanyController(ICompanyDAL companyDAL, IPreferencesDAL preferencesDAL)
        {
            this.companyDAL = companyDAL;
            this.preferencesDAL = preferencesDAL;
        }

        public ActionResult Index()
        {
            List<Company> companies = companyDAL.GetAllCompanies();
            return View("Index", companies);
        }

        [HttpPost]
        public ActionResult AddCompany(string companyName, int numberOfTablesDay1, int numberOfTablesDay2)
        {
            companyDAL.AddCompanyToDB(companyName, numberOfTablesDay1, numberOfTablesDay2);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveCompany(int companyId)
        {
            preferencesDAL.RemoveAllPreferencesForCompany(companyId);
            companyDAL.RemoveCompanyFromDBById(companyId);
            return RedirectToAction("Index");
        }
    }
}