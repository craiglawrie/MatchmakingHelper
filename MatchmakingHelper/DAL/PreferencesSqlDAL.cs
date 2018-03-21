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
                    SqlCommand cmd = new SqlCommand("SELECT " +
                                                    "   c.id as companyId, " +
                                                    "   c.name as companyName " +
                                                    "FROM student_company_preferences scp " +
                                                    "JOIN company c ON c.id = scp.company_id " +
                                                    "WHERE scp.student_id = @id " +
                                                    "ORDER BY scp.preference_rank;", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Company company = new Company()
                        {
                            Id = Convert.ToInt32(reader["companyId"]),
                            Name = Convert.ToString(reader["companyName"])
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

        public bool AddCompanyPreference(string studentId, int companyId, int preferenceRank)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO student_company_preferences (student_id, company_id, preference_rank) VALUES (@studentId, @companyId, @preferenceRank)", conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    cmd.Parameters.AddWithValue("@preferenceRank", preferenceRank);

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

        public bool RemoveCompanyPreference(string studentId, int companyId)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM student_company_preferences WHERE student_id = @studentId AND company_id = @companyId", conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@companyId", companyId);

                    result = cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            RenumberRemainingPreferenceRanks(studentId);

            return result;
        }

        private void RenumberRemainingPreferenceRanks(string studentId)
        {
            List<Company> companies = GetPreferredCompaniesByStudentId(studentId);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE student_company_preferences " +
                                                    "SET preference_rank = @preferenceRank " +
                                                    "WHERE " +
                                                    "   student_id = @studentId AND " +
                                                    "   company_id = @companyId", conn);

                    for (int i = 0; i < companies.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@companyId", companies[i].Id);
                        cmd.Parameters.AddWithValue("@preferenceRank", i + 1);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}