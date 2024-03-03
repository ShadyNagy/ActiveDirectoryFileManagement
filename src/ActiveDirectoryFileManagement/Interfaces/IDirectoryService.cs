namespace ActiveDirectoryFileManagement.Interfaces;

public interface IDirectoryService
{
	void CreateUnderUser(string path);
	void Create(string path);
	bool IsExistsUnderUser(string path);
	bool IsExists(string path);
	void DeleteUnderUser(string path);
	void Delete(string path);
	IEnumerable<string> GetFilesUnderUser(string path);
	IEnumerable<string> GetFiles(string path);
	IEnumerable<string> GetFilesUnderUser(string path, string[] extensions);
	IEnumerable<string> GetFiles(string path, string[] extensions);
	IEnumerable<string> GetDirectoriesUnderUser(string path);
	IEnumerable<string> GetDirectories(string path);
}
