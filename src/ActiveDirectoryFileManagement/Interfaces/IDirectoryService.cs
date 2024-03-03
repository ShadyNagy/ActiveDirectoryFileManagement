namespace ActiveDirectoryFileManagement.Interfaces;

/// <summary>
/// Defines a set of methods for directory operations, including creation, deletion,
/// existence checks, and retrieval of files and directories, with support for
/// operations under a user context, potentially utilizing Active Directory for
/// impersonation.
/// </summary>
public interface IDirectoryService
{
	/// <summary>
	/// Creates a directory at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the directory will be created.</param>
	void CreateUnderUser(string path);

	/// <summary>
	/// Creates a directory at the specified path.
	/// </summary>
	/// <param name="path">The path where the directory will be created.</param>
	void Create(string path);

	/// <summary>
	/// Checks if a directory exists at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the directory to check.</param>
	/// <returns>true if the directory exists; otherwise, false.</returns>
	bool IsExistsUnderUser(string path);

	/// <summary>
	/// Checks if a directory exists at the specified path.
	/// </summary>
	/// <param name="path">The path of the directory to check.</param>
	/// <returns>true if the directory exists; otherwise, false.</returns>
	bool IsExists(string path);

	/// <summary>
	/// Deletes a directory at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the directory to delete.</param>
	void DeleteUnderUser(string path);

	/// <summary>
	/// Deletes a directory at the specified path.
	/// </summary>
	/// <param name="path">The path of the directory to delete.</param>
	void Delete(string path);

	/// <summary>
	/// Retrieves the files from the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	IEnumerable<string> GetFilesUnderUser(string path);

	/// <summary>
	/// Retrieves the files from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	IEnumerable<string> GetFiles(string path);

	/// <summary>
	/// Retrieves the files from the specified path under the context of an Active Directory user,
	/// optionally filtering by extensions.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <param name="extensions">An array of file extensions to filter the files. If null or empty, all files are returned.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	IEnumerable<string> GetFilesUnderUser(string path, string[] extensions);

	/// <summary>
	/// Retrieves the files from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <param name="extensions">An array of file extensions to filter the files. If null or empty, all files are returned.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	IEnumerable<string> GetFiles(string path, string[] extensions);

	/// <summary>
	/// Retrieves the directories from the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The directory path to search for subdirectories.</param>
	/// <returns>An enumerable collection of directory paths found at the specified path.</returns>
	IEnumerable<string> GetDirectoriesUnderUser(string path);

	/// <summary>
	/// Retrieves the directories from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search for subdirectories.</param>
	/// <returns>An enumerable collection of directory paths found at the specified path.</returns>
	IEnumerable<string> GetDirectories(string path);
}
