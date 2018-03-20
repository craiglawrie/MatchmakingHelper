using MatchmakingHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchmakingHelper.DAL
{
    public interface IStudentDAL
    {
        List<Student> GetAllStudents();
        Student GetStudentById(string id);
        bool AddStudentToDB(Student student);
        bool RemoveStudentFromDBById(string id);
    }
}
