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

			string Query = string.Format($"SELECT Contract.*,"+
				$"ContractTypes.ContractName, EquipmentType.EquipmentType " +
				
				$"FROM ContractTypes " +
				$"RIGHT JOIN (EquipmentType RIGHT JOIN Contract ON EquipmentType.EquipmentType = Contract.ContractEquipmentType) ON " +
				$"ContractTypes.ContractCode = Contract.ContractType where Contract.ContractCompanyId = '" + CompanyID +"' ");
			var Data = dapper.Query<ContractEntity>(Query, null, null, true, null, CommandType.Text);
			return Data.ToList();
		}
        public List<PurchaseOrderEntity> GetPurcaseorderList(string ContractNumber)
        {

            string Query = string.Format($"SELECT PurchaseOrder.PurchaseId, PurchaseOrder.PurchaseContractNumber, " +
				$"PurchaseOrder.PurchaseCompanyId, Company.CompanyName, PurchaseOrder.PurchaseOrderDate, " +
				$"[lng_Sum_PO]+[PurchaseCarriageCosts] AS TotalPurchase\r\n" +
				$"FROM Company INNER JOIN (PurchaseOrder INNER JOIN tbl_Total_Sales ON PurchaseOrder.PurchaseId = tbl_Total_Sales.PurchaseOrderId)" +
				$" ON Company.CompanyId = PurchaseOrder.PurchaseCompanyId\r\n" +
				$"WHERE PurchaseOrder.PurchaseContractNumber='"+ContractNumber+"' ");
            var Data = dapper.Query<PurchaseOrderEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public PurchaseOrderEntity GetPurcaseorderDetail(string ContractNumber,int PurchaseId)
        {

            string Query = string.Format($"Select * from PurchaseOrder " +
                $"WHERE PurchaseContractNumber='" + ContractNumber + "' and PurchaseId='"+PurchaseId+"' ");
            var Data = dapper.Query<PurchaseOrderEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList().FirstOrDefault();
        }
        public List<ProductEntity> GetCompanyProductsList(string CompanyId)
        {

            string Query = string.Format($"SELECT Product.ProductId AS ID, Product.ProductName," +
				$" Product.ProductSizeOrType, Product.ProductPrice, Product.ProductSupplierId, " +
				$"Company.CompanyName\r\n" +
				$"FROM Company " +
				$"INNER JOIN Product ON Company.CompanyId = Product.ProductSupplierId\r\n" +
				$"WHERE Product.ProductSupplierId='"+CompanyId+"'\r\n" +
				$"ORDER BY Product.ProductId");
            var Data = dapper.Query<ProductEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public List<z_tbl_Temp_Purchase_Order> GetPurcaseorderBasket(int PurchaseId)
        {

			string Query = string.Format($"Delete from z_tbl_Temp_Purchase_Order \r\n" +
				$"INSERT INTO z_tbl_Temp_Purchase_Order \r\n( PurchaseItemId, PurchaseOrderId," +
				$" PurchaseProductId, ProductName, ProductSupplierId,\r\n   " +
				$"         ProductCategoryId, ProductPrice, Quantity," +
				$" ProductSizeOrType, TotalPrice,\r\n\t\t\t" +
				$"bln_Loaded,bln_Amount_Updated,bln_Delete ,bln_Edit)" +
				$" \r\n            " +
				$"SELECT PurchaseOrderProducts.PurchaseItemId, " +
				$"PurchaseOrderProducts.PurchaseOrderId,\r\n    " +
				$"        " +
				$"PurchaseOrderProducts.PurchaseProductId, Product.ProductName, \r\n  " +
				$"          Product.ProductSupplierId, Product.ProductCategoryId,\r\n  " +
				$"          Product.ProductPrice, " +
				$"PurchaseOrderProducts.PurchaseQuantity, \r\n " +
				$"           Product.ProductSizeOrType," +
				$" [ProductPrice]*[PurchaseQuantity] AS Total_Price, " +
				$"1 AS Flag \r\n\t\t\t,0 as bln_Amount_Updated\r\n\t\t\t," +
				$"0 as bln_Delete\r\n\t\t\t,0 as bln_Edit\r\n  " +
				$"          FROM PurchaseOrderProducts \r\n    " +
				$"        INNER JOIN Product ON PurchaseOrderProducts.PurchaseProductId = Product.ProductId \r\n  " +
				$"          WHERE PurchaseOrderProducts.PurchaseOrderId='" + PurchaseId + "' " +
			
				$" SELECT z_tbl_Temp_Purchase_Order.PurchaseItemId, " +
				$"z_tbl_Temp_Purchase_Order.PurchaseOrderId AS PID," +
				$" \r\nz_tbl_Temp_Purchase_Order.PurchaseProductId," +
				$"\r\nz_tbl_Temp_Purchase_Order.ProductName," +
				$" \r\nz_tbl_Temp_Purchase_Order.ProductPrice," +
				$" \r\nz_tbl_Temp_Purchase_Order.Quantity," +
				$" \r\nz_tbl_Temp_Purchase_Order.TotalPrice, " +
				$"\r\nz_tbl_Temp_Purchase_Order.ProductId, " +
				$"\r\nz_tbl_Temp_Purchase_Order.bln_Loaded," +
				$" \r\nz_tbl_Temp_Purchase_Order.bln_Edit," +
				$"\r\nz_tbl_Temp_Purchase_Order.bln_Delete\r\n" +
				$" FROM z_tbl_Temp_Purchase_Order " +
				$"WHERE PurchaseOrderId='" + PurchaseId + "' and bln_Delete= 0 "); 
            var Data = dapper.Query<z_tbl_Temp_Purchase_Order>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public int SavePurchaseOrderDetail(PurchaseOrderEntity obj, List<z_tbl_Temp_Purchase_Order> localBasket)
        {
            string Query = "";
           
                Query = string.Format($"UPDATE PurchaseOrder SET [PurchaseOrderDate] = '" + obj.PurchaseOrderDate + "'" +
                    $",[PurchaseRequiredDate] = '" + obj.PurchaseRequiredDate + "' " +
                    $",[PurchaseRequiredTime] = '" + obj.PurchaseRequiredTime + "'" +
                    $",[PurchaseOrderNotes] ='" + obj.PurchaseOrderNotes + "'" +
                    $",[PurchaseDeliveryAddress] = '" + obj.PurchaseDeliveryAddress + "'" +
                    $" WHERE [PurchaseId] = '" + obj.PurchaseId + "'");


            var result = dapper.Execute<int>(Query, null, null, true, null, CommandType.Text);
			var Query2 = "";
			if(localBasket !=null && localBasket.Count > 0)
			{
				var existingItem= localBasket.Where(x => x.PurchaseItemId >0 ).ToList();
				if(existingItem !=null && existingItem.Count > 0)
				{
					foreach (var item in existingItem)
					{
						Query2 = string.Format($"\r\nUPDATE PurchaseOrderProducts\r\n" +
						$"SET \r\nPurchaseOrderProducts.PurchaseQuantity = '"+item.Quantity+"'," +
                        $" \r\nPurchaseOrderProducts.PurchaseUnitPrice = '" +item.ProductPrice+"'," +
                        $" \r\nPurchaseOrderProducts.sys_User_Name = 'admin'," +
						$" \r\nPurchaseOrderProducts.sys_User_Date = GETDATE()\r\n" +
						$"WHERE PurchaseOrderProducts.PurchaseOrderId='"+obj.PurchaseId+"'\r\n" +
						$"AND PurchaseOrderProducts.PurchaseProductId ='"+item.PurchaseProductId+"'\r\n" +
						$"AND PurchaseOrderProducts.PurchaseItemId='"+item.PurchaseItemId+"'" +
                        "\r\n\t\t   Update Product\r\n\t\t  " +
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
                        Query2 = string.Format($"Declare @vItemID int \r\nSelect @vItemID=max(PurchaseItemId) + 1  from [PurchaseOrderProducts]" +
							$" INSERT INTO [dbo].[PurchaseOrderProducts]\r\n" +
							$"           (\r\n  PurchaseItemId,          [PurchaseQuantity]\r\n   " +
							$"        ,[PurchaseUnitPrice]\r\n     " +
							$"      ,[PurchaseOrderId]\r\n    " +
							$"       ,[PurchaseProductId]\r\n   " +
							$"        ,[sys_User_Name]\r\n   " +
							$"        ,[sys_User_Date])\r\n  " +
							$"   VALUES\r\n           (@vItemID, '" +item.Quantity+"','"+item.ProductPrice+"','"+obj.PurchaseId+"','"+item.PurchaseProductId+"','admin',GETDATE()) " +
                            "\r\n\t\t   Update Product\r\n\t\t " +
							"  Set ProductPrice = '"+item.ProductPrice+"'\r\n\t\t  " +
							" where ProductId = '"+item.PurchaseProductId+"'");
                        var result2 = dapper.Execute<int>(Query2, null, null, true, null, CommandType.Text);
                    }
                }
				
			}

			var Query3 = "";
			Query3 = string.Format($"If Not Exists (Select top 1 1 from tbl_Total_Sales where PurchaseOrderId = '"+obj.PurchaseId+"')\r\n" +
				$"begin\r\nInsert into tbl_Total_Sales(PurchaseOrderId,lng_Sum_PO,sys_User_Name,sys_User_Date)" +
				$"\r\nvalues('"+obj.PurchaseId+"','"+obj.TotalPurchase+"','admin',GETDATE())\r\n" +
				$"end\r\n\r\n" +
				$"else \r\n" +
				$"begin\r\n" +
				$"Update tbl_Total_Sales\r\n" +
				$"Set lng_Sum_PO = '"+obj.TotalPurchase+"'\r\n" +
				$"where PurchaseOrderId = '" +obj.PurchaseId+ "'" +
				$"\r\nend");
            var result3 = dapper.Execute<int>(Query3, null, null, true, null, CommandType.Text);
            return result;
        }
        public List<StaffList> GetStaffList()
        {

            string Query = string.Format($"SELECT Staff.StaffId, [StaffForename] + ' ' + [StaffLastname] AS Expr1\r\nFROM Staff\r\nWHERE ((([StaffForename] + ' ' + [StaffLastname])<>''))\r\nORDER BY Staff.StaffId, Staff.StaffHaveLeft DESC , Staff.StaffLastname");
            var Data = dapper.Query<StaffList>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
    }
}
