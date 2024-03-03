using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ActiveDirectoryFileManagement.Helpers;
internal class Impersonation
{
	[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);

	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(SafeAccessTokenHandle hObject);
}
