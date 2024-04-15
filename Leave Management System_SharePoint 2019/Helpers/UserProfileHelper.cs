using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementCPTU.Helpers
{
    public class UserProfileHelper
    {
        public static string GetPropertyValue(string loginName, string propertyConstant)
        {
            string value = "";
            if (!String.IsNullOrEmpty(loginName))
            {
                loginName = ConvertUsernameToClaim(loginName);

                try
                {
                    UserProfileManager profileManager = new UserProfileManager();
                    UserProfile profile = profileManager.GetUserProfile(loginName);
                    if (propertyConstant.Contains(" "))
                    {
                        value = String.Format("{0} {1}",
                            GetPropertyValue(loginName, propertyConstant.Split(' ')[0]),
                            GetPropertyValue(loginName, propertyConstant.Split(' ')[1]));
                    }
                    else
                        value = profile[propertyConstant].Value.ToString();
                }
                catch (Exception) { }
            }
            return value;
        }

        public static string ConvertUsernameToClaim(string loginName)
        {
            string claimUsername = loginName;

            if (claimUsername.Contains('\\'))
                claimUsername = claimUsername.Substring(claimUsername.IndexOf('\\') + 1);

            claimUsername = String.Format("i:0#.w|cptu\\{0}", claimUsername);

            return claimUsername;
        }

        public static string FormatLoginNameAsPlainText(string loginName)
        {
            ////Sample Input=> i:0#.w|xyz\testuser1
            //// Sample Output=> testuser1 
            try
            {
                string simpleLoginName = loginName.Substring(loginName.IndexOf('\\') + 1);
                return simpleLoginName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserEmailByUserId(int userID)
        {
            return SPContext.Current.Web.SiteUsers.GetByID(userID).Email;
        }

        public static int GetUserIdByEmail(string email)
        {
            return SPContext.Current.Web.SiteUsers.GetByEmail(email).ID;
        }

        public static string GetUserLoginNameById(int userID)
        {
            return SPContext.Current.Web.SiteUsers.GetByID(userID).LoginName;
        }

        public string GetUserLoginNameByEmail(string email)
        {
            return SPContext.Current.Web.SiteUsers.GetByEmail(email).LoginName;
        }

        public string GetUserNameById(int userID)
        {
            return SPContext.Current.Web.SiteUsers.GetByID(userID).Name;
        }

    }

    public class UserProfileConstants
    {
        public const string DisplayName = PropertyConstants.PreferredName;
        public const string FirstName = PropertyConstants.FirstName;
        public const string LastName = PropertyConstants.LastName;
        public static string FullName
        {
            get
            {
                return String.Format("{0} {1}", PropertyConstants.FirstName, PropertyConstants.LastName);
            }
        }
        public const string WorkEmail = PropertyConstants.WorkEmail;
        public const string Department = PropertyConstants.Department;
        public const string Designation = PropertyConstants.Title;
        public const string Phone = PropertyConstants.Office;
        public const string Pager = "Pager";
        public const string ManagerLoginName = PropertyConstants.Manager;
        public const string UserName = PropertyConstants.AccountName;

    }

}
