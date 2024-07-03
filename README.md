# Logger Library for C# (.NET 8) #

This logger library is designed to facilitate the logging of information, warnings, and errors to `.txt` files, organized by date. It is structured to create a new `.txt` file according to the date, within folders organized by year and month. This library can be used as a DLL to log various messages in other projects.

## Features ##
* Creates a folder structure based on the current date:
    * Year
    * Month folder inside the year folder
    * Day `.txt` file inside the month folder   
* Logs are categorized into information, warnings, and errors.
* Additional separate `Errors.txt` file for storing errors.
* The default log folder is `Logs` in the project directory, but this can be customized.

## Logging Format ##
`2024-03-05 13:13:21 -> [INFO]  : {message}`

## Installation ##
1. Clone the repository and build it to generate the DLL.
2. Add the DLL to your project references.

## Usage ##
1. Import the logger library into your project.
   ```C#
   using LoggerLib;
   ```
2. Inject the logger service into your service container.
   ```C#
   builder.Services.AddSingleton<IFileLogger, Logger>();
   ```
3. (Optional) Set a custom file path for the logs.
   ```C#
   var logger = serviceProvider.GetService<ILogger>();
   logger.SetFilePath("custom/path/to/logs");
   ```
4. Use the logging methods to log messages.
   ```C#
   logger.LogInfo("This is an informational message.");
   logger.LogWarning("This is a warning message.");
   logger.LogError("This is an error message.");
   ```

## Methods ##
* `LogInfo(string message)`: Logs an informational message.
* `LogWarning(string message)`: Logs a warning message.
* `LogError(string message)`: Logs an error message.

## Example ##
```C#
using LoggerLibrary;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Setup service collection
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILogger, Logger>();

        // Build service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get logger instance
        var logger = serviceProvider.GetService<ILogger>();

        // Optionally set custom file path
        logger.SetFilePath("custom/path/to/logs");

        // Log messages
        logger.LogInfo("Application started.");
        logger.LogWarning("This is a warning.");
        logger.LogError("An error occurred.");
    }
}
```
