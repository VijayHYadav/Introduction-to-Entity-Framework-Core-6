```
dotnet run

dotnet watch

dotnet watch --no-hot-reload

Agenda
- Statuses
- Insert
- Flexible mapping
- Updating
- Deleting
- Query filters

Models of Work
- Ef Core keeps tracking
- SaveChanges saves the pending changes
- Connected model(The idea of this model is that we use the same instance of the dbcontext, both to fetch the data and also to modify the data.)
- Disconnected model
- Entity status

Statuses
- Added
- Modified
- Unchanged
- Deleted
- Detached

Flexible Mapping
When we create an entity, sometimes we only use properties which will represent columns in a table, in sqlserver Typically this is fine.
But there may be other locations in which we may want to use a combination of properties and fields.
The idea is that we may want to use a field to represent our column in the sqlserver table. We will use the property to do a transformation 
over the data that we are about to insert or modify. We call this flexible mapping.

Agenda
- Configuration
- Primary keys
- Ignore
- Idexes, conversions
- Shadow properties
- Automating configurations

Configuration Modes
- When we talk about configurations, we mean defining the behaviour of EF Core when certain things happen.

Indexes
- We can configure indexes in our tables to increase the speed of certain queries.
- Indexes can be configured as unique.
- Primary keys are already configured as unique indexes.

Keyless Entities
- They allow us to express the result of an arbitrary query in terms of a class.
    so, we can enjoy the advantages of a strongly-typed language when consuming arbitrary queries.
- They allow us to centralize queries.
- We don't have to worry about the change tracker.

Summary Configurations, Primary Keys, Ignore, Indexes, Value Conversions, Keyless entities, Shadow Properties, Automating Configurations.

Agenda
- Basic concepts
- Configurations
- OnDelet
- Relationship types

Basic Types of Relationships
- One-to-One relationships
- One-to-Many relationships
- Many-to-Many relationships
Concepts
- Principle Key : Principal key typically refers to the primary key through which entities are linked.
For example, in our case of cinemas and cinema halls, there is typically a field in cinema halls that links the cinema hall to the corresponding cinema.
The value of this field is usually equal to the primary key in this case of the cinema entity. Although this does not have to be the case since we can 
configure an alternative key with which we can make the relationship.

- Principle entity: The principal entity is the one that contains the principal key. In the case of our example of cinemas and cinema halls, the principal entity is cinema.

- Dependent entity: The dependent entity is the one that does not contain the primary key as a column of its own. In the case of cinema halls, it does not contain the value of the primary key as its own value, but rather uses to link to a cinema foreign key.

- Foreign key : This refers to a property of an entity that links to other related entities.
For example, in the case of cinema, the property that contains a collection of cinema halls is an
example of an obligation property.


- Navigation property
- Required relationship
- Optional relationship

- OnDelete
Cascade
Client Cascade
No Action
Client No Action
Restrict
Set Null
Client Set Null

Owned Types:  Owen types allows us to separate columns from a table in different classes.
The main difference is that with own properties or own types that depend on entity can be used in several entities.

Summary
- Principal and dependent entity
- Configurations
- Conventions
- Data Annotations
- Fluent API
- Table Splitting
- Owned types
- Table-per-hierachy
- Table-per-type

Agenda
- Get-Help
- Modifying migrations
- Script-Migration
- Compiled models
- Database-First

>>dotnet ef migrations add --help
>>Get-Help Add-Migration
>>Get-Help Add-Migration -detailed
>>Get-Help Add-Migration -full

>>dotnet ef migrations add GenresExample
>>Add-Migration GenresExample

>>dotnet ef migrations add Initial --project ../Data/Data.csproj
>>Add-Migration Initial -Project Data

>>dotnet ef database update
>>Update-Database
>>Get-Help Update-Database
>>Get-Help Update-Database -detailed

>>dotnet ef migrations remove
>>Remove-Migration

>>dotnet ef migrations remove --force
>>Remove-Migration -Force

Another way is to have a new migration that reverts the previous migration.
This is useful for when in a migration you have serveral changes and maybe
you don't want to revert them.

>>dotnet ef migrations list
>>Get-Migration

>>dotnet ef migrations list --no-connect
>>Get-Migration -NoConnect

>>dotnet ef database drop
>>Drop-Database

First Create a migration then modify it and then apply it.

Migration Bundles => with migration bundles we create an executable which we are
going to run against our database then this is going to takecare of applying the migrations.

>> dotnet ef migrations bundle --configuration Bundle
>> .\efbundle.exe --connection "Server=.;Database=MigrationBundleEFCoreMovies;Integrated Security=True"

>> dotnet ef migrations bundle --configuration Bundle --force
>> .\efbundle.exe --connection "Server=.;Database=MigrationBundleEFCoreMovies;Integrated Security=True"

The script migration command allows us to generate a sql script with a migration that we'have.
>> dotnet ef migrations script
>> Script-Migration

Idempotent: allows us to generate the script that is going to take into account if migration has
already been applied and if it has, then it will not be applied twice.
>> dotnet ef migrations script --idempotent
>> Script-Migration -Idempotent


IMP: You should prefer migration bundles over to you migration script if you have custom sql code in the migrations.

Database.Migrate
- With migrate we can apply migrations from our application
- It's simple
- We have to be careful if the app executes in parallel
- There could be a timeout
- The app may load slowly
- If there's an error, it may be hard to debug
Ex Database.Migrate
using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    applicationDbContext.Database.Migrate();
}

Compiled Models
- They allow us to optimize our models
- It's not recommended if you have few entities.
- It's not compatible with query filters.
- It's not compatible with lazy loading.
- When you make chanegs, you must remember to execute a command.
>> dotnet efdbcontext optimize
>> Optimize-DbContext
options.UseModel(ApplicationDbContextModel.Instance);

>> dotnet ef dbcontext scaffold name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
>> Scaffold-DbContext -Connection name=DefaultConnection -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
>> Scaffold-DbContext -Connection name=DefaultConnection -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
builder.Services.AddDbContext<EFCoreMoviesDbContext>();

So if we add a new entity, a new property, or we make changes to the configuration of our model using
the fluent API, we have to make a migration to generate a class that will indicate the changes that
are going to occur in our database.

How does entity framework or knows which migrations has been applied and which migrations are pending?
Well, it knows that because it does a query into a table call EFMigrationsHistory.

Summary
- Get-Help
- Add-Migration
- Remove-Migration
- Update-Database
- Get-Migration
- Migration-Bundles
- Script-Migration
- Database-Migrate
- Compiled models
- Database-First

Section 8: DbContext
- Properties
- OnConfigure
- Updating state
- Events
- Transactions

We have already seen that the db-context is the centerpiece of entity framework core what are you
use called first or database first that db-context have some properties that allows us to do several
advanced tasks.

Properties
- Databases
- Change Tracker
- Model
- ContextId
- 

Summary
- DbContext
- OnConfiguring
- State
- Updating some fields
- SaveChanges
- Events
- Arbitrary Queries
- Transactions


Section 9: Advanced Scenarios
- Functions
- Computed columns
- Concurrency
- Temporal tables
- Commands

User-Defined Functions
- A user-defined is a function in our database which we can use to encapsulate functionality.
- These functions are only for querying data.
- They are not meant to be used to modify the database in any way, like inserting, modifying or deleting data.
- The result of this function can be a scalar or a result set.
- A function to compute the sum or costs.
- A function to compute the average of costs.

139. Concurrency Handling with ConcurrencyCheck
Note: Concurrency conflicts occurs when two people try to make an update over the same record and the update of the second person overrides accidentally Change that the first person.
The entity framework or allows us to handle the situations in two levels at the field level, at the role level.
In this video, we will talk about handling this conflict in the field level.
The idea of handling a concurrency conflict over a field is that there are fields that you want to treat with care.
In this way If two users read a field that is sensitive and they both try to update a field at the same time, then
the second user that did the update will get an error because this user does not have their latest version of the data.

.


```
![Alt text](resources/image.png)
![Alt text](resources/globleNoTracking.png)


