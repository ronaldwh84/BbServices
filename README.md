# README #



### What is this repository for? ###

This is a very simple web service for inserting and retrieving product information. I don't cover the step to deploy this to a real server but I assume this code will be run using the Visual Studio with IISExpress as the webserver.

This code is tested under the localhost environment with SQL Express as the database. The better way is to deploy this to a real server with MSSQL Server. But again, this is not the scope for now.

### How to run the application ###

* Pull the master branch.
* Setup the Database following the **Database Configuration** section.
* Open the BbServices.sln on Visual Studio.
* Restore the solution's NuGet Packages.
* Set Bb.WebService as Startup Project.
* Clean the solution.
* Rebuild the solution.
* Start the solution (by default it will open the IISExpress).
* The web service will run under IISExpress with URL 'http://localhost:5709/' with a blank page that showing a text 'Sorry, you have no access to this site.'

*You may experience an issue where you see an error page with the message 'Could not find file '...\BbServices\Bb.WebService\bin\roslyn\csc.exe'.' Simply clear, rebuild and start the solution again*

**Authentication:**
<br>
All the endpoints are secured using the Basic Authentication.
<br>
Username: BbWsUserName
<br>
Password: BbWsP@ssword!
<br>

These are the 3 endpoints available.
* http://localhost:5709/Products/PutProducts
* http://localhost:5709/Products/GetProducts
* http://localhost:5709/Products/DeleteProducts

To make your life easier you can import the Postman collection to start testing the Web Services. The script is located at the **Scripts** folder with name **BbService.postman_collection.json**

### Exception Log ###
To make it easier to identify any exception I log the exception caught to the **logs\webservice.log** file under the Bb.WebService project. When there is any exception caught the API will show the Reference Id in the **Message** field of the response where we can search the id on the log file to get more details on the exception.

### Database Configuration ###
* Run the **DatabaseScript.sql** in the **Scripts** folder on MSSQL Server. This script will create the database called **BbDb** with 1 table named **Product**
* Update the ConnectionString section on the Web.Config of the Bb.WebService project to match your own MSSQL Server and credential
The sample below is using SQL Express on the localhost:

```
  <connectionStrings>
    <add name="BbContext" connectionString="Data Source=(local)\SQLEXPRESS;Initial Catalog=BbDb;Persist Security Info=True;User Id=username;Password=password;Connect Timeout=300;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

### Unit Testing Setup ###
There are 2 projects for Unit Testing under UnitTest folder:
* Bb.UnitTest.Data.Repository.Ef, this project test the Entity Framework repository implementation.
* Bb.UnitTest.WebService, this project test the ProductsController in the Bb.WebService project

For each function and action, we test them with a different number of products. Besides the code algorithm efficiency, the performance of the functions will depend on several external factors like the database and the server's specification thus some of them may fail if the specification is too low.

Before you run the unit testing, update the ConnectionString section on the App.Config of the Bb.WebService project to match your own MSSQL Server and credential, similar to the one on the **Database Configuration** section.

To run the Unit Test:
* Open the Test Explorer
* Right-click the Test that you want to run and choose 'Run Selected Tests' OR just click the 'Run All' to start all tests.

### Projects Structure ###
- Data
  - Bb.Data
    <br>This project contains the model of our entity in the database and also the interface of the repository to access the data.
  - Bb.Data.Repository.Ef
    <br>This project contains the implementation of the interface of the repository using Entity Framework. We can create another project to use different ORM if needed in the future.
- Presentation
  - Bb.WebService
    <br>This project contains the implementation of the web service using ASP.NET MVC framework.
    <br>We implement IOC using the StructureMap library to make it more flexible for us to switch to another repository implementation in the future. For example, if we want to switch from Entity Framework to NHibernate. We can see the mapping configuration at this file **DefaultRegistry.cs**.
    <br>Automapper is used to map the view model with the entity.
- UnitTest
  - Bb.UnitTest.Data.Repository.Ef
    <br>This is the Unit Test for the repository implementation on Bb.Data.Repository.Ef project.
  - Bb.UnitTest.WebService
    <br>This is the Unit Test for the web service implementation on Bb.WebService.
  
### Asynchronous Approach ###
The asynchronous approach may not make any sense as for now but in the future when we need to perform some other operations (e.g. tracking requests parameters, sending data to analytics service) the asynchronous approach will give more benefits.

### Technology Stacks ###
* ASP.NET MVC
* .NET Framework 4.7.2
* Entity Framework 6.3.0
* StructureMap
* Automapper

