using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatchmakingHelper.Models;
using System.Data.SqlClient;

namespace MatchmakingHelper.DAL
{
    public class CompanySqlDAL : ICompanyDAL
    {
        private string connectionString;

        public CompanySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddCompanyToDB(string companyName, int numberOfTables)
        {
            bool result = false;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO company (name, numberoftables) VALUES (@name, @numberoftables)", conn);
                    cmd.Parameters.AddWithValue("@name", companyName);
                    cmd.Parameters.AddWithValue("@numberoftables", numberOfTables);

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

        public List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM company", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Company company = new Company()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            NumberOfTables = Convert.ToInt32(reader["numberoftables"])
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