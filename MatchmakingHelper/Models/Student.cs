using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchmakingHelper.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Company> Preferences { get; set; }
    }
}