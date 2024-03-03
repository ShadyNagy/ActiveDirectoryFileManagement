using System.Runtime.InteropServices;
using System.Security.Principal;
using ActiveDirectoryFileManagement.Helpers;
using ActiveDirectoryFileManagement.Interfaces;
using ActiveDirectoryFileManagement.Models;
using Microsoft.Win32.SafeHandles;

namespace ActiveDirectoryFileManagement.Services;

public class ActiveDirectoryService : IActiveDirectoryService
{
	private readonly ActiveDirectorySettings _activeDirectorySettings;

	private const int LOGON32_LOGON_INTERACTIVE = 2;
	private const int LOGON32_PROVIDER_DEFAULT = 0;

	public ActiveDirectoryService(ActiveDirectorySettings activeDirectorySettings)
	{
		_activeDirectorySettings = activeDirectorySettings;
	}

	public TResult ImpersonateUserAndRunAction<TResult>(Func<TResult> action)
	{
		var userHandle = Login();

		try
		{
			TResult result = WindowsIdentity.RunImpersonated(userHandle, action);
			return result;
		}
		finally
		{
			Impersonation.CloseHandle(userHandle);
		}
	}

	public void ImpersonateUserAndRunAction(Action action)
	{
		var userHandle = Login();

		try
		{
			WindowsIdentity.RunImpersonated(userHandle, () => { action(); });
		}
		finally
		{
			Impersonation.CloseHandle(userHandle);
		}
	}

	private SafeAccessTokenHandle Login()
	{
		bool success = Impersonation.LogonUser(_activeDirectorySettings.Username, _activeDirectorySettings.Domain, _activeDirectorySettings.Password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out SafeAccessTokenHandle userHandle);
		if (!success)
		{
			throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
		}

		return userHandle;
	}
}
