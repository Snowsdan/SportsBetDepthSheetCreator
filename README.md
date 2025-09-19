This project is a console app that will print out depth sheets for two teams in the NRL and one team from the MLB.
Several operations will be performed to the depth chart and the results of which you will be able to see in the console.
The code for these operations can be found in DepthChartApp.cs and is mainly used to demonstrate the functionality of the Depth Chart.

### How do I build and run this code?
You will need .NET 9.0. You can download it here: https://dotnet.microsoft.com/en-us/download/dotnet/9.0\

You can either:\
Open the solution in your favourite IDE, then build and run the DepthSheetCreator configuration.

OR

You can open the project in a terminal, navigate to the root of the project, and run the following commands:
```
dotnet build
dotnet run --project ./DepthSheetCreator/DepthSheetCreator.csproj
```

Additional Notes:\
Automated tests for the DepthSheetCreator project can be found in the DepthSheetCreator.Tests project.
