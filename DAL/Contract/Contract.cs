using Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
	public class Contract
	{
		DapperManager dapper;
		private static IConfiguration _configuration;
		public Contract()
		{
			dapper = new DapperManager();
		}
		public List<ContractEntity> GetContractList(string CompanyID)
		{

			string Query = string.Format($"SELECT contract.*,"+
				$"contracttypes.ContractName, equipmenttype.EquipmentType " +
				
				$"FROM contracttypes " +
				$"RIGHT JOIN (equipmenttype RIGHT JOIN contract ON equipmenttype.EquipmentType = contract.ContractEquipmentType) ON " +
				$"contracttypes.ContractCode = contract.ContractType where contract.ContractCompanyId = '" + CompanyID +"' ");
			var Data = dapper.Query<ContractEntity>(Query, null, null, true, null, CommandType.Text);
			return Data.ToList();
		}
        public List<PurchaseOrderEntity> GetPurcaseorderList(string ContractNumber)
        {

            string Query = string.Format($"SELECT purchaseorder.PurchaseId, purchaseorder.PurchaseContractNumber, " +
				$"purchaseorder.PurchaseCompanyId, company.CompanyName, purchaseorder.PurchaseOrderDate, " +
				$"lng_Sum_PO+PurchaseCarriageCosts AS TotalPurchase\r\n" +
				$"FROM company INNER JOIN (purchaseorder INNER JOIN tbl_total_sales ON purchaseorder.PurchaseId = tbl_total_sales.PurchaseOrderId)" +
				$" ON company.CompanyId = purchaseorder.PurchaseCompanyId\r\n" +
				$"WHERE purchaseorder.PurchaseContractNumber='"+ContractNumber+"' ");
            var Data = dapper.Query<PurchaseOrderEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public PurchaseOrderEntity GetPurcaseorderDetail(string ContractNumber,int PurchaseId)
        {

            string Query = string.Format($"Select * from purchaseorder " +
                $"WHERE PurchaseContractNumber='" + ContractNumber + "' and PurchaseId='"+PurchaseId+"' ");
            var Data = dapper.Query<PurchaseOrderEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList().FirstOrDefault();
        }
        public List<ProductEntity> GetCompanyProductsList(string CompanyId)
        {

            string Query = string.Format($"SELECT product.ProductId AS ID, product.ProductName," +
				$" product.ProductSizeOrType, product.ProductPrice, product.ProductSupplierId, " +
				$"company.CompanyName\r\n" +
				$"FROM company " +
				$"INNER JOIN product ON company.CompanyId = product.ProductSupplierId\r\n" +
				$"WHERE product.ProductSupplierId='"+CompanyId+"'\r\n" +
				$"ORDER BY product.ProductId");
            var Data = dapper.Query<ProductEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public List<z_tbl_Temp_Purchase_Order> GetPurcaseorderBasket(int PurchaseId)
        {

			string Query = string.Format($"Delete from z_tbl_temp_purchase_order; \r\n" +
				$"INSERT INTO z_tbl_temp_purchase_order \r\n( PurchaseItemId, PurchaseOrderId," +
				$" PurchaseProductId, ProductName, ProductSupplierId,\r\n   " +
				$"         ProductCategoryId, ProductPrice, Quantity," +
				$" ProductSizeOrType, TotalPrice,\r\n\t\t\t" +
				$"bln_Loaded,bln_Amount_Updated,bln_Delete ,bln_Edit)" +
				$" \r\n            " +
				$"SELECT purchaseorderproducts.PurchaseItemId, " +
				$"purchaseorderproducts.PurchaseOrderId,\r\n    " +
				$"        " +
				$"purchaseorderproducts.PurchaseProductId, product.ProductName, \r\n  " +
				$"          product.ProductSupplierId, product.ProductCategoryId,\r\n  " +
				$"          product.ProductPrice, " +
				$"purchaseorderproducts.PurchaseQuantity, \r\n " +
				$"           product.ProductSizeOrType," +
				$" ProductPrice*PurchaseQuantity AS Total_Price, " +
				$"1 AS Flag \r\n\t\t\t,0 as bln_Amount_Updated\r\n\t\t\t," +
				$"0 as bln_Delete\r\n\t\t\t,0 as bln_Edit\r\n  " +
				$"          FROM purchaseorderproducts \r\n    " +
				$"        INNER JOIN product ON purchaseorderproducts.PurchaseProductId = product.ProductId \r\n  " +
				$"          WHERE purchaseorderproducts.PurchaseOrderId='" + PurchaseId + "'; " +
			
				$" SELECT z_tbl_temp_purchase_order.PurchaseItemId, " +
				$"z_tbl_temp_purchase_order.PurchaseOrderId AS PID," +
				$" \r\nz_tbl_temp_purchase_order.PurchaseProductId," +
				$"\r\nz_tbl_temp_purchase_order.ProductName," +
				$" \r\nz_tbl_temp_purchase_order.ProductPrice," +
				$" \r\nz_tbl_temp_purchase_order.Quantity," +
				$" \r\nz_tbl_temp_purchase_order.TotalPrice, " +
				$"\r\nz_tbl_temp_purchase_order.ProductId, " +
				$"\r\nz_tbl_temp_purchase_order.bln_Loaded," +
				$" \r\nz_tbl_temp_purchase_order.bln_Edit," +
				$"\r\nz_tbl_temp_purchase_order.bln_Delete\r\n" +
				$" FROM z_tbl_temp_purchase_order " +
				$"WHERE PurchaseOrderId='" + PurchaseId + "' and bln_Delete= 0 "); 
            var Data = dapper.Query<z_tbl_Temp_Purchase_Order>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public int SavePurchaseOrderDetail(PurchaseOrderEntity obj, List<z_tbl_Temp_Purchase_Order> localBasket)
        {
            string Query = "";
			string _PurchaseRequiredDate = null;
			if(obj != null)
			{
				if(obj.PurchaseRequiredDate != null)
				{
					_PurchaseRequiredDate = obj.PurchaseRequiredDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                }
			}
                Query = string.Format($"UPDATE purchaseorder SET PurchaseOrderDate = '" + obj.PurchaseOrderDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    //$", PurchaseRequiredDate = '" + _PurchaseRequiredDate + "' " +
                    //$", PurchaseRequiredTime = '" + obj.PurchaseRequiredTime + "'" +
                    $", PurchaseOrderNotes ='" + obj.PurchaseOrderNotes + "'" +
                    $", PurchaseDeliveryAddress = '" + obj.PurchaseDeliveryAddress + "'" +
                    $"  WHERE PurchaseId = '" + obj.PurchaseId + "'");


            var result = dapper.Execute<int>(Query, null, null, true, null, CommandType.Text);
			var Query2 = "";
			if(localBasket !=null && localBasket.Count > 0)
			{
				var existingItem= localBasket.Where(x => x.PurchaseItemId >0 ).ToList();
				if(existingItem !=null && existingItem.Count > 0)
				{
					foreach (var item in existingItem)
					{
						Query2 = string.Format($"\r\nUPDATE purchaseorderproducts\r\n" +
						$"SET \r\npurchaseorderproducts.PurchaseQuantity = '"+item.Quantity+"'," +
                        $" \r\npurchaseorderproducts.PurchaseUnitPrice = '" +item.ProductPrice+"'," +
                        $" \r\npurchaseorderproducts.sys_User_Name = 'admin'," +
						$" \r\npurchaseorderproducts.sys_User_Date = NOW()\r\n" +
						$"WHERE purchaseorderproducts.PurchaseOrderId='"+obj.PurchaseId+"'\r\n" +
						$"AND purchaseorderproducts.PurchaseProductId ='"+item.PurchaseProductId+"'\r\n" +
						$"AND purchaseorderproducts.PurchaseItemId='"+item.PurchaseItemId+"';" +
                        "\r\n\t\t   Update product\r\n\t\t  " +
						" Set ProductPrice = '"+item.ProductPrice+"'\r\n\t\t " +
						"  where ProductId = '"+item.PurchaseProductId+"' ");
						var result2 = dapper.Execute<int>(Query2, null, null, true, null, CommandType.Text);
					 
					}

					
                }
				

				var isNew = localBasket.Where(x=>x.PurchaseItemId == -1).ToList();
				if(isNew !=null && isNew.Count > 0)
				{
                    foreach (var item in isNew)
                    {
                        string Query5 = string.Format($"\r\nSelect (MAX(PurchaseItemId) + 1) as MaxPurchaseItemID  from purchaseorderproducts;");
                        var Data5 = dapper.Query<PurchaseMaxItemValue>(Query5, null, null, true, null, CommandType.Text);
                        var maxvalue= Data5.ToList().FirstOrDefault().MaxPurchaseItemID;
                        Query2 = string.Format($"\r\n" +
							$"\r\n INSERT INTO purchaseorderproducts\r\n" +
							$"           (\r\n  PurchaseItemId,     PurchaseQuantity\r\n   " +
							$"        ,PurchaseUnitPrice\r\n     " +
							$"      ,PurchaseOrderId\r\n    " +
							$"       ,PurchaseProductId\r\n   " +
							$"        ,sys_User_Name\r\n   " +
							$"        ,sys_User_Date)\r\n  " +
							$"   VALUES\r\n           ('"+ maxvalue + "', '" +item.Quantity+"','"+item.ProductPrice+"','"+obj.PurchaseId+"','"+item.PurchaseProductId+"','admin',NOW()); " +
                            "\r\n\t\t   Update product\r\n\t\t " +
							"  Set ProductPrice = '"+item.ProductPrice+"'\r\n\t\t  " +
							" where ProductId = '"+item.PurchaseProductId+"'");
                        var result2 = dapper.Execute<int>(Query2, null, null, true, null, CommandType.Text);
                    }
                }
				
			}
			var query4 = "";
			var query5 = string.Format($"Select * from tbl_total_sales where PurchaseOrderId = '" + obj.PurchaseId + "'");
            var Data = dapper.Query<ProductEntity>(Query, null, null, true, null, CommandType.Text);
            var Query3 = "";
			if (Data != null && Data.ToList().Count > 0)
			{
				Query3 = string.Format($"Insert into tbl_total_sales(PurchaseOrderId,lng_Sum_PO,sys_User_Name,sys_User_Date)" +
			$"\r\nvalues('" + obj.PurchaseId + "','" + obj.TotalPurchase + "','admin',NOW());\r\n" );
			}
			else
			{
				Query3 = string.Format($"Update tbl_total_sales\r\n" +
					$"Set lng_Sum_PO = '" + obj.TotalPurchase + "'\r\n" +
					$"where PurchaseOrderId = '" + obj.PurchaseId + "';" +
					$"\r\n");
			}
            var result3 = dapper.Execute<int>(Query3, null, null, true, null, CommandType.Text);
            return result;
        }
        public List<StaffList> GetStaffList()
        {

            string Query = string.Format($"SELECT staff.StaffId, CONCAT('', StaffForename, ' ', StaffLastname) Expr1\r\nFROM staff\r\nWHERE (((CONCAT('', StaffForename, ' ', StaffLastname))<>''))\r\nORDER BY staff.StaffId, staff.StaffHaveLeft DESC , staff.StaffLastname");
            var Data = dapper.Query<StaffList>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
    }
}
