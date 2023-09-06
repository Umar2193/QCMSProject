using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Company;
using Repository.Contract;

namespace QCMS.Controllers
{
	[Authorize]
	public class ContractController : Controller
	{

		private ICompanyRepository _companyRepository = new CompanyRepository();
		private IContractRepository _contractRepository = new ContractRepository();
		public IActionResult Index()
		{
			return View();
		}
		public JsonResult ContractGrid(string CompanyID = "", int page = 1, string sidx = "", string sord = "", int rows = 20)
		{
			int totalPages = 0;
			int totalItems = 0;
			var lst = _contractRepository.GetContractList(CompanyID);
			if (lst != null && lst.Count > 0)
			{


				switch (sidx)
				{
					case "contractNumber":
						if (sord == "desc")
						{
							lst = lst.OrderByDescending(x => x.ContractNumber).ToList();
						}
						else
						{
							lst = lst.OrderBy(x => x.ContractNumber).ToList();
						}
						break;
					case "contractLocation":
						if (sord == "desc")
						{
							lst = lst.OrderByDescending(x => x.ContractLocation).ToList();
						}
						else
						{
							lst = lst.OrderBy(x => x.ContractLocation).ToList();
						}
						break;



					default:

						break;
				}
				totalItems = lst.Count;
				totalPages = (int)Math.Ceiling(totalItems / (double)rows);
				// Ensure the page number is within the valid range
				page = Math.Max(1, Math.Min(page, totalPages));
				lst = lst.Skip((page - 1) * rows).Take(rows).ToList();
			}
			// return View(lst);
			return Json(new { page = page, total = totalPages, records = totalItems, rows = lst });
		}
		public IActionResult ContractSearchDetail(string CompanyID, string ContractNo )
		{

			ContractDTO contractDTO = new ContractDTO();
			ContractEntity contractEntity = new ContractEntity();
			var lst = _contractRepository.GetContractList(CompanyID);
			contractDTO.lstContract = lst;
			if (lst != null && lst.Count > 0)
			{
				contractEntity = lst.Where(x => x.ContractNumber == ContractNo).FirstOrDefault();
				contractDTO.contractDetailDTO.contractEntity = contractEntity;
			}
			var lstPurcase = _contractRepository.GetPurcaseorderList(ContractNo);
			contractDTO.contractDetailDTO.lstPurcaseOrder = lstPurcase;
            ViewBag.StaffList = _contractRepository.GetStaffList();
            return View(contractDTO);
		}
		public IActionResult GetContraclDetail(string CompanyID, string ContractNo)
		{
            
            ContractDetailDTO contractDetailDTO = new ContractDetailDTO();
			var lst = _contractRepository.GetContractList(CompanyID);

			if (lst != null && lst.Count > 0) {
				contractDetailDTO.contractEntity = lst.Where(x => x.ContractNumber == ContractNo).FirstOrDefault();
			}
			var lstPurcase = _contractRepository.GetPurcaseorderList(ContractNo);
			contractDetailDTO.lstPurcaseOrder = lstPurcase;
			ViewBag.StaffList = _contractRepository.GetStaffList();
			return View("ContractDetail", contractDetailDTO);
		}
		public IActionResult GetPurchaseOrderDetail(string CompanyID, string ContractNo, int PurchaseId)
		{
            var complst = _companyRepository.GetCompanyList().Where(x => x.CompanyIsSupplier == true && x.CompanyName != null).OrderBy(x => x.CompanyName).ToList();
            ViewBag.cmplst = complst;
            PurchaseOrderDTO purchaseOrderDTO = new PurchaseOrderDTO();
			var lst = _contractRepository.GetCompanyProductsList(CompanyID);

			if (lst != null && lst.Count > 0)
			{
				purchaseOrderDTO.lstProductEntities = lst;
			}


			purchaseOrderDTO.purchaseOrderEntity = _contractRepository.GetPurcaseorderDetail(ContractNo,PurchaseId);
			purchaseOrderDTO.Lstz_Tbl_Temp_Purchase_Orders = _contractRepository.GetPurcaseorderBasket(PurchaseId);
			return View(purchaseOrderDTO);
		}
		[HttpPost]
        public IActionResult SavePurhcaseOrderDetail(PurchaseOrderEntity purchaseorderDetail, List<z_tbl_Temp_Purchase_Order> localBasket)
        {

			var result = 0;
			result = _contractRepository.SavePurchaseOrderDetail(purchaseorderDetail, localBasket);
		    return	Json(result);
        }
    }
}
