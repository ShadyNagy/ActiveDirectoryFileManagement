[![publish to nuget](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/actions/workflows/nuget-publish.yml/badge.svg)](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/actions/workflows/nuget-publish.yml)
[![ActiveDirectoryFileManagement on NuGet](https://img.shields.io/nuget/v/ActiveDirectoryFileManagement?label=ActiveDirectoryFileManagement)](https://www.nuget.org/packages/ActiveDirectoryFileManagement/)
[![NuGet](https://img.shields.io/nuget/dt/ActiveDirectoryFileManagement)](https://www.nuget.org/packages/ActiveDirectoryFileManagement)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/ShadyNagy/ActiveDirectoryFileManagement/blob/main/LICENSE)
[![paypal](https://img.shields.io/badge/PayPal-tip%20me-green.svg?logo=paypal)](https://www.paypal.me/shadynagy)
[![Visit My Website](https://img.shields.io/badge/Visit-My%20Website-blue?logo=internetexplorer)](https://ShadyNagy.com)

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

# DirectoryService

The `DirectoryService` class provides functionalities to interact with the file system under the context of an Active Directory (AD) user or directly. It leverages an `IActiveDirectoryService` for operations requiring AD user impersonation.

## Prerequisites

- An implementation of `IActiveDirectoryService` is required, properly configured for AD user impersonation.
- Ensure the application has permissions to create, delete, and access directories and files on the file system.

## Initialization

To initialize a `DirectoryService` instance, you need to pass an `IActiveDirectoryService` instance to its constructor.

```csharp
IActiveDirectoryService activeDirectoryService = new ActiveDirectoryService(activeDirectorySettings);
DirectoryService directoryService = new DirectoryService(activeDirectoryService);
```

### Creating Directories
#### CreateUnderUser
Creates a directory at the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleDirectory";
directoryService.CreateUnderUser(path);
// The directory is created at the specified path, using the permissions of the impersonated AD user.
```

#### Create
Creates a directory at the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleDirectory";
directoryService.Create(path);
// The directory is created at the specified path.
```

### Checking Directory Existence
#### IsExistsUnderUser
Checks if a directory exists at the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleDirectory";
bool exists = directoryService.IsExistsUnderUser(path);
Console.WriteLine(exists); // true or false
```

#### IsExists
Checks if a directory exists at the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleDirectory";
bool exists = directoryService.IsExists(path);
Console.WriteLine(exists); // true or false
```

### Deleting Directories
#### DeleteUnderUser
Deletes a directory at the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleDirectory";
directoryService.DeleteUnderUser(path);
// The directory at the specified path is deleted using the permissions of the impersonated AD user.
```

#### Delete
Deletes a directory at the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleDirectory";
directoryService.Delete(path);
// The directory at the specified path is deleted.
```

### Retrieving Files
#### GetFilesUnderUser
Retrieves the files from the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleDirectory";
IEnumerable<string> files = directoryService.GetFilesUnderUser(path);
// Retrieves all files in the specified directory using the permissions of the impersonated AD user.
```

#### GetFiles
Retrieves the files from the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleDirectory";
IEnumerable<string> files = directoryService.GetFiles(path);
// Retrieves all files in the specified directory.
```

### Filtering Files by Extension
#### GetFilesUnderUser with Extensions
Retrieves files from the specified path under the context of an AD user, filtered by extensions.
```csharp
string path = @"C:\ExampleDirectory";
string[] extensions = new[] { "txt", "docx" };
IEnumerable<string> files = directoryService.GetFilesUnderUser(path, extensions);
// Retrieves files with .txt and .docx extensions in the specified directory, using the permissions of the impersonated AD user.
```

#### GetFiles with Extensions
Retrieves files from the specified path without AD user impersonation, filtered by extensions.
```csharp
string path = @"C:\ExampleDirectory";
string[] extensions = new[] { "txt", "docx" };
IEnumerable<string> files = directoryService.GetFiles(path, extensions);
// Retrieves files with .txt and .docx extensions in the specified directory.
```

### Retrieving Directories
#### GetDirectoriesUnderUser
Retrieves the directories from the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleDirectory";
IEnumerable<string> directories = directoryService.GetDirectoriesUnderUser(path);
// Retrieves all subdirectories in the specified directory using the permissions of the impersonated AD user.
```

#### GetDirectories
Retrieves the directories from the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleDirectory";
IEnumerable<string> directories = directoryService.GetDirectories(path);
// Retrieves all subdirectories in the specified directory.
```

# FileService
The `FileService` class provides functionalities to manage files, including creating, overwriting, deleting, and reading files, with operations that can be performed under the context of an Active Directory (AD) user or directly.

## Prerequisites
- An implementation of `IActiveDirectoryService` is needed, properly configured for AD user impersonation.
- Ensure the application has permissions to manage files on the file system.

## Initialization
To use the `FileService`, instantiate it by passing an `IActiveDirectoryService` instance to its constructor.
```csharp
IActiveDirectoryService activeDirectoryService = new ActiveDirectoryService(activeDirectorySettings);
FileService fileService = new FileService(activeDirectoryService);
```

### Creating New Files
#### CreateUnderUser
Creates a new file with specified content under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
string content = "Hello, World!";
fileService.CreateUnderUser(path, content);
// Creates a new file with the specified content, using the permissions of the impersonated AD user.
```

#### Create
Creates a new file with specified content without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
string content = "Hello, World!";
fileService.Create(path, content);
// Creates a new file at the specified path with the given content.
```

### Overwriting Existing Files
#### OverwriteUnderUser
Overwrites an existing file with specified content under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
string newContent = "New Content";
fileService.OverwriteUnderUser(path, newContent);
// Overwrites the file with new content, using the permissions of the impersonated AD user.
```

#### Overwrite
Overwrites an existing file with specified content without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
string newContent = "New Content";
fileService.Overwrite(path, newContent);
// Overwrites the file at the specified path with the new content.
```

### Deleting Files
#### DeleteUnderUser
Deletes a file at the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
fileService.DeleteUnderUser(path);
// Deletes the file using the permissions of the impersonated AD user.
```

#### Delete
Deletes a file at the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
fileService.Delete(path);
// Deletes the file at the specified path.
```

### Reading File Contents
#### ReadUnderUser
Reads the contents of a file as a byte array under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
byte[] content = fileService.ReadUnderUser(path);
// Reads the file contents into a byte array, using the permissions of the impersonated AD user.
```

#### Read
Reads the contents of a file as a byte array without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
byte[] content = fileService.Read(path);
// Reads the file contents into a byte array at the specified path.
```

#### ReadTextUnderUser
Reads the contents of a file as a string under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
string content = fileService.ReadTextUnderUser(path);
// Reads the file contents as a string, using the permissions of the impersonated AD user.
```

#### ReadText
Reads the contents of a file as a string without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
string content = fileService.ReadText(path);
// Reads the file contents as a string at the specified path.
```

#### ReadLinesUnderUser
Reads the lines of a file as an enumerable collection of strings under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
IEnumerable<string> lines = fileService.ReadLinesUnderUser(path);
// Reads the file lines into a collection of strings, using the permissions of the impersonated AD user.
```

#### ReadLines
Reads the lines of a file as an enumerable collection of strings without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
IEnumerable<string> lines = fileService.ReadLines(path);
// Reads the file lines into a collection of strings at the specified path.
```

### Checking File Existence
#### IsExistsUnderUser
Checks if a file exists at the specified path under the context of an AD user.
```csharp
string path = @"C:\ExampleFile.txt";
bool exists = fileService.IsExistsUnderUser(path);
Console.WriteLine(exists); // Outputs: true or false
```

#### IsExists
Checks if a file exists at the specified path without AD user impersonation.
```csharp
string path = @"C:\ExampleFile.txt";
bool exists = fileService.IsExists(path);
Console.WriteLine(exists); // Outputs: true or false
```

## Extension Method for IServiceCollection
The ServiceCollectionExtensions class defines extension methods for the IServiceCollection interface. This interface is a part of the Microsoft.Extensions.DependencyInjection namespace, which is the DI container used in .NET Core and ASP.NET Core applications. By adding extension methods to IServiceCollection, you make it easy to register your custom services and configurations in the DI container, thereby decoupling the configuration from the application startup logic.

### Methods Overview  
- AddActiveDirectoryFileManagementServices with Parameters:  
This method allows for configuring Active Directory settings directly through parameters (userName, password, domain). It creates an instance of ActiveDirectorySettings with these parameters and registers it as a singleton in the service collection. This means only one instance of ActiveDirectorySettings will be created and used throughout the application lifecycle.  
It then calls the parameterless AddActiveDirectoryFileManagementServices method to register additional services.  

- AddActiveDirectoryFileManagementServices with ActiveDirectorySettings:  
This overload allows passing an already created ActiveDirectorySettings object. This is useful when the settings are pre-configured or loaded from another source. It registers the provided ActiveDirectorySettings instance as a singleton in the service collection.  
It also delegates to the parameterless AddActiveDirectoryFileManagementServices to register the additional services.  

- AddActiveDirectoryFileManagementServices (Parameterless):  
This method registers the core services related to Active Directory file management as scoped services. Scoped services are created once per request within the scope. This is ideal for services such as IFileService, IDirectoryService, IActiveDirectoryService, and IActiveDirectoryUserManager, which may maintain state or use resources like database connections or file streams that are request-specific.  

### Decoupling through DI  
Decoupling is achieved by abstracting the concrete implementations of services behind interfaces. When a class requires one of these services, it does not instantiate them directly but rather declares a dependency on the interface. The DI container is responsible for injecting these dependencies at runtime. This separation of concerns makes the system more flexible and easier to maintain.  

#### Example Usage  
#### Imagine you have a controller in an ASP.NET Core application that needs to manage files in an Active Directory environment:  
```csharp
public class FileManagerController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileManagerController(IFileService fileService)
    {
        _fileService = fileService;
    }

    // Actions that use _fileService to manage files
}
```

#### During application startup, you would configure your services like this:  
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddActiveDirectoryFileManagementServices("username", "password", "domain");
    // Or using an ActiveDirectorySettings instance
    // var settings = new ActiveDirectorySettings("username", "password", "domain");
    // services.AddActiveDirectoryFileManagementServices(settings);
    
    // Add other services like controllers
    services.AddControllers();
}
```

By registering services in this manner, you effectively decouple the FileManagerController from the concrete implementations of IFileService, allowing for more modular, testable, and maintainable code.  