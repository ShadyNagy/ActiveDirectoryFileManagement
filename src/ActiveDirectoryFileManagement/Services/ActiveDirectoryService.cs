using System.Runtime.InteropServices;
using System.Security.Principal;
using ActiveDirectoryFileManagement.Helpers;
using ActiveDirectoryFileManagement.Interfaces;
using ActiveDirectoryFileManagement.Models;
using Microsoft.Win32.SafeHandles;

namespace ActiveDirectoryFileManagement.Services;

/// <summary>
/// Service for executing actions under the context of an impersonated Active Directory user.
/// </summary>
public class ActiveDirectoryService : IActiveDirectoryService
{
	private readonly ActiveDirectorySettings _activeDirectorySettings;

	private const int LOGON32_LOGON_INTERACTIVE = 2;
	private const int LOGON32_PROVIDER_DEFAULT = 0;

	/// <summary>
	/// Initializes a new instance of the <see cref="ActiveDirectoryService"/> class.
	/// </summary>
	/// <param name="activeDirectorySettings">Configuration settings for connecting to Active Directory.</param>
	public ActiveDirectoryService(ActiveDirectorySettings activeDirectorySettings)
	{
		_activeDirectorySettings = activeDirectorySettings;
	}

	/// <summary>
	/// Executes a function under the context of an impersonated Active Directory user and returns the result.
	/// </summary>
	/// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
	/// <param name="action">The function to execute, which returns a result of type <typeparamref name="TResult"/>.</param>
	/// <returns>The result of the function executed under the impersonated user context.</returns>
	/// <exception cref="System.ComponentModel.Win32Exception">Thrown when there is an error logging in the user.</exception>
	public TResult ImpersonateUserAndRunAction<TResult>(Func<TResult> action)
	{
		var userHandle = Login();

		try
		{
			return WindowsIdentity.RunImpersonated(userHandle, action);
		}
		finally
		{
			Impersonation.CloseHandle(userHandle);
		}
	}

	/// <summary>
	/// Executes an action under the context of an impersonated Active Directory user.
	/// </summary>
	/// <param name="action">The action to execute under the impersonated user context.</param>
	/// <exception cref="System.ComponentModel.Win32Exception">Thrown when there is an error logging in the user.</exception>
	public void ImpersonateUserAndRunAction(Action action)
	{
		var userHandle = Login();

		try
		{
			WindowsIdentity.RunImpersonated(userHandle, () => action());
		}
		finally
		{
			Impersonation.CloseHandle(userHandle);
		}
	}

	/// <summary>
	/// Logs in the Active Directory user based on the provided settings and returns the user token handle.
	/// </summary>
	/// <returns>A safe token handle for the impersonated user.</returns>
	/// <exception cref="System.ComponentModel.Win32Exception">Thrown when there is an error during the login process.</exception>
	private SafeAccessTokenHandle Login()
	{
		bool success = Impersonation.LogonUser(
				_activeDirectorySettings.Username,
				_activeDirectorySettings.Domain,
				_activeDirectorySettings.Password,
				LOGON32_LOGON_INTERACTIVE,
				LOGON32_PROVIDER_DEFAULT,
				out SafeAccessTokenHandle userHandle);

		if (!success)
		{
			throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
		}

		return userHandle;
	}
}
