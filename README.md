[![publish to nuget](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/actions/workflows/nuget-publish.yml)
[![ActiveDirectoryFileManagement on NuGet](https://img.shields.io/nuget/v/ActiveDirectoryFileManagement?label=ActiveDirectoryFileManagement)](https://www.nuget.org/packages/ActiveDirectoryFileManagement/)
[![NuGet](https://img.shields.io/nuget/dt/ActiveDirectoryFileManagement)](https://www.nuget.org/packages/ActiveDirectoryFileManagement)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/blob/main/LICENSE)
[![paypal](https://img.shields.io/badge/PayPal-tip%20me-green.svg?logo=paypal)](https://www.paypal.me/shadynagy)
<a href="https://ShadyNagy.com">
    <img src="https://img.shields.io/badge/Visit-My%20Website-blue?logo=internetexplorer" alt="Visit My Website">
</a>


# Active Directory Manager

The `IActiveDirectoryManager` interface defines a contract for managing users within Active Directory. This includes capabilities to retrieve user details, update user attributes, and fetch specific user entries based on their SAM account name.

## Methods

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

# ActiveDirectoryService
The `ActiveDirectoryService` class is designed to facilitate the execution of actions under the context of an impersonated Active Directory (AD) user. This functionality is crucial in scenarios where actions need to be performed with the permissions of a specific AD user, rather than the permissions of the application's service account.

## Prerequisites

Before using the `ActiveDirectoryService`, ensure that:

- You have the necessary Active Directory permissions to impersonate a user.
- The application has a reference to the namespace or project where `ActiveDirectoryService` and its dependencies are defined.
- You have configured `ActiveDirectorySettings` with valid AD credentials and domain information.

## Usage

### Instantiating ActiveDirectoryService

To use the `ActiveDirectoryService`, first instantiate it with appropriate `ActiveDirectorySettings`:

```csharp
var activeDirectorySettings = new ActiveDirectorySettings
{
    Username = "ADUsername",
    Domain = "ADDomain",
    Password = "ADPassword"
};

var activeDirectoryService = new ActiveDirectoryService(activeDirectorySettings);
```

### Executing an Action with a Return Type

To execute a function that returns a result under the impersonated user context, use `ImpersonateUserAndRunAction<TResult>`:
```csharp
var result = activeDirectoryService.ImpersonateUserAndRunAction(() =>
{
    // Place your code here that needs to be run under the impersonated user.
    // For example, accessing a file on a network share that requires AD user permissions.
    return "Success";
});

Console.WriteLine(result); // Outputs: Success
```

### Executing an Action without a Return Type

To execute an action without expecting a return value, use ImpersonateUserAndRunAction:

```csharp
activeDirectoryService.ImpersonateUserAndRunAction(() =>
{
    // Place your action code here.
    // For example, writing to a log file on a network share.
});
```

