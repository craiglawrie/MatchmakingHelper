using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatchmakingHelper.Models;
using System.Data.SqlClient;

namespace MatchmakingHelper.DAL
{
    public class PreferencesSqlDAL : IPreferencesDAL
    {
        string connectionString;

        public PreferencesSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Company> GetPreferredCompaniesByStudentId(string id)
        {
            List<Company> companies = new List<Company>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM student_company_preferences WHERE student_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Company company = new Company()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"])
                        };

                        companies.Add(company);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return companies;
        }
    }
}