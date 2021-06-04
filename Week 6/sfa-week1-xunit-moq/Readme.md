## Running the code coverage commands

###  1. Install latest version of packages
 - coverlet.collector
 - coverlet.msbuild

###  2. Oppen PowerShell and go to "src" folder or the folder with your solution file and execute

### 3. Run the command
 ```PowerShell
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura  /p:CoverletOutput='./testresults/' /p:Exclude="[RateApp.Models]*%2c[*]RateApp.DAL.Entities*" 
 ```
This command will collect code coverage information and store it in  "testresults" folder of each unit test project.


### 4. Run the command
```PowerShell
reportgenerator "-reports:.\RateApp.BLL.UnitTests\testresults\coverage.cobertura.xml;.\RateApp.WEB.UnitTests\testresults\coverage.cobertura.xml" -targetdir:.\testresults   
```
This command will merge the collected information of the two test result files into a single report and output it in the "src\testresults". The test results folder should have an "index.html" that could be opened with any browser. To see the results.

### Combine the two commands in a single step
```PowerShell
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura  /p:CoverletOutput='./testresults/' /p:Exclude="[RateApp.Models]*%2c[*]RateApp.DAL.Entities*"; reportgenerator "-reports:.\RateApp.BLL.UnitTests\testresults\coverage.cobertura.xml;.\RateApp.WEB.UnitTests\testresults\coverage.cobertura.xml" -targetdir:.\testresults   
```



 
- [Link to coverlet Documentation](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/MSBuildIntegration.md)

