using ActiveDirectoryFileManagement.Interfaces;

namespace ActiveDirectoryFileManagement.Services;

/// <summary>
/// Provides services for managing directories, including creation, deletion, and existence checks,
/// with the ability to perform actions under the context of an Active Directory user.
/// </summary>
public class DirectoryService : IDirectoryService
{
	private readonly IActiveDirectoryService _activeDirectoryService;

	/// <summary>
	/// Initializes a new instance of the DirectoryService class.
	/// </summary>
	/// <param name="activeDirectoryService">The Active Directory service to use for impersonation.</param>
	public DirectoryService(IActiveDirectoryService activeDirectoryService)
	{
		_activeDirectoryService = activeDirectoryService;
	}

	/// <summary>
	/// Creates a directory at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the directory will be created.</param>
	public void CreateUnderUser(string path)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			Directory.CreateDirectory(path);
		});
	}

	/// <summary>
	/// Creates a directory at the specified path.
	/// </summary>
	/// <param name="path">The path where the directory will be created.</param>
	public void Create(string path)
	{
		Directory.CreateDirectory(path);
	}

	/// <summary>
	/// Checks if a directory exists at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the directory to check.</param>
	/// <returns>true if the directory exists; otherwise, false.</returns>
	public bool IsExistsUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			return Directory.Exists(path);
		});
	}

	/// <summary>
	/// Checks if a directory exists at the specified path.
	/// </summary>
	/// <param name="path">The path of the directory to check.</param>
	/// <returns>true if the directory exists; otherwise, false.</returns>
	public bool IsExists(string path)
	{
		return Directory.Exists(path);
	}

	/// <summary>
	/// Deletes a directory at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the directory to delete.</param>
	public void DeleteUnderUser(string path)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() => { Directory.Delete(path, true); });
	}

	/// <summary>
	/// Deletes a directory at the specified path.
	/// </summary>
	/// <param name="path">The path of the directory to delete.</param>
	public void Delete(string path)
	{
		Directory.Delete(path, true);
	}


	/// <summary>
	/// Retrieves the files from the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	public IEnumerable<string> GetFilesUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => Directory.GetFiles(path));
	}

	/// <summary>
	/// Retrieves the files from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	public IEnumerable<string> GetFiles(string path)
	{
		return Directory.GetFiles(path);
	}

	/// <summary>
	/// Retrieves the files from the specified path under the context of an Active Directory user,
	/// optionally filtering by extensions.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <param name="extensions">An array of file extensions to filter the files. If null or empty, all files are returned.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	public IEnumerable<string> GetFilesUnderUser(string path, string[] extensions)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => GetFilesByExtensions(path, extensions));
	}

	/// <summary>
	/// Retrieves the files from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search.</param>
	/// <param name="extensions">An array of file extensions to filter the files. If null or empty, all files are returned.</param>
	/// <returns>An enumerable collection of file paths.</returns>
	public IEnumerable<string> GetFiles(string path, string[] extensions)
	{
		return GetFilesByExtensions(path, extensions);
	}

	/// <summary>
	/// Retrieves the directories from the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The directory path to search for subdirectories.</param>
	/// <returns>An enumerable collection of directory paths found at the specified path.</returns>
	public IEnumerable<string> GetDirectoriesUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => Directory.GetDirectories(path));
	}

	/// <summary>
	/// Retrieves the directories from the specified path.
	/// </summary>
	/// <param name="path">The directory path to search for subdirectories.</param>
	/// <returns>An enumerable collection of directory paths found at the specified path.</returns>
	public IEnumerable<string> GetDirectories(string path)
	{
		return Directory.GetDirectories(path);
	}

	private IEnumerable<string> GetFilesByExtensions(string path, string[] extensions)
	{
		var extensionSet = new HashSet<string>(extensions.Select(ext => "." + ext), StringComparer.OrdinalIgnoreCase);
		return Directory.EnumerateFiles(path)
										.Where(file => extensionSet.Contains(Path.GetExtension(file)));
	}
}
