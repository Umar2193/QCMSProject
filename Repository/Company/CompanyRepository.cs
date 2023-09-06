using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        DAL.Company.Company company = new DAL.Company.Company();
        public List<CompanyEntity> GetCompanyList() {
            try { 
           
            return company.GetCompanyList();
            }
            catch {

                return null;
            }
        }
        public List<PeopleEntity> GetPeopleByCompany(string CompanyID)
        {
            try
            {
               
                return company.GetPeopleByCompany(CompanyID);
            }
            catch
            {

                return null;
            }
        }
        public List<CallsPromptsEntity> GetCallsPromptsByCompany(string CompanyID)
        {
            try
            {
               
                return company.GetCallsPromptsByCompany(CompanyID);
            }
            catch
            {

                return null;
            }
        }
        public List<Location> GetCompanySites(string CompanyID)
        {
            try
            {

                return company.GetCompanySites(CompanyID);
            }
            catch
            {

                return null;
            }
        }
        public int SaveCompanySite(string CompanyID, string CompanyName)
        {
            try
            {

                return company.SaveCompanySite(CompanyID,CompanyName);
            }
            catch
            {

                return -1;
            }
        }
        public int SaveCompanyInformation(CompanyEntity obj)
        {
            try
            {

                return company.SaveCompanyInformation(obj);
            }
            catch
            {

                return -1;
            }
        }
        public int SaveCompanyDataFromGrid(CompanyEntity obj)
        {
            try
            {

                return company.SaveCompanyDataFromGrid(obj);
            }
            catch
            {

                return -1;
            }
        }
		public bool ValidateLogin(string username, string password)
		{
			try
			{

				return company.ValidateLogin(username,password);
			}
			catch
			{

				return false;
			}
		}   
	}
}
