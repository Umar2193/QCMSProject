using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ContractDTO
    {
        public ContractDTO()
        {
            lstContract = new List<ContractEntity>();
            contractDetailDTO = new ContractDetailDTO();
        }
        public List<ContractEntity> lstContract { get; set; }
        public ContractDetailDTO contractDetailDTO { get; set; }
    }
    public class ContractDetailDTO
    {
        public ContractDetailDTO() {
            contractEntity = new ContractEntity();
            lstPurcaseOrder = new List<PurchaseOrderEntity>();
        }
        public ContractEntity contractEntity { get; set; }
        public List<PurchaseOrderEntity> lstPurcaseOrder { get; set; }

    }
    public class  PurchaseOrderEntity
    {
        public int PurchaseId { get; set; }
        public string PurchaseContractNumber { get; set; }
        public string PurchaseCompanyId { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public DateTime?  PurchaseRequiredDate { get; set; }
        public string  PurchaseRequiredTime { get; set; }
        public decimal PurchaseCarriageCosts { get; set; }
        public int PurchaseOrderedBy { get; set; }
        public bool PurchaseChecked { get; set; }
        public int PurchaseCheckedWhoBy { get; set; }
        public DateTime? PurchaseCheckedWhen { get; set; }
        public string PurchaseOrderNotes { get; set; }
        public string PurchaseDeliveryAddress { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
        public string CompanyName { get; set; }
        public decimal TotalPurchase { get; set; }

    }
    public class PurchaseOrderDTO
    {
        public PurchaseOrderDTO()
        {
            lstProductEntities = new List<ProductEntity>();
            purchaseOrderEntity = new PurchaseOrderEntity();
            Lstz_Tbl_Temp_Purchase_Orders = new List<z_tbl_Temp_Purchase_Order>();
        }
        public List<ProductEntity> lstProductEntities { get; set; }
        public PurchaseOrderEntity purchaseOrderEntity { get; set; }
        public List<z_tbl_Temp_Purchase_Order> Lstz_Tbl_Temp_Purchase_Orders { get; set; }

    }
    public class StaffList
    {
        public int StaffId { get; set; }
        public string Expr1 { get; set; }

    }
}
