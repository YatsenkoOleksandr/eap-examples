# Unit Of Work

Unit Of Work - maintains a list of objects affected by a business transactionp and coordinates the writing out of changes and the resolution of concurrency problems.

It is possible to change database with each change to object model, but this can lead to lots of very small database calls which ends up being very slow. A Unit Of Work keeps track of everything done during a business transaction that can affect the database, and then it figures out everything that needs to be done to alter the database as a result of work.

Unit Of Work keeps track on changes (new object created, existing one updated, or deleted). Also Unit Of Work may know about read objects so that it can check for inconsistent reads by verifying that none of the objects changed on the database during the transaction.
When it comes time to commit, the Unit Of Work decides what to do. It opens a transaction, does any concurrency checking, and writes changes out to to the database. Application programmers never explicitly call methods for database updates.

Places to register object to keep track:

1. Caller Registration - The client (caller) should explicitly register an object in the Unit Of Work. The drawback that it is possible to forget to do it.
2. Object Registration - Registration methods are placed in object methods. Unit Of Work should be either passed to the object, or to be in a well-known place (static class).

Unit of Work Controller - acts as a proxy/controller over a database. It handles all reads from the database and registers clean objects whenever they are read. Rather than marking objects as dirty, Unit Of Work Controller takes a copy at read time and then compares the object at commit time. It allows a selective updated of only changed fields, and avoids registration calls from the domain object, but this adds overhead to the commit process.

Ojects need to be able to find their current Unit Of Work.
A good way to do this is with a thread-scoped Registry.
Another way is to pass the Unit Of Work to objects that need it.
In either case it is needed to ensure that more than one thread can't get access to an Unit Of Work.
If there is an already session object associated with the bussiness transaction execution thread then the current Unit Of Work should be placed on that session object.

Unit of Work works with any transactional resource, not just databases, so you can also use it to coordinate with message queues and transaction monitors.

Alternatives to Unit Of Work:

1. Explicitly save object whenever need to alter it
2. Leave all updates to the end when having multiple database calls:
   - For Transaction Script:
     - Use variables to keep track of all the changed objects
   - For Domain Model:
     - use dirty flag and find all dirty objects at the end of transaction

## Pros & Cons

Pros:

- The best way to synchronize in-memory data with the database
- Allows to avoid multiple small calls (which end up to be slow) but have a single (database/business) transaction
- Allows to handle batch update
- Keeps traversing and update order in one place

Cons:

- Handling referential integrity might be hard and complex
- unit testing of domain object might be complicated when object is registered inside domain object methods (mock Unit Of Work)
- Unit Of Work should be thread-scoped because each business transaction executes within a single thread

## .NET Implementation (before EF)

In .NET the Unit of Work is done by the disconnected data set.
This makes it a slightly different pattern from the classical variety.
Most Units of Work register and track changes to objects.
.NET reads data from the database into a data set, which is a series of objects arranged like database tables, rows, and columns.
The data set is essentially an in-memory mirror image of the result of one or more SQL queries.
Each data row has the concept of a version (current, original, proposed) and a state (unchanged, added, deleted, modified), which, together with the fact that the data set mimics the database structure, makes for straightforward writing of changes to the database.

## Example Notes

- `DomainObject` is a domain object supertype and it has methods to register an object.
- `UnitOfWork.RegisterClean()` does not do anything in this example.
  
    Usually, an Identity Map is placed within a Unit Of Work to store a single domain object state in memory.
    In this case:

      - `UnitOfWork.RegisterClean()` would put the registered object in the map.
      - `UnitOfWork.RegisterNew()` - would put a new object in the map.
      - `UnitOfWork.RegisterRemoved()` - would remove a deleted object from the map.

- `UnitOfWork.Commit()` should locate the Data Mapper for each object and invoke the appropriate mapping method
- Data Mapper (`Mapper`) and Data Mapper Registry (`MapperRegistry`) are omitted for the simplicity.
- Tracking of read objects is not included for the simplicity.
- As each business transaction executes within a single thread an `UnitOfWork` is associated with the currently executing thread using `ThreadLocal<T>`. If there is an already session object associated with the bussiness transaction execution thread then the current Unit Of Work should be placed on that session object.
