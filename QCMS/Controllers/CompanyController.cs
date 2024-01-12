using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Company;
using System.Collections.Generic;

namespace QCMS.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ICompanyRepository _companyRepository = new CompanyRepository();

        //private readonly IOptions<ConnectionClass> _options;

        public CompanyController()
        {

            //_options = options;

        }

	
		public IActionResult Index()
        {
            


            var lst = _companyRepository.GetCompanyList();
            ViewBag.cmplst = lst;

            return View();
        }
        public JsonResult CompanyGrid(string CompanyID = "", string CompanyName = "", string CompanyTown = "", string CompanyPostCode = "", string DatabaseType = "", int page = 1, string sidx = "", string sord = "", int rows = 20)
        {
            int totalPages = 0;
            int totalItems = 0;
            var lst = _companyRepository.GetCompanyList();
            if (lst != null && lst.Count > 0)
            {
                lst = lst.Where(x =>

                 ((string.IsNullOrEmpty(CompanyID)) || (x.CompanyId == CompanyID))
                && (string.IsNullOrEmpty(CompanyName) || (x.CompanyName != null && x.CompanyName.ToLower().Contains(CompanyName.ToLower())))
                && (string.IsNullOrEmpty(CompanyTown) || x.CompanyTown != null && x.CompanyTown.ToLower().Contains(CompanyTown.ToLower()))
                && (string.IsNullOrEmpty(CompanyPostCode) || x.CompanyPostCode != null && x.CompanyPostCode.ToLower().Contains(CompanyPostCode.ToLower()))
                && (string.IsNullOrEmpty(DatabaseType) || x.CompanyId != null && x.CompanyId.ToLower().StartsWith(DatabaseType.ToLower()))

                ).ToList();
                switch (sidx)
                {
                    case "companyId":
                        if (sord == "desc")
                        {
                            lst = lst.OrderByDescending(x => x.CompanyId).ToList();
                        }
                        else
                        {
                            lst = lst.OrderBy(x => x.CompanyId).ToList();
                        }
                        break;
                    case "companyName":
                        if (sord == "desc")
                        {
                            lst = lst.OrderByDescending(x => x.CompanyName).ToList();
                        }
                        else
                        {
                            lst = lst.OrderBy(x => x.CompanyName).ToList();
                        }
                        break;
                    case "companyTown":
                        if (sord == "desc")
                        {
                            lst = lst.OrderByDescending(x => x.CompanyTown).ToList();
                        }
                        else
                        {
                            lst = lst.OrderBy(x => x.CompanyTown).ToList();
                        }
                        break;
                    case "companyPostCode":
                        if (sord == "desc")
                        {
                            lst = lst.OrderByDescending(x => x.CompanyPostCode).ToList();
                        }
                        else
                        {
                            lst = lst.OrderBy(x => x.CompanyPostCode).ToList();
                        }
                        break;
                    // Add more cases for other fields to be sorted

                    default:
                        // Default sorting if no specific field is provided
                        //lst = lst.OrderBy(x => x.CompanyName).ToList();
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
        public IActionResult PromptsCallGrid(string CompanyID)
        {
            var lst = _companyRepository.GetCallsPromptsByCompany(CompanyID);
            return View(lst);
        }
        public IActionResult PeopleGrid(string CompanyID)
        {

            var lst = _companyRepository.GetPeopleByCompany(CompanyID);
            return View(lst);
        }
        public IActionResult AddNewCompany(string CompanyID)
        {
            var lst = _companyRepository.GetCompanyList().Where(x => x.CompanyId == CompanyID).FirstOrDefault();
            if (lst != null)
            {
                lst.lstsites = _companyRepository.GetCompanySites(CompanyID);
            }
            if (lst != null)
            {
                lst.lstPeople = _companyRepository.GetPeopleByCompany(CompanyID);
            }
            return View(lst);
        }
        [HttpPost]
        public JsonResult SaveCompanyInformation(CompanyEntity obj)
        {
            int result = 0;
            result = _companyRepository.SaveCompanyInformation(obj);
            return Json(result);
        }
        public JsonResult GetCompanySites(string CompanyID)
        {
            return Json(_companyRepository.GetCompanySites(CompanyID));
        }
        public IActionResult AddCompanySite(string CompanyID, string CompanyName)
        {
            var lst = _companyRepository.GetCompanySites(CompanyID);
            ViewBag.CompanyName = CompanyName;
            ViewBag.CompanyId = CompanyID;
            return View(lst);
        }
        public IActionResult SaveCompanySite(string CompanyID, string CompanyName)
        {
            int result = -1;

            var lst = _companyRepository.GetCompanySites(CompanyID);
            if (lst != null && lst.Count > 0)
            {

                var _siteexist = lst.Exists(x => (x.ContractLocation != null && x.ContractLocation.ToLower() == CompanyName.ToLower()));
                if (_siteexist)
                {

                    result = -2;
                    return Json(result);
                }

            }

            result = _companyRepository.SaveCompanySite(CompanyID, CompanyName);
            return Json(result);
        }
        public IActionResult PeopleGridHistory(string CompanyID)
        {

            var lst = _companyRepository.GetPeopleByCompany(CompanyID);
            return View(lst);
        }

        [HttpPost]
        public JsonResult SaveCompanyDataFromGrid(CompanyEntity obj, string id)
        {
            int result = 0;
            result = _companyRepository.SaveCompanyDataFromGrid(obj);
            return Json(result);
        }
    }
}
