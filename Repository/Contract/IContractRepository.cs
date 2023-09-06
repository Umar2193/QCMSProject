using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public interface IContractRepository
    {
        List<ContractEntity> GetContractList(string CompanyID);
        List<PurchaseOrderEntity> GetPurcaseorderList(string ContractNumber);
        PurchaseOrderEntity GetPurcaseorderDetail(string ContractNumber, int PurchaseId);
        List<ProductEntity> GetCompanyProductsList(string CompanyId);
        List<z_tbl_Temp_Purchase_Order> GetPurcaseorderBasket(int PurchaseId);
        int SavePurchaseOrderDetail(PurchaseOrderEntity obj, List<z_tbl_Temp_Purchase_Order> localBasket);
        List<StaffList> GetStaffList();
    }
}
