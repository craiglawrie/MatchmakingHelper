using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchmakingHelper.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Company> Preferences { get; set; }
    }
}