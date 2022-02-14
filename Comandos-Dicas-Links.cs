#Comando p/ criar a solution:
dotnet new sln [name]

#Comando para criar a API:
dotnet new webapi -o API

#Comando p/ adicionar o Projeto a Solution:
dotnet sln add [Name/]

#Comando p/ criar ClassLib
dotnet new classlib -o [Classlib_Name] (precisa adicionar na solution)

#Comando p/ adicionar referencia
dotnet add reference ../[Classlib_Name]/

dotnet restore

- Criar uma pasta Data e uma classe DbContext : DbContext
------------------------------------------------------------------------------------------------
Instalar os Pacotes:
-------------------------------------------------------------------------------------------------
EntityFrameworkCore
EntityFrameworkCore.Design
EntityFrameworkCore.SqlServer
EntityFrameworkCore.Tools
	- Comandos do Tool:
		dotnet tool install --global dotnet-ef
		dotnet tool install --global dotnet-ef --version 3.1.4
--------------------------------------------------------------------------------------------------
- appsettings, adicionar a string de conexão
  "ConnectionStrings": {
    "DefaultConnection" : "server=localhost\\sqlexpress;database=[DB_NAME];trusted_connection=true"
  }
    "ConnectionStrings": {
    "DefaultConnection" : "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SkipedDB;Integrated Security=True;"
  }
---------------------------------------------------------------------------------------------------
- Registrar o banco dentro de Program ou Startup

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

- dotnet ef migrations add [MigrationName] ou
	dotnet ef migrations add [MigrationName] -o Data/Migrations
    dotnet ef migrations add InitialCreate -o ..\Infrastructure\Data\Migrations
    dotnet ef migrations add InitialCreate -p Infrastructure -s API -o Data\Migrations
    dotnet ef --startup-project ../API/ migrations add InitialCreate -o Data\Migrations


    link https://stackoverflow.com/questions/38705694/add-migration-with-different-assembly

    targetAssembly = the target project you are operating on. On the command line, it is the project in the current working directory. In Package Manager Console, it is whatever project is selected in the drop down box on the top right of that window pane.

    migrationsAssembly = assembly containing code for migrations. This is configurable. By default, this will be the assembly containing the DbContext, in your case, Project.Data.dll. As the error message suggests, you have have a two options to resolve this

    1 - Change target assembly.

    cd Project.Data/
    dotnet ef --startup-project ../Project.Api/ migrations add Initial

    // code doesn't use .MigrationsAssembly...just rely on the default
    options.UseSqlServer(connection)

    2 - Change the migrations assembly.

    cd Project.Api/
    dotnet ef migrations add Initial

    // change the default migrations assembly
    options.UseSqlServer(connection, b => b.MigrationsAssembly("Project.Api"))



- dotnet ef migrations remove
ou
   dotnet ef migrations remove -p Infrastructure -s API
- dotnet ef database update
- dotnet ef database drop -p Infrastructure -s API

//Insert em ID do tipo GUID
--DECLARE @UNIQUEX UNIQUEIDENTIFIER
--SET @UNIQUEX = NEWID();

--insert into [dbo].[Products] (Id) values (@UNIQUEX)

- dotnet watch run
watch : Hot reload enabled. For a list of supported edits, see https://aka.ms/dotnet/hot-reload. Press "Ctrl + R" to restart.




//Startup or Program
//register and setup the DB
services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

//appsettings.json
"ConnectionStrings": {
    "DefaultConnection" : "server=localhost\\sqlexpress;database=SkinetDb;trusted_connection=true"
  }


link https://elanderson.net/2020/10/add-git-ignore-to-an-existing-visual-studio-solution-new-git-experience/
link https://stackoverflow.com/questions/5049363/difference-between-repository-and-service-layer
link https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application