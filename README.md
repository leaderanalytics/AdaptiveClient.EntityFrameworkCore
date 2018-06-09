# AdaptiveClient.EntityFrameworkCore

Utilities and classes for using [AdaptiveClient](https://github.com/leaderanalytics/AdaptiveClient) to work with DBContext and migrations.

## Classes

### `DatabaseUtilities`
* Use an `IEndPointConfiguration` to resolve a `DbContext`.  Check status of resolved `DbContext` or perform other operations such as applying migrations or dropping the database.

### `DatabaseValidationResult`
* Returns names of applied migrations, if any, and if database was created.

### `ServiceManifestFactory`
* A base class that allows any service belonging to a specific API to be accessed from an instance of any other service belonging to the same API.

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
* **RegisterServiceManifest**  - Keys a class derived from `ServiceManifestFactory` to a specific provider and end point type.

## ResolutionHelper Extensions
* **ResolveDbContext**  - Resolves a `DbContext` instance using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveDbContextOptions**  - Resolves an instance of `DbContextOptions`  using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveMigrationContext**  - Resolves an implementation of `IMigrationContext` using keys provided by a `IEndPointConfiguration` parameter.
* **ResolveDatabaseInitializer**  - Resolves an implementation of `IDatabaseInitalizer` using keys provided by a `IEndPointConfiguration` parameter.
