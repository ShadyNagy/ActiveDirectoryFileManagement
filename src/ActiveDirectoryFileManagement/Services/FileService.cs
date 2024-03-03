using ActiveDirectoryFileManagement.Interfaces;

namespace ActiveDirectoryFileManagement.Services;

public class FileService : IFileService
{
	private readonly IActiveDirectoryService _activeDirectoryService;

	public FileService(IActiveDirectoryService activeDirectoryService)
	{
		_activeDirectoryService = activeDirectoryService;
	}

	/// <summary>
	/// Creates a new file with specified content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="content">The string content to write to the file.</param>
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

	/// <summary>
	/// Creates a new file with specified content.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="content">The string content to write to the file.</param>
	public void Create(string path, string content)
	{
		if (!File.Exists(path))
		{
			File.WriteAllText(path, content);
		}
	}

	/// <summary>
	/// Creates a new file with specified byte content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
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

	/// <summary>
	/// Creates a new file with specified byte content.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	public void Create(string path, byte[] contentBytes)
	{
		if (!File.Exists(path))
		{
			File.WriteAllBytes(path, contentBytes);
		}
	}

	/// <summary>
	/// Creates a new file and writes a sequence of lines under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="lines">The lines to write to the file.</param>
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

	/// <summary>
	/// Creates a new file and writes a sequence of lines.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="lines">The lines to write to the file.</param>
	public void Create(string path, IEnumerable<string> lines)
	{
		if (!File.Exists(path))
		{
			File.WriteAllLines(path, lines);
		}
	}

	/// <summary>
	/// Overwrites an existing file with specified content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="content">The string content to write to the file.</param>
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

	/// <summary>
	/// Overwrites an existing file with specified content.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="content">The string content to write to the file.</param>
	public void Overwrite(string path, string content)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllText(path, content);
	}

	/// <summary>
	/// Overwrites an existing file with specified byte content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
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

	/// <summary>
	/// Overwrites an existing file with specified byte content.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	public void Overwrite(string path, byte[] contentBytes)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllBytes(path, contentBytes);
	}

	/// <summary>
	/// Overwrites an existing file by writing a sequence of lines under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="lines">The lines to write to the file.</param>
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

	/// <summary>
	/// Overwrites an existing file by writing a sequence of lines.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="lines">The lines to write to the file.</param>
	public void Overwrite(string path, IEnumerable<string> lines)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.WriteAllLines(path, lines);
	}

	/// <summary>
	/// Deletes a file at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to delete.</param>
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

	/// <summary>
	/// Deletes a file at the specified path.
	/// </summary>
	/// <param name="path">The path of the file to delete.</param>
	public void Delete(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	/// <summary>
	/// Reads the contents of a file at the specified path as a byte array under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A byte array containing the contents of the file.</returns>
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

	/// <summary>
	/// Reads the contents of a file at the specified path as a byte array.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A byte array containing the contents of the file.</returns>
	public byte[] Read(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}

		return Array.Empty<byte>();
	}

	/// <summary>
	/// Reads the contents of a file at the specified path as a string under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A string containing the contents of the file.</returns>
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

	/// <summary>
	/// Reads the contents of a file at the specified path as a string.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A string containing the contents of the file.</returns>
	public string ReadText(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadAllText(path);
		}

		return string.Empty;
	}

	/// <summary>
	/// Reads the lines of a file at the specified path as an enumerable collection of strings under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file from which to read the lines.</param>
	/// <returns>An enumerable collection of strings, each representing a line in the file.</returns>
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

	/// <summary>
	/// Reads the lines of a file at the specified path as an enumerable collection of strings.
	/// </summary>
	/// <param name="path">The path of the file from which to read the lines.</param>
	/// <returns>An enumerable collection of strings, each representing a line in the file.</returns>
	public IEnumerable<string> ReadLines(string path)
	{
		if (File.Exists(path))
		{
			return File.ReadLines(path);
		}

		return new List<string>();
	}

	/// <summary>
	/// Checks if a file exists at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to check.</param>
	/// <returns>true if the file exists; otherwise, false.</returns>
	public bool IsExistsUnderUser(string path)
	{
		return _activeDirectoryService.ImpersonateUserAndRunAction(() =>
		{
			return File.Exists(path);
		});
	}

	/// <summary>
	/// Checks if a file exists at the specified path.
	/// </summary>
	/// <param name="path">The path of the file to check.</param>
	/// <returns>true if the file exists; otherwise, false.</returns>
	public bool IsExists(string path)
	{
		return File.Exists(path);
	}
}
