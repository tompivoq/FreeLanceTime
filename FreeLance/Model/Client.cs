using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeLance.Model
{
    public class Client
    {
        public int Id { get; set; }
        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String ContactName { get; set; }
        public String Email { get; set; }
        public String WebSite { get; set; }
        public String CVR { get; set; }
        public DateTime CreationDate { get; set; }

    }
}