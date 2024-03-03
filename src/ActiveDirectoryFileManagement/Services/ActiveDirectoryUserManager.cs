using System.DirectoryServices;
using ActiveDirectoryFileManagement.Interfaces;
using ActiveDirectoryFileManagement.Models;

namespace ActiveDirectoryFileManagement.Services;

/// <summary>
/// Manages users in Active Directory, including retrieving and updating user details.
/// </summary>
public class ActiveDirectoryUserManager : IActiveDirectoryUserManager
{
	private readonly ActiveDirectorySettings _activeDirectorySettings;
	private readonly string _ldapPath = string.Empty;

	public ActiveDirectoryUserManager(ActiveDirectorySettings activeDirectorySettings)
	{
		_activeDirectorySettings = activeDirectorySettings;
		var domainParts = _activeDirectorySettings.Domain.Split(".");
		_ldapPath = domainParts.Length switch
		{
			> 1 => $"LDAP://DC={string.Join(",DC=", domainParts)}",
			_ => $"LDAP://"
		};
	}

	/// <summary>
	/// Retrieves a DirectoryEntry object for a user based on their SAM account name.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user to retrieve.</param>
	/// <returns>A DirectoryEntry object representing the user if found; otherwise, null.</returns>
	public DirectoryEntry? GetUser(string samAccountName)
	{
		try
		{
			using var directoryEntry = new DirectoryEntry(_ldapPath, _activeDirectorySettings.Username, _activeDirectorySettings.Password);
			var directorySearcher = new DirectorySearcher(directoryEntry)
			{
				Filter = $"(&(objectClass=user)(sAMAccountName={samAccountName}))"
			};

			var searchResult = directorySearcher.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			var userToUpdate = searchResult.GetDirectoryEntry();

			return userToUpdate;
		}
		catch (Exception)
		{
			return null;
		}
	}

	/// <summary>
	/// Updates the details of a user in Active Directory.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user to update.</param>
	/// <param name="userDetails">An ActiveDirectoryUserDetails object containing the details to update.</param>
	public void UpdateUserDetails(string samAccountName, ActiveDirectoryUserDetails userDetails)
	{
		try
		{
			using var directoryEntry = new DirectoryEntry(_ldapPath, _activeDirectorySettings.Username, _activeDirectorySettings.Password);
			var directorySearcher = new DirectorySearcher(directoryEntry)
			{
				Filter = $"(&(objectClass=user)(sAMAccountName={samAccountName}))"
			};

			var searchResult = directorySearcher.FindOne();

			if (searchResult != null)
			{
				UpdateUserProperties(searchResult, userDetails);
			}
		}
		catch (Exception)
		{
		}
	}

	/// <summary>
	/// Retrieves detailed information about a user from Active Directory.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user whose details are to be retrieved.</param>
	/// <returns>An ActiveDirectoryUserDetails object containing the user's details.</returns>
	public ActiveDirectoryUserDetails GetUserDetails(string samAccountName)
	{
		var activeDirectoryUserDetails = new ActiveDirectoryUserDetails();

		try
		{
			using var directoryEntry = new DirectoryEntry(_ldapPath, _activeDirectorySettings.Username, _activeDirectorySettings.Password);
			var directorySearcher = new DirectorySearcher(directoryEntry)
			{
				Filter = $"(&(objectClass=user)(sAMAccountName={samAccountName}))"
			};

			var searchResult = directorySearcher.FindOne();

			if (searchResult != null)
			{
				var user = searchResult.GetDirectoryEntry();
				foreach (PropertyValueCollection propertyValue in user.Properties)
				{
					var propertyName = propertyValue.PropertyName;
					if (propertyValue.Count > 1)
					{
						var multiValues = string.Join("; ", propertyValue.Cast<object>().Select(v => v.ToString()));
						activeDirectoryUserDetails.AddDetail(propertyName, multiValues);
					}
					else
					{
						var singleValue = propertyValue.Value?.ToString() ?? string.Empty;
						activeDirectoryUserDetails.AddDetail(propertyName, singleValue);
					}
				}
			}

			return activeDirectoryUserDetails;
		}
		catch (Exception)
		{
			return activeDirectoryUserDetails;
		}
	}


	private void UpdateUserProperties(SearchResult searchResult, ActiveDirectoryUserDetails userDetails)
	{
		var userToUpdate = searchResult.GetDirectoryEntry();

		bool changesMade = false;

		foreach(var userDetail in userDetails.UserDetails)
		{
			userToUpdate.Properties[userDetail.Key].Value = userDetail.Value;
			changesMade = true;
		}

		if (changesMade)
		{
			userToUpdate.CommitChanges();
		}
	}
}
