Write-Host "Starting to Conjure Projectxxxx" -ForegroundColor Green

$ProjectName= Read-Host -Prompt "Enter Your Solution Name"
$UserName= Read-Host -Prompt "Enter Your UserName (example UD_XYZ)"
$PW= Read-Host -Prompt "Enter Your Database Password"
$conn= Read-Host -Prompt "Enter Your TNS Name Connection"


#$UserName= "LAB3"
#$PW= "Section1"
#$conn= "FREEPDB1"
#$ProjectName= "WebRest"

mkdir  ${ProjectName}
cd  ${ProjectName}
dotnet new sln -n ${ProjectName}
dotnet new webapi -f net8.0 -n ${ProjectName} --use-controllers
dotnet sln add ${ProjectName}/${ProjectName}.csproj
dotnet new classlib -f net8.0 -n ${ProjectName}EF
dotnet sln add ${ProjectName}EF/${ProjectName}EF.csproj
cd ${ProjectName}
dotnet add reference ..\${ProjectName}EF\${ProjectName}EF.csproj
cd ..
cd ${ProjectName}EF
dotnet add package Oracle.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet build
dotnet ef dbcontext scaffold "Data Source=${conn};User ID=${UserName};Password=${PW}" Oracle.EntityFrameworkCore --namespace ${ProjectName}EF.EF.Models --context-namespace ${ProjectName}EF.EF.Data --output-dir Models --context-dir Data --context "${ProjectName}OracleContext" --data-annotations --no-onconfiguring --force
dotnet build


cd ..
cd ${ProjectName}
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
 
dotnet aspnet-codegenerator controller -name RecipeIngredientsController -m RecipeIngredient -dc "${ProjectName}OracleContext" --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries