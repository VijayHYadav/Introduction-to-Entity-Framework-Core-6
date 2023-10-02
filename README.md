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

```
![Alt text](resources/image.png)
![Alt text](resources/globleNoTracking.png)
