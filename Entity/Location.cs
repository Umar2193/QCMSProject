using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Location
    {
        public int Location_ID { get; set; }
        public string CompanyId { get; set; }
        public string ContractLocation { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
    }
}
