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
    }
}
