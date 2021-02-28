# AdaptiveClient.EntityFrameworkCore

Utilities for using [AdaptiveClient](https://github.com/leaderanalytics/AdaptiveClient) to work with DBContext and migrations.


### Change log

2021-02-07 v2.0.0 - Migrate to .net 5. Drop support for .net framework. Update Entity Framework dependencies to EF Core 5.

## Classes

### `DatabaseUtilities`
* Use an `IEndPointConfiguration` to resolve a `DbContext`.  Check status of resolved `DbContext` or perform other operations such as applying migrations or dropping the database.

### `DatabaseValidationResult`
* Returns names of applied migrations, if any, and if database was created.


## Structures
### DatabaseStatus 
* Enum that provides status of a given database:  Unknown, DoesNotExist, NotConsistentWithModel, ConsistentWithModel.

## Interfaces
### `IDataBaseUtilities`
* Methods for resolving and working with DbContext.  See `DatabaseUtilities` implementation.
### `IDatabaseInitializer`
* Method for seeding a database when it is created or when a migration is applied.
 
### `IDbContextOptions`
* An interface for keying DbContextOptions.

### `IMigrationContext`
*  A placeholder interface for keying a MigrationContext

## RegistrationHelper Extensions
* **RegisterDbContext**  - Keys a class derived from `DbContext` to an API_Name.
* **RegisterDbContextOptions**  -   Keys a class that implements IDbContextOptions to a specific provider such as MSSQL or MySQL.
* **RegisterMigrationContext**  -  Allows EntityFramework to reflect on an assembly and resolve a DbContext to a specific provider such as MSSQL or MySQL.
* **RegisterDatabaseInitializer**  - Keys a class that provides initialization services such as seeding a newly created database.

## ResolutionHelper Extensions
* **ResolveDbContext**  - Resolves a `DbContext` instance using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveDbContextOptions**  - Resolves an instance of `DbContextOptions`  using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveMigrationContext**  - Resolves an implementation of `IMigrationContext` using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveDatabaseInitializer**  - Resolves an implementation of `IDatabaseInitalizer` using keys provided by a `IEndPointConfiguration` parameter.
