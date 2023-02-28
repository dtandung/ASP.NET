using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021044.DataLayers;
using _19T1021044.DomainModels;

namespace _19T1021044.BusinessLayers
{
    /// <summary>
    /// các chức năng liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;
        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTypes"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static UserAccount Authorize(AccountTypes accountTypes, string userName, string passWord)
        {
            if(accountTypes == AccountTypes.Employee)
                return employeeAccountDB.Authorize(userName, passWord);
            else
                return customerAccountDB.Authorize(userName, passWord);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountTypes"></param>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ChangePassword(AccountTypes accountTypes, string userName, string oldPassword, string newPassword)
        {
            if (accountTypes == AccountTypes.Employee)
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            else
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }
    }
}
