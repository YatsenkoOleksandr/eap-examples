# Active Record

Active Record - an object that wraps a row in a database table (or view), encapsulates the database access, and adds domain logic on that data.

The essence of an Active Record is a Domain Model (116) in which the classes match very closely the record structure of an underlying database.
Each Active Record is responsible for saving and loading to the database and also for any domain logic that acts on the data.
This may be all the domain logic in the application, or you may find that some domain logic is held in Transaction Scripts (110) with common and data-oriented code in the Active Record.

The data structure of the Active Record should exactly match that of the database: one field in the class for each column in the table.
Type the fields the way the SQL interface gives you the data—don’t do any conversion at this stage. You may consider Foreign Key Mapping (236), but you may also leave the foreign keys as they are.

The Active Record class typically has methods that do the following:

- Construct an instance of the Active Record from a SQL result set row
- Construct a new instance for later insertion into the table
- Static finder methods to wrap commonly used SQL queries and return Active Record objects
- Update the database and insert into it the data in the Active Record
- Get and set the fields
- Implement some pieces of business logic

Also, the getting method for related table can return the appropriate Active Record.

Active Record is a good choice for simple domain logic, like CRUD operations.
It's easy to build Active Record, and they are easy to understand - it works well only if the Active Record objects correspond directly to the database tables.

If business logic is complex (or there is a need to use object's direct relationships, collections, inheritance, and so forth) then it is better to use Data Mapper instead.

Active Record is a good pattern if Transaction Script beginning to have code duplication and difficulty in updating scripts and tables. In this case start gradually refactor behavior and create Active Records. It often helps to wrap the tables as a Gateway first and then start moving behavior so that the tables evolve to an Active Record.

Pros:

- easy to build when domain logic is simple

Cons:

- couples the object design to the database design - it makes difficult to refactor either design as a project goes forward

## Example Notes

- `DB` class actes as a facade for DB communication, public contract taken as-is from the book.