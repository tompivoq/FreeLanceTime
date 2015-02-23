using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeLance.Model
{
    public class Project
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String SubTitle { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float HoursAllocated { get; set; }
    }
}