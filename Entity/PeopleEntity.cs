using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PeopleEntity
    {
        public int PeopleId { get; set; }
        public string PeopleTitle { get; set; }

        public string PeopleInitials { get; set; }
        public string PeopleForename { get; set; }

        public string PeopleLastname { get; set; }
        public string PeopleJobTitle { get; set; }
        public string PeopleDirectTelephone { get; set; }
        public string PeopleMobile { get; set; }
        public string PeopleEmail { get; set; }
        public string PeopleOtherContactDetails { get; set; }
        public string PeopleCompanyId { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
    }
}