Temporal tables allows us to have a history of all of the changes that occur in a table.
And by this, we mean that we are going to store in a separate table all of the data changes that occur in a table.


Summary
- User-Defined functions
- Scalar functions
- Tabled-valued  functions
- computed column
- concurrency conflicts
- temporal tables
- DbContext in another project

Basic Concepts
- A test it's an experiment that's done on something, to know the result of it.
- Testing a software is using it too see if it works.
- This is tedious and error-prone.

Automatic Tests
- An automatic test is a software the tests our software.

Qualities of a Good Test
- Consistent
- Independent
- Does not make changes
- Quick

Test Suites
- A test suite is a set of tests

Parts of a Test
- Preparation
- Testing
- Verification

Example of  a Test
- Preparation: 
    In order to test a wire of two accounts, we need two accounts, and one of them should have money.
    herefore, in this step, we are going to create two accounts and maybe something else that we may need could be a database, for example, testing.

- Testing: 
    Now that we have the two accounts we need to execute, a function that is going to take one account,
    is going to subtract the money from it and transfer that money to the other account. So for that, we may need to 
    execute a function that will receive the two accounts as a parameter and

- Verification: 
    We need to have a way to verify that for on one account we subtract the money and that the money was
    sent to the other account. And this depends entirely on how our application works.
    Maybe we want to use a query into a database to see that the money was transferred to the correct account


Unit Testing
- A unit test deals with individual units of source code.
- Unit tests are usually fast.
