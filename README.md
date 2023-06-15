# How to use MomenusApp

In the Windows desktop application, you will find three buttons with specific purposes:

![alt text](https://raw.githubusercontent.com/rotanmihyar/MomentusTechnologies-/master/Screenshot.png)
## Exit
This button is used to terminate the application. Clicking on the "Exit" button will close the application and end its execution.

## CSV Path ##
By clicking on this button, you can select the location where you want to export a CSV file. This file will be used to save the data from the application.

## Export ##
Clicking on the "Export" button triggers an action where the application pulls all the data from the database. It then converts this data into a CSV format and saves it in the location you previously selected by clicking on the "CSV Path" button.

These buttons provide functionality for controlling the application, selecting the export file location, and exporting data from the database to a CSV file.
# Solved Bugs #
1)The export takes sometimes forever, maybe there is an issue with the SQL.

solved by changing the query  [on line](https://github.com/rotanmihyar/MomentusTechnologies-/blob/master/Momentus%20Email%20Task/Form1.cs#L85)

2)The tool exports only prospect accounts, but it should only export active accounts.

solved by changing Status from "O" to "A" [on line](https://github.com/rotanmihyar/MomentusTechnologies-/blob/master/Momentus%20Email%20Task/Form1.cs#L85)

3)The last character of the country gets cut off, please make sure the full name is shown.

solved by changing the -2 to -1 on subString [on line](https://github.com/rotanmihyar/MomentusTechnologies-/blob/master/Momentus%20Email%20Task/Form1.cs#L101)

4)The selection of the path is not shown in the text field.

solved by adding new label and putting the path in it [on line](https://github.com/rotanmihyar/MomentusTechnologies-/blob/master/Momentus%20Email%20Task/Form1.cs#L38)


# Assumptions #
1- I assumed that the Status code for the Active Accounts is the letter "A". As I don't have access to the rest of the code or the actual DB, I couldn't verify that.

2- I assumed that the only performance issue on the query was the nested query. There is a possibility that there could be other issues (ex: no indexes), but with no access to the actual DB it was hard to verify.


# How to Run Windows Desktop Application using C# .NET 7

This guide provides instructions on how to run a Windows desktop application built using C# .NET 7.

## Prerequisites
Before running the application, please ensure that you have the following prerequisites installed on your system:

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

## Getting Started
To run the Windows desktop application, please follow these steps:

1. Clone or download the application's source code from the GitHub repository.

2. Open the solution file (.sln) in a compatible IDE such as Visual Studio or Visual Studio Code.

3. Build the solution to ensure that all dependencies are resolved and the application is compiled successfully.

4. Once the solution is built, locate the main executable file (.exe) in the output folder, typically found in the `bin/Debug` or `bin/Release` directory.

5. Double-click on the executable file to launch the Windows desktop application.

## Troubleshooting
If you encounter any issues while running the Windows desktop application, please consider the following troubleshooting steps:

1. Ensure that you have the correct version of the .NET 7 SDK installed on your system.

2. Verify that all required dependencies are properly referenced and installed.

3. Check for any error messages or exceptions in the application's logs or console output.

4. Consult the application's documentation or seek support from the project's maintainers for assistance.


## Support
For any questions or issues related to running the Windows desktop application, please contact rotanmihyar@gmail.com.

## Conclusion
With these instructions, you should now be able to successfully run the Windows desktop application using C# .NET 7. Enjoy using the application!
