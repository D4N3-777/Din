﻿using Ldap;

namespace Logic
{
    public static class AccountManagment
    {
        private static readonly LdapManager LdapManager = new LdapManager();

        public static bool ChangePassword(string username, string newPassword1, string newPassword2)
        {
            return newPassword1 == newPassword2 && LdapManager.ChangePassword(username, newPassword1);
        }

    }
}