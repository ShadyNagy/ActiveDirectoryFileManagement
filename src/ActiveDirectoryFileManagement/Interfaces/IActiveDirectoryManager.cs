using System.DirectoryServices;
using ActiveDirectoryFileManagement.Models;

namespace ActiveDirectoryFileManagement.Interfaces;

/// <summary>
/// Defines the contract for managing users in Active Directory, including retrieving and updating user details.
/// </summary>
public interface IActiveDirectoryManager
{
	/// <summary>
	/// Retrieves a DirectoryEntry object for a user based on their SAM account name.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user to retrieve.</param>
	/// <returns>A DirectoryEntry object representing the user if found; otherwise, null.</returns>
	DirectoryEntry? GetUser(string samAccountName);

	/// <summary>
	/// Updates the details of a user in Active Directory.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user to update.</param>
	/// <param name="userDetails">An ActiveDirectoryUserDetails object containing the details to update.</param>
	void UpdateUserDetails(string samAccountName, ActiveDirectoryUserDetails userDetails);

	/// <summary>
	/// Retrieves detailed information about a user from Active Directory.
	/// </summary>
	/// <param name="samAccountName">The SAM account name of the user whose details are to be retrieved.</param>
	/// <returns>An ActiveDirectoryUserDetails object containing the user's details.</returns>
	ActiveDirectoryUserDetails GetUserDetails(string samAccountName);
}
