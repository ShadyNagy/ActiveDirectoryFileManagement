using ActiveDirectoryFileManagement.Services;

namespace ActiveDirectoryFileManagement.Interfaces;

/// <summary>
/// Defines a set of methods for file operations, including creation, deletion, reading, and checking existence,
/// with support for operations under a user context, potentially utilizing Active Directory for impersonation.
/// </summary>
public interface IFileService
{
	/// <summary>
	/// Creates a new file with specified content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="content">The string content to write to the file.</param>
	void CreateUnderUser(string path, string content);

	/// <summary>
	/// Creates a new file with specified content.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="content">The string content to write to the file.</param>
	void Create(string path, string content);

	/// <summary>
	/// Creates a new file with specified byte content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	void CreateUnderUser(string path, byte[] contentBytes);

	/// <summary>
	/// Creates a new file with specified byte content.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	void Create(string path, byte[] contentBytes);

	/// <summary>
	/// Creates a new file and writes a sequence of lines under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="lines">The lines to write to the file.</param>
	void CreateUnderUser(string path, IEnumerable<string> lines);

	/// <summary>
	/// Creates a new file and writes a sequence of lines.
	/// </summary>
	/// <param name="path">The path where the file will be created.</param>
	/// <param name="lines">The lines to write to the file.</param>
	void Create(string path, IEnumerable<string> lines);

	/// <summary>
	/// Overwrites an existing file with specified content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="content">The string content to write to the file.</param>
	void OverwriteUnderUser(string path, string content);

	/// <summary>
	/// Overwrites an existing file with specified content.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="content">The string content to write to the file.</param>
	void Overwrite(string path, string content);

	/// <summary>
	/// Overwrites an existing file with specified byte content under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	void OverwriteUnderUser(string path, byte[] contentBytes);

	/// <summary>
	/// Overwrites an existing file with specified byte content.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="contentBytes">The byte array content to write to the file.</param>
	void Overwrite(string path, byte[] contentBytes);

	/// <summary>
	/// Overwrites an existing file by writing a sequence of lines under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="lines">The lines to write to the file.</param>
	void OverwriteUnderUser(string path, IEnumerable<string> lines);

	/// <summary>
	/// Overwrites an existing file by writing a sequence of lines.
	/// </summary>
	/// <param name="path">The path of the file to overwrite.</param>
	/// <param name="lines">The lines to write to the file.</param>
	void Overwrite(string path, IEnumerable<string> lines);
	/// <summary>
	/// Deletes a file at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to delete.</param>
	void DeleteUnderUser(string path);

	/// <summary>
	/// Deletes a file at the specified path.
	/// </summary>
	/// <param name="path">The path of the file to delete.</param>
	void Delete(string path);

	/// <summary>
	/// Reads the contents of a file at the specified path as a byte array under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A byte array containing the contents of the file.</returns>
	byte[] ReadUnderUser(string path);

	/// <summary>
	/// Reads the contents of a file at the specified path as a byte array.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A byte array containing the contents of the file.</returns>
	byte[] Read(string path);

	/// <summary>
	/// Reads the contents of a file at the specified path as a string under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A string containing the contents of the file.</returns>
	string ReadTextUnderUser(string path);

	/// <summary>
	/// Reads the contents of a file at the specified path as a string.
	/// </summary>
	/// <param name="path">The path of the file to read.</param>
	/// <returns>A string containing the contents of the file.</returns>
	string ReadText(string path);

	/// <summary>
	/// Reads the lines of a file at the specified path as an enumerable collection of strings under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file from which to read the lines.</param>
	/// <returns>An enumerable collection of strings, each representing a line in the file.</returns>
	IEnumerable<string> ReadLinesUnderUser(string path);

	/// <summary>
	/// Reads the lines of a file at the specified path as an enumerable collection of strings.
	/// </summary>
	/// <param name="path">The path of the file from which to read the lines.</param>
	/// <returns>An enumerable collection of strings, each representing a line in the file.</returns>
	IEnumerable<string> ReadLines(string path);

	/// <summary>
	/// Checks if a file exists at the specified path under the context of an Active Directory user.
	/// </summary>
	/// <param name="path">The path of the file to check.</param>
	/// <returns>true if the file exists; otherwise, false.</returns>
	bool IsExistsUnderUser(string path);

	/// <summary>
	/// Checks if a file exists at the specified path.
	/// </summary>
	/// <param name="path">The path of the file to check.</param>
	/// <returns>true if the file exists; otherwise, false.</returns>
	bool IsExists(string path);

	/// <summary>
	/// Moves file from path to another path under the context of an Active Directory user.
	/// </summary>
	/// <param name="sourcePath">The path of the file source.</param>
	/// <param name="destinationPath">The path of the file destination.</param>
	void MoveUnderUser(string sourcePath, string destinationPath);

	/// <summary>
	/// Moves file from path to another path.
	/// </summary>
	/// <param name="sourcePath">The path of the file source.</param>
	/// <param name="destinationPath">The path of the file destination.</param>
	void Move(string sourcePath, string destinationPath);
}
