using MatchmakingHelper.DAL;
using MatchmakingHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchmakingHelper.Controllers
{
    public class StudentsController : Controller
    {
        IStudentDAL studentDAL;
        ICompanyDAL companyDAL;
        IPreferencesDAL preferencesDAL;

        public StudentsController(IStudentDAL studentDAL, ICompanyDAL companyDAL, IPreferencesDAL preferencesDAL)
        {
            this.studentDAL = studentDAL;
            this.companyDAL = companyDAL;
            this.preferencesDAL = preferencesDAL;
        }

        public ActionResult Index()
        {
            List<Student> students = studentDAL.GetAllStudents();
            return View("Index", students);
        }

        [HttpPost]
        public ActionResult Index(string studentName)
        {
            List<Student> students = studentDAL.GetAllStudents();
            if (!students.Select(s => s.Name).Contains(studentName))
            {
                studentDAL.AddStudentToDB(studentName);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Preferences(string id)
        {
            StudentPrefViewModel spv = new StudentPrefViewModel();

            spv.Student = studentDAL.GetStudentById(id);
            spv.AllCompanies = companyDAL.GetAllCompanies();
            spv.Student.Preferences = preferencesDAL.GetPreferredCompaniesByStudentId(id);

            return View("Preferences", spv);
        }
    }
}