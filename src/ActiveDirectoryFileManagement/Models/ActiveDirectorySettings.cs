namespace ActiveDirectoryFileManagement.Models;
public class ActiveDirectorySettings
{
	public string Username { get; set; }
	public string Password { get; set; }
	public string Domain { get; set; }

	public ActiveDirectorySettings(string username, string password, string domain)
	{
		Username = username;
		Password = password;
		Domain = domain;
	}
}
