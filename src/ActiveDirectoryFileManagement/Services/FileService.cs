using ActiveDirectoryFileManagement.Interfaces;

namespace ActiveDirectoryFileManagement.Services;

public class FileService : IFileService
{
	private readonly IActiveDirectoryService _activeDirectoryService;

	public FileService(IActiveDirectoryService activeDirectoryService)
	{
		_activeDirectoryService = activeDirectoryService;
	}

	public void CreateUnderUser(string path, string content)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (!File.Exists(path))
			{
				File.WriteAllText(path, content);
			}
		});
	}

	public void Create(string path, string content)
	{
		if (!File.Exists(path))
		{
			File.WriteAllText(path, content);
		}
	}

	public void CreateUnderUser(string path, byte[] contentBytes)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (!File.Exists(path))
			{
				File.WriteAllBytes(path, contentBytes);
			}
		});
	}

	public void Create(string path, byte[] contentBytes)
	{
		if (!File.Exists(path))
		{
			File.WriteAllBytes(path, contentBytes);
		}
	}

	public void CreateUnderUser(string path, IEnumerable<string> lines)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (!File.Exists(path))
			{
				File.WriteAllLines(path, lines);
			}
		});
	}

	public void Create(string path, IEnumerable<string> lines)
	{
		if (!File.Exists(path))
		{
			File.WriteAllLines(path, lines);
		}
	}

	public void OverwriteUnderUser(string path, string content)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			File.WriteAllText(path, content);
		});
	}

	public void Overwrite(string path, string content)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllText(path, content);
	}

	public void OverwriteUnderUser(string path, byte[] contentBytes)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			File.WriteAllBytes(path, contentBytes);
		});
	}

	public void Overwrite(string path, byte[] contentBytes)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllBytes(path, contentBytes);
	}

	public void OverwriteUnderUser(string path, IEnumerable<string> lines)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			File.WriteAllLines(path, lines);
		});
	}

	public void Overwrite(string path, IEnumerable<string> lines)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllLines(path, lines);
	}

	public void DeleteUnderUser(string path)
	{
		_activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		});
	}

	public void Delete(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	public byte[] ReadUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				return File.ReadAllBytes(path);
			}

			return Array.Empty<byte>();
		});
	}

	public byte[] Read(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}

		return Array.Empty<byte>();
	}

	public string ReadTextUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				return File.ReadAllText(path);
			}

			return string.Empty;
		});
	}

	public string ReadText(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadAllText(path);
		}

		return string.Empty;
	}

	public IEnumerable<string> ReadLinesUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			if (File.Exists(path))
			{
				return File.ReadLines(path);
			}

			return new List<string>();
		});
	}

	public IEnumerable<string> ReadLines(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadLines(path);
		}

		return new List<string>();
	}

	public bool IsExistsUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			return File.Exists(path);
		});
	}

	public bool IsExists(string path)
	{
		return File.Exists(path);
	}
}
