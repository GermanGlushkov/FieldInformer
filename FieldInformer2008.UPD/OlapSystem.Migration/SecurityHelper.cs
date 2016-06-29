using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace OlapSystem.Migration
{
    public class SecurityHelper
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(
        String lpszUsername,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);
        private static IntPtr tokenHandle = new IntPtr(0);
        private static WindowsImpersonationContext impersonatedUser;

        // If you incorporate this code into a DLL, be sure to demand that it runs with FullTrust.
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public static void Impersonate(string userDomainName, string password)
        {
            string[] s = userDomainName.Split(new char[] { '\\' }, 2, StringSplitOptions.RemoveEmptyEntries);
            Impersonate(
                (s.Length == 1 ? "" : s[0]),
                (s.Length == 1 ? s[0] : s[1]),
                password);
        }

        // If you incorporate this code into a DLL, be sure to demand that it runs with FullTrust.
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public static void Impersonate(string domainName, string userName, string password)
        {
            if (domainName == "")
                domainName = null;

            //try
            {
                // Use the unmanaged LogonUser function to get the user token for
                // the specified user, domain, and password.
                const int LOGON32_PROVIDER_DEFAULT = 0;
                // Passing this parameter causes LogonUser to create a primary token.
                const int LOGON32_LOGON_INTERACTIVE = 2;
                const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
                tokenHandle = IntPtr.Zero;
                // ---- Step - 1
                // Call LogonUser to obtain a handle to an access token.
                bool returnValue = LogonUser(
                userName,
                domainName,
                password,
                LOGON32_LOGON_NEW_CREDENTIALS,
                LOGON32_PROVIDER_DEFAULT,
                ref tokenHandle); // tokenHandle - new security token
                if (false == returnValue)
                {
                    int ret = Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(ret);
                }
                // ---- Step - 2
                WindowsIdentity newId = new WindowsIdentity(tokenHandle);
                // ---- Step - 3
                impersonatedUser = newId.Impersonate();
            }
        }
        // Stops impersonation
        public static void EndImpersonate()
        {
            if (impersonatedUser != null)
                impersonatedUser.Undo();

            // Free the tokens.
            if (tokenHandle != IntPtr.Zero)
                CloseHandle(tokenHandle);
        }
    }
}
