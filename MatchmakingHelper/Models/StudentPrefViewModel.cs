using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchmakingHelper.Models
{
    public class StudentPrefViewModel
    {
        public List<Company> AllCompanies { get; set; }
        public Student Student { get; set; }
    }
}