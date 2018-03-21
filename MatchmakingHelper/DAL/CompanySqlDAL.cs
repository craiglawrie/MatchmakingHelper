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

        public bool AddCompanyToDB(string companyName, int numberOfTablesDay1, int numberOfTablesDay2)
        {
            bool result = false;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO company (name, numberoftablesday1, numberoftablesday2) VALUES (@name, @numberoftablesday1, @numberoftablesday2)", conn);
                    cmd.Parameters.AddWithValue("@name", companyName);
                    cmd.Parameters.AddWithValue("@numberoftablesday1", numberOfTablesDay1);
                    cmd.Parameters.AddWithValue("@numberoftablesday2", numberOfTablesDay2);

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
                            NumberOfTablesDay1 = Convert.ToInt32(reader["numberoftablesday1"]),
                            NumberOfTablesDay2 = Convert.ToInt32(reader["numberoftablesday2"])
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

        public bool RemoveCompanyFromDBById(int companyId)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM company WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", companyId);

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
    }
}