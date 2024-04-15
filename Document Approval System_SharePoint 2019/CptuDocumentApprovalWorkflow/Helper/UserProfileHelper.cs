using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL_FarmSolution.Helper
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
                        value = String.Format("{0} {1}", profile[propertyConstant.Split(' ')[0]], profile[propertyConstant.Split(' ')[1]]);
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

            if (!claimUsername.Contains("i:0#.w|"))
            {
                claimUsername = String.Format("i:0#.w|{0}", claimUsername);
            }

            return claimUsername;
        }

        public static string GetUserInformation(string login, string property)
        {
            SPList UserInfoList = SPContext.Current.Web.SiteUserInfoList;
            SPUser user = SPContext.Current.Web.EnsureUser(login);
            SPItem userInfo = UserInfoList.GetItemById(user.ID);
            return userInfo[property].ToString();
        }
    }
}
