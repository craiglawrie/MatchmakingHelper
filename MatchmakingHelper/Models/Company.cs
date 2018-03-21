using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchmakingHelper.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfTablesDay1 { get; set; }
        public int NumberOfTablesDay2 { get; set; }
    }
}