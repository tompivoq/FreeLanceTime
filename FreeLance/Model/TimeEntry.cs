using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeLance.Model
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        public DateTime RegistrationDate { get; set; }
        public float HoursSpent { get; set; }
        public String Comment { get; set; }
    }
}