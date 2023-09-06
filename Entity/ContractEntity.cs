using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class ContractEntity
	{
        public string EquipmentType { get; set; }
        public string ContractNumber { get; set; }
        public string ContractCompanyId { get; set; }
        public string ContractLocation { get; set; }
        public string ContractQuotationNo { get; set; }
        public string ContractPO { get; set; }
        public string ContractCustOrderNo { get; set; }
		public DateTime? ContractOrderDate { get; set; }
		public DateTime? ContractPlannedStartDate { get; set; }
		public DateTime? ContractPlannedCompDate { get; set; }
		public DateTime? ContractActualStartDate { get; set; }
		public DateTime? ContractActualCompDate { get; set; }
        public int ContractManagedBy { get; set; }
		public string ContractCustomerSector { get; set; }
		public string ContractType { get; set; }
		public string ContractInvoiceType { get; set; }
		public string ContractSiteName { get; set; }
		public string ContractSiteAddressLines { get; set; }
		public string ContractSiteCounty { get; set; }
		public string ContractSitePostCode { get; set; }
		public string ContractSiteContact { get; set; }
		public string ContractSiteTelephone { get; set; }
		public string ContractInvoiceName { get; set; }
		public string ContractInvoiceAddressLines { get; set; }
		public string ContractInvoiceCounty { get; set; }
		public string ContractInvoicePostCode { get; set; }
		public string ContractInvoiceContact { get; set; }
		public string ContractInvoiceContactTel { get; set; }
		public string ContractAction { get; set; }
		public string ContractEquipmentType { get; set; }
		public string ContractNote { get; set; }
        public decimal ContractProfitPulledToDate { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }

    }
    
}
