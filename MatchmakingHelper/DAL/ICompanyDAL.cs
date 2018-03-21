using MatchmakingHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchmakingHelper.DAL
{
    public interface ICompanyDAL
    {
        List<Company> GetAllCompanies();
        bool AddCompanyToDB(string companyName, int numberOfTablesDay1, int numberOfTablesDay2);
        bool RemoveCompanyFromDBById(int companyId);
    }
}
