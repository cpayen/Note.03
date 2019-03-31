using Note.Core.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Entities
{
    public static class AppUserExtensions
    {
        public static string GetFullName(this AppUser appUser)
        {
            if(!string.IsNullOrEmpty(appUser.FirstName) && !string.IsNullOrEmpty(appUser.LastName))
            {
                return $"{appUser.FirstName} {appUser.LastName}";
            }

            return appUser.UserName;
        }

        public static AppUser SetPassword(this AppUser appUser, string password)
        {
            var salt = Security.GetNewSalt();
            var hashedPassword = Security.EncryptPassword(password, salt);
            appUser.Salt = salt;
            appUser.Password = hashedPassword;

            return appUser;
        }

        public static bool CheckPassword(this AppUser appUser, string password)
        {
            return Security.CheckPassword(password, appUser.Salt, appUser.Password);
        }

        public static bool HasRole(this AppUser appUser, string role)
        {
            return appUser.GetRoles().Contains(role);
        }

        public static AppUser SetRoles(this AppUser appUser, IEnumerable<string> roles)
        {
            appUser.Roles = string.Join(",", roles);
            return appUser;
        }

        public static IEnumerable<string> GetRoles(this AppUser appUser)
        {
            return appUser.Roles.Split(',');
        }
    }
}
