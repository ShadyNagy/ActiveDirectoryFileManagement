# ActiveDirectoryFileManagement

# Active Directory Manager Documentation

The `IActiveDirectoryManager` interface defines a contract for managing users within Active Directory. This includes capabilities to retrieve user details, update user attributes, and fetch specific user entries based on their SAM account name.

## Interface Methods

### GetUser

Retrieves a `DirectoryEntry` object representing a user in Active Directory based on their SAM account name.

```csharp
DirectoryEntry? GetUser(string samAccountName);
```

- Parameters  
samAccountName: The SAM account name of the user to retrieve.  

- Returns  
A DirectoryEntry object for the specified user if found; otherwise, null.  

Example for GetUser
```csharp
using ActiveDirectoryFileManagement.Interfaces;
using ActiveDirectoryFileManagement.Services;
using ActiveDirectoryFileManagement.Models;
using System;

// Example setup - ensure you have these using directives

class Program
{
    static void Main(string[] args)
    {
        // Initialize Active Directory settings
        var settings = new ActiveDirectorySettings
        {
            Domain = "yourdomain.com",
            Username = "adminusername",
            Password = "adminpassword"
        };

        // Create an instance of your Active Directory manager
        IActiveDirectoryManager adManager = new ActiveDirectoryManager(settings);

        // SAM account name of the user you want to retrieve
        string samAccountName = "jdoe";

        // Use the manager to get the user's DirectoryEntry
        var userDirectoryEntry = adManager.GetUser(samAccountName);

        if (userDirectoryEntry != null)
        {
            // Successfully retrieved the user, you can now work with the DirectoryEntry object
            Console.WriteLine($"User found: {userDirectoryEntry.Properties["name"].Value}");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
}
```

### UpdateUserDetails
Updates specified attributes for a user in Active Directory.

```csharp
void UpdateUserDetails(string samAccountName, ActiveDirectoryUserDetails userDetails);
```

- Parameters  
samAccountName: The SAM account name of the user whose details are to be updated.  
userDetails: An ActiveDirectoryUserDetails object containing the attributes to update.  

Example for UpdateUserDetails  
```csharp
class Program
{
    static void Main(string[] args)
    {
        var settings = new ActiveDirectorySettings
        {
            Domain = "yourdomain.com",
            Username = "adminusername",
            Password = "adminpassword"
        };

        IActiveDirectoryManager adManager = new ActiveDirectoryManager(settings);

        string samAccountName = "jdoe";
        var userDetails = new ActiveDirectoryUserDetails()
                            .AddDetail("telephoneNumber", "123-456-7890")
                            .AddDetail("title", "Software Engineer");

        adManager.UpdateUserDetails(samAccountName, userDetails);

        Console.WriteLine("User details updated successfully.");
    }
}
```

### GetUserDetails
Retrieves detailed information about a user from Active Directory.
```csharp
ActiveDirectoryUserDetails GetUserDetails(string samAccountName);
```

- Parameters  
samAccountName: The SAM account name of the user whose details are to be retrieved.  
- Returns  
An ActiveDirectoryUserDetails object containing the user's details.  

Example for GetUserDetails
```csharp
class Program
{
    static void Main(string[] args)
    {
        var settings = new ActiveDirectorySettings
        {
            Domain = "yourdomain.com",
            Username = "adminusername",
            Password = "adminpassword"
        };

        IActiveDirectoryManager adManager = new ActiveDirectoryManager(settings);

        string samAccountName = "jdoe";

        var userDetails = adManager.GetUserDetails(samAccountName);

        if (userDetails != null && userDetails.UserDetails.Count > 0)
        {
            Console.WriteLine("User details retrieved successfully:");
            foreach (var detail in userDetails.UserDetails)
            {
                Console.WriteLine($"{detail.Key}: {detail.Value}");
            }
        }
        else
        {
            Console.WriteLine("User details not found or user has no details.");
        }
    }
}
```