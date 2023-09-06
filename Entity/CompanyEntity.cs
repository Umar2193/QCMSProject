using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CompanyEntity
    {
        public CompanyEntity() {
            lstsites = new List<Location>();
            lstPeople = new List<PeopleEntity>();
        }
        public string CompanyId { get; set; }
        public bool CompanyIsCustomer { get; set; }
        public bool CompanyIsSupplier { get; set; }
        public bool CompanyIsSubcontractor { get; set; }
        public bool CompanyIsEnquirer { get; set; }
        public bool CompanyIsContracts { get; set; }
        public string CompanyShortCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressLines { get; set; }
        public string CompanyTown { get; set; }
        public string CompanyCounty { get; set; }
        public string CompanyPostCode { get; set; }
        public string CompanyMainTelephone { get; set; }
        public string CompanyMainFax { get; set; }
        public string CompanyAccountNo { get; set; }
        public bool CompanyApproved { get; set; }
        public int CompanyCreditDays { get; set; }
        public string CompanyMentorRef { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
        public List<Location> lstsites { get; set; }
        public List<PeopleEntity> lstPeople { get; set; }
    }
}
