# Introduction-to-Entity-Framework-Core-6

```
migration: A migration allows us to express in code the changes that are going to occur in our database.

===========================================================================

>> dotnet ef migrations add initial(initial name is just the name of the migration You can put whatever name you want.)
>> Add-Migration Intial(initial name is just the name of the migration You can put whatever name you want.)

===========================================================================

>> dotnet ef database update
>> update-database
This UPDATE database is going to see all of the migrations that we have and it's going to try to connect to our server.
It is going to first verify if there is already an existing database that corresponds to the connection string that we created.
If there is no database, then it is going to create a new database and it is going to apply all of the migrations that we have in our application.
But if there is already a database that corresponds to that connection string that we created, then it is going to see what are the pending migrations.
Because if there are pending migrations, then we only want to apply those migrations in that database.
And the migrations that have already been applied in the database will not be applied a second time.

Agenda
-Create entities
-Configure fields
-Relationships
-3 kinds of  configuratoins

```