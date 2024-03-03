namespace ActiveDirectoryFileManagement.Models;

public class ActiveDirectoryUserDetails
{
	public Dictionary<string, string> UserDetails { get; private set; } = new();

	public ActiveDirectoryUserDetails AddDetail(string propertyName, string value)
	{
		if (UserDetails.ContainsKey(propertyName))
		{
			UserDetails[propertyName] = value;
			return this;
		}
		UserDetails.Add(propertyName, value);

		return this;
	}

	public string GetDetail(string propertyName)
	{
		if (!UserDetails.ContainsKey(propertyName))
		{
			return string.Empty;
		}

		return UserDetails[propertyName];
	}

	public ActiveDirectoryUserDetails DeleteDetail(string propertyName)
	{
		if (!UserDetails.ContainsKey(propertyName))
		{
			return this;
		}

		UserDetails.Remove(propertyName);

		return this;
	}
}
