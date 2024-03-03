namespace ActiveDirectoryFileManagement.Interfaces;
public interface IFileService
{
	void CreateUnderUser(string path, string content);
	void Create(string path, string content);
	void CreateUnderUser(string path, byte[] contentBytes);
	void Create(string path, byte[] contentBytes);
	void CreateUnderUser(string path, IEnumerable<string> lines);
	void Create(string path, IEnumerable<string> lines);
	void OverwriteUnderUser(string path, string content);
	void Overwrite(string path, string content);
	void OverwriteUnderUser(string path, byte[] contentBytes);
	void Overwrite(string path, byte[] contentBytes);
	void OverwriteUnderUser(string path, IEnumerable<string> lines);
	void Overwrite(string path, IEnumerable<string> lines);
	void DeleteUnderUser(string path);
	void Delete(string path);
	byte[] ReadUnderUser(string path);
	byte[] Read(string path);
	string ReadTextUnderUser(string path);
	string ReadText(string path);
	IEnumerable<string> ReadLinesUnderUser(string path);
	IEnumerable<string> ReadLines(string path);
	bool IsExistsUnderUser(string path);
	bool IsExists(string path);
}
