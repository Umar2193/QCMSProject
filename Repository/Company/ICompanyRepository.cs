using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Company
{
    public interface ICompanyRepository
    {
        List<CompanyEntity> GetCompanyList();
        List<PeopleEntity> GetPeopleByCompany(string CompanyID);
        List<CallsPromptsEntity> GetCallsPromptsByCompany(string CompanyID);
        List<Location> GetCompanySites(string CompanyID);
        int SaveCompanySite(string CompanyID, string CompanyName);
        int SaveCompanyInformation(CompanyEntity obj);
		int SaveCompanyDataFromGrid(CompanyEntity obj);
		bool ValidateLogin(string username, string password);
	}
}
