using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProductEntity
    {
        public string CompanyName { get; set; }
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductSupplierId { get; set; }
        public string ProductSizeOrType { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCategoryId { get; set; }
        public string sys_User_Name { get; set; }
        public DateTime? sys_User_Date { get; set; }
    }
    public class z_tbl_Temp_Purchase_Order
    {
        public int PID { get; set; }
        public int PurchaseItemId { get; set; }
        public int ProductId { get; set; }
        public int PurchaseProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSupplierId { get; set; }
        public string ProductSizeOrType { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal dbl_Update_Amount { get; set; }
        public bool bln_Loaded { get; set; }
        public bool bln_Delete { get; set; }
        public bool bln_Edit { get; set; }
        public bool bln_Amount_Updated { get; set; }

    }
}
