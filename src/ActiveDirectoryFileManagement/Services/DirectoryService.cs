using ActiveDirectoryFileManagement.Interfaces;

namespace ActiveDirectoryFileManagement.Services;

public class DirectoryService : IDirectoryService
{
	private readonly IActiveDirectoryService _activeDirectoryService;

	public DirectoryService(IActiveDirectoryService activeDirectoryService)
	{
		_activeDirectoryService = activeDirectoryService;
	}

	public void CreateUnderUser(string path)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			Directory.CreateDirectory(path);
		});
	}

	public void Create(string path)
	{
		Directory.CreateDirectory(path);
	}

	public bool IsExistsUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			return Directory.Exists(path);
		});
	}

	public bool IsExists(string path)
	{
		return Directory.Exists(path);
	}

	public void DeleteUnderUser(string path)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() => { Directory.Delete(path, true); });
	}

	public void Delete(string path)
	{
		Directory.Delete(path, true);
	}

	public IEnumerable<string> GetFilesUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => Directory.GetFiles(path));
	}

	public IEnumerable<string> GetFiles(string path)
	{
		return Directory.GetFiles(path);
	}

	public IEnumerable<string> GetFilesUnderUser(string path, string[] extensions)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => GetFilesByExtensions(path, extensions));
	}

	public IEnumerable<string> GetFiles(string path, string[] extensions)
	{
		return GetFilesByExtensions(path, extensions);
	}

	private IEnumerable<string> GetFilesByExtensions(string path, string[] extensions)
	{
		var extensionSet = new HashSet<string>(extensions.Select(ext => "." + ext), StringComparer.OrdinalIgnoreCase);
		return Directory.EnumerateFiles(path)
										.Where(file => extensionSet.Contains(Path.GetExtension(file)));
	}

	public IEnumerable<string> GetDirectoriesUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() => Directory.GetDirectories(path));
	}

	public IEnumerable<string> GetDirectories(string path)
	{
		return Directory.GetDirectories(path);
	}
}
