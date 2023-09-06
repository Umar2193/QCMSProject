using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CallsPromptsEntity
    {
        public int CallID { get; set; }
        public string CallCompanyID { get; set; }
        public int CallPeopleID { get; set; }
        public DateTime? CallWhen { get; set; }
        public int CallStaff { get; set; }
        public string CallNotes { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
        public string PeopleName { get; set; }
        public string StaffName { get; set; }
    }
}
