using MatchmakingHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchmakingHelper.DAL
{
    public interface IPreferencesDAL
    {
        List<Company> GetPreferredCompaniesByStudentId(string id);
        bool AddCompanyPreference(string studentId, int companyId, int preferenceRank);
        bool RemoveCompanyPreference(string studentId, int companyId);
        bool ExchangeRanksBetween(Company source, Company target, string studentId);
    }
}
