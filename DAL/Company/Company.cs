using Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Company
{
    public class Company
    {
          DapperManager dapper;
        private static IConfiguration _configuration;
        public Company()
        {
            dapper = new DapperManager();
        }
        public  List<CompanyEntity> GetCompanyList()
        {

            string Query = string.Format($"Select *  from company");
            var Data = dapper.Query<CompanyEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public  List<PeopleEntity> GetPeopleByCompany(string CompanyID)
        {

            string Query = string.Format($"Select *  from people where PeopleCompanyId ='"+ CompanyID+"'");
            var Data = dapper.Query<PeopleEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public List<CallsPromptsEntity> GetCallsPromptsByCompany(string CompanyID)
        {

            string Query = string.Format($"SELECT  CallID,CallCompanyID],CallPeopleID,Call When as CallWhen,CallStaff,CallNotes,cp.sys_User_Name,cp.sys_User_Date " +
                $",pe.PeopleForename + ' ' + PeopleLastname as PeopleName ,CONCAT('',st.StaffForename , ' ' , st.StaffLastname) as StaffName" +
                $"  FROM callsprompts cp left Join People pe on cp.CallPeopleID = pe.PeopleId  " +
                $"left join staff st on cp.CallStaff = st.StaffId where cp.CallCompanyID = '" + CompanyID + "'");
            var Data = dapper.Query<CallsPromptsEntity>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public List<Location> GetCompanySites(string CompanyID)
        {

            string Query = string.Format($"Select *  from location where CompanyId = '" + CompanyID + "'");
            var Data = dapper.Query<Location>(Query, null, null, true, null, CommandType.Text);
            return Data.ToList();
        }
        public int SaveCompanySite(string CompanyID, string CompanyName)
        {

            string Query = string.Format($"INSERT INTO location (CompanyId ,ContractLocation,Location_ID,sys_User_Name,sys_User_Date) " +
                $"Values('"+CompanyID+"','"+CompanyName+"','0','Admin','"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            var result = dapper.Execute<int>(Query, null, null, true, null, CommandType.Text);
            return result;
        }
        public int SaveCompanyInformation(CompanyEntity obj)
        {
            string Query = "";
            if (!string.IsNullOrEmpty(obj.CompanyId))
            {
                Query = string.Format($"UPDATE company SET CompanyId = '"+obj.CompanyId +"'" +
                    $",CompanyIsCustomer = "+obj.CompanyIsCustomer+" " +
                    $",CompanyIsSupplier = "+obj.CompanyIsSupplier+"" +
                    $",CompanyIsSubcontractor ="+obj.CompanyIsSubcontractor+"" +
                    $",CompanyIsEnquirer = "+obj.CompanyIsEnquirer+"" +
                    $",CompanyIsContracts = "+obj.CompanyIsContracts+"" +
                    $",CompanyShortCode = '"+obj.CompanyShortCode+"'" +
                    $",CompanyName = '"+obj.CompanyName+"'" +
                    $",CompanyAddressLines = '"+obj.CompanyAddressLines+"'" +
                    $",CompanyTown = '"+obj.CompanyTown+"'" +
                    $",CompanyCounty = '"+obj.CompanyCounty+"'" +
                    $",CompanyPostCode = '"+obj.CompanyPostCode+"'" +
                    $",CompanyMainTelephone = '"+obj.CompanyMainTelephone+"'" +
                    $",CompanyMainFax = '"+obj.CompanyMainFax+"'" +
                    $",CompanyAccountNo = '"+obj.CompanyAccountNo+"'" +
                    $",CompanyApproved = "+obj.CompanyApproved+"" +
                    $",CompanyCreditDays = '"+obj.CompanyCreditDays+"'" +
                    $",CompanyMentorRef = '"+obj.CompanyMentorRef+"'" +
                    $" WHERE CompanyId = '"+obj.CompanyId+"'");
            }
            else
            {
                Query = string.Format($"INSERT INTO company (CompanyId ,CompanyIsCustomer,CompanyIsSupplier,CompanyIsSubcontractor ,CompanyIsEnquirer,CompanyIsContracts,CompanyShortCode,CompanyName,CompanyAddressLines ,CompanyTown ,CompanyCounty,CompanyPostCode ,CompanyMainTelephone,CompanyMainFax ,CompanyAccountNo ,CompanyApproved ,CompanyCreditDays  ,CompanyMentorRef ,sys_User_Name,sys_User_Date)" +
                    $" VALUES('"+obj.CompanyId+"',"+obj.CompanyIsCustomer+"" +
                    $","+obj.CompanyIsSupplier+" ,"+obj.CompanyIsSubcontractor+"" +
                    $","+obj.CompanyIsEnquirer+" ,"+obj.CompanyIsContracts+"" +
                    $",'"+obj.CompanyShortCode+"' ,'"+obj.CompanyName+"'" +
                    $",'"+obj.CompanyAddressLines+"'" +
                    $",'"+obj.CompanyTown+"','"+obj.CompanyCounty+"','"+obj.CompanyPostCode+"'" +
                    $" ,'"+obj.CompanyMainTelephone+"','"+obj.CompanyMainFax+"'" +
                    $" ,'"+obj.CompanyAccountNo+"'" +
                    $" ,"+obj.CompanyApproved+",'"+obj.CompanyCreditDays+"'" +
                    $" ,'"+obj.CompanyMentorRef+"','"+obj.sys_User_Name+"'" +
                    $" ,'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')"); 

            }
            var result = dapper.Execute<int>(Query, null, null, true, null, CommandType.Text);
            return result;
        }
        public int SaveCompanyDataFromGrid(CompanyEntity obj)
        {
           
              string  Query = string.Format($"UPDATE company  "  +
                    $"set CompanyIsCustomer = '" + obj.CompanyIsCustomer + "' " +
                    $",CompanyIsSupplier = '" + obj.CompanyIsSupplier + "'" +
                    $",CompanyIsSubcontractor ='" + obj.CompanyIsSubcontractor + "'" +
                    $",CompanyIsEnquirer = '" + obj.CompanyIsEnquirer + "'" +
                    $",CompanyName = '" + obj.CompanyName + "'" +
                    $",CompanyTown = '" + obj.CompanyTown + "'" +
                    $",CompanyPostCode = '" + obj.CompanyPostCode + "'" +
                    $" WHERE CompanyId = '" + obj.CompanyId + "'");
            
         
            var result = dapper.Execute<int>(Query, null, null, true, null, CommandType.Text);
            return result;
        }
		public bool ValidateLogin(string username,string password)
		{

            string Query = string.Format($"Select *  from sysUserPW where strUserName ='" + username + "' and strPassword = '" + password + "'");
            var Data = dapper.Query<PeopleEntity>(Query, null, null, true, null, CommandType.Text);
			if( Data != null)
            {
                var lst= Data.ToList();
                if(lst.Count > 0)
                {
                    return true;
                }
            }
            return false;
		}
	}
}
