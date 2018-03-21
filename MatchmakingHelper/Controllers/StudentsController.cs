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
        public ActionResult AddStudent(string studentName, string studentEmail)
        {
            List<Student> students = studentDAL.GetAllStudents();
            if (!students.Select(s => s.Name).Contains(studentName))
            {
                Student student = new Student();
                student.Name = studentName;
                student.Email = studentEmail;
                studentDAL.AddStudentToDB(student);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveStudent(string id)
        {
            preferencesDAL.RemoveAllPreferencesForStudent(id);
            studentDAL.RemoveStudentFromDBById(id);
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

        [HttpPost]
        public ActionResult AddPreference(string studentId, int companyId)
        {
            List<Company> preferences = preferencesDAL.GetPreferredCompaniesByStudentId(studentId);
            if (!preferences.Select(p => p.Id).Contains(companyId))
            {
                preferencesDAL.AddCompanyPreference(studentId, companyId, preferences.Count + 1);
            }


            return RedirectToAction("Preferences", new { id = studentId });
        }

        [HttpPost]
        public ActionResult ActOnPreferredCompanies(string studentId, int companyId, string buttonPressed)
        {
            if (buttonPressed == "removePreference")
            {
                preferencesDAL.RemoveCompanyPreference(studentId, companyId);
            }
            else
            {
                List<Company> preferences = preferencesDAL.GetPreferredCompaniesByStudentId(studentId);
                if (buttonPressed == "increasePreference")
                {
                    Company source;
                    Company target;
                    for (int i = 1; i < preferences.Count; i++)
                    {
                        if (preferences[i].Id == companyId)
                        {
                            source = preferences[i];
                            target = preferences[i - 1];
                            preferencesDAL.ExchangeRanksBetween(source, target, studentId);
                        }
                    }
                }
                else if (buttonPressed == "decreasePreference")
                {
                    Company source;
                    Company target;
                    for (int i = 0; i < preferences.Count - 1; i++)
                    {
                        if (preferences[i].Id == companyId)
                        {
                            source = preferences[i];
                            target = preferences[i + 1];
                            preferencesDAL.ExchangeRanksBetween(source, target, studentId);
                        }
                    }
                }
            }

            return RedirectToAction("Preferences", new { id = studentId });
        }
    }
}