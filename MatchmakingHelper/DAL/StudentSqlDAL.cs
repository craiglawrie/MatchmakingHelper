using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatchmakingHelper.Models;
using System.Data.SqlClient;

namespace MatchmakingHelper.DAL
{
    public class StudentSqlDAL : IStudentDAL
    {
        string connectionString;

        public StudentSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM student", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student()
                        {
                            Id = Convert.ToString(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            Email = Convert.ToString(reader["email"])
                        };

                        students.Add(student);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return students;
        }

        public bool AddStudentToDB(Student student)
        {
            bool result = false;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO student (id, name, email) VALUES (@id, @name, @email)", conn);
                    cmd.Parameters.AddWithValue("@id", student.Name.GetHashCode().ToString());
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@email", student.Email);

                    result = cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return result;
        }

        public Student GetStudentById(string id)
        {
            Student student = new Student();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM student WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        student.Id = Convert.ToString(reader["id"]);
                        student.Name = Convert.ToString(reader["name"]);
                        student.Email = Convert.ToString(reader["email"]);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return student;
        }

        public bool RemoveStudentFromDBById(string id)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM student WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    result = cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return result;
        }

        string SqlQueryStringStudentPref = "SELECT " +
                                           "   s.id as student_id, " +
                                           "   s.name as student_name, " +
                                           "   scp.preference_rank as rank, " +
                                           "   c.name as company_name " +
                                           "FROM student s " +
                                           "JOIN student_company_preferences scp ON s.id = scp.student_id " +
                                           "JOIN company c ON c.id = scp.company_id;";

    }
}