using Entity;
using Repository.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public class ContractRepository : IContractRepository
	{
        DAL.Contract.Contract _contract = new DAL.Contract.Contract();
        public List<ContractEntity> GetContractList(string CompanyID) {
            try { 
           
            return _contract.GetContractList(CompanyID);
            }
            catch(Exception ex) {

                return null;
            }
        }
        public List<PurchaseOrderEntity> GetPurcaseorderList(string ContractNumber)
        {
            try
            {

                return _contract.GetPurcaseorderList(ContractNumber);
            }
            catch(Exception ex)
            {

                return null;
            }
        }
        public PurchaseOrderEntity GetPurcaseorderDetail(string ContractNumber, int PurchaseId)
        {
            try
            {

                return _contract.GetPurcaseorderDetail(ContractNumber,PurchaseId);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<ProductEntity> GetCompanyProductsList(string CompanyId)
        {
            try
            {

                return _contract.GetCompanyProductsList(CompanyId);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<z_tbl_Temp_Purchase_Order> GetPurcaseorderBasket(int PurchaseId)
        {
            try
            {

                return _contract.GetPurcaseorderBasket(PurchaseId);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int SavePurchaseOrderDetail(PurchaseOrderEntity obj, List<z_tbl_Temp_Purchase_Order> localBasket)
        {
            try
            {

                return _contract.SavePurchaseOrderDetail(obj,localBasket);
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        public List<StaffList> GetStaffList()
        {
            try
            {

                return _contract.GetStaffList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
