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

        public StudentsController(IStudentDAL studentDAL)
        {
            this.studentDAL = studentDAL;
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

        
    }
}