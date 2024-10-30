# Identity Field

Identity Field - saves a database ID field in an object to maintain identity between an in-memory object and a database row.

In order to write data back need to tie the database to the in-memory object system.
In essence, Identity Field - store the primary key of the relational database table in the object's fields.

## Choose Key

1. Meaningful key vs Meaningless key

    Key should be unique, and it is well when key is immutable.
    Meaningful key is often mutable which may cause to mistakes due to human error while editing key value by client.
    So it is better to use meaningless keys.

1. Simple key vs Compound Key

    If use simple key, can re-use the same code for all key manipulation.
    Compound keys require special handling in concrete classes (which is not a problem with code generation).
    The most common operation with a key is equality checking, so need a type with fast equality operation.
    The other important operation is getting the next key.

    Hence integer type is often the best.
    Strings, can also work but equality checking may be slower and incrementing strings might be not a simple task.

    It is not recommended to use dates or times in keys - they may lead to problems with portability and consistency.
    Dates in particular are vulnerable because they are often stored to some fractional second precision which can easily get out of sync and lead to identity problems.

1. Table-unique key vs database-unique key

    A table-unique key is unique across the table.
    A database-unique key is unique across every row in every table in the database.
    Database-unique key is often easier to do and allows to use a single Identity Map.

    It is unlikely to run out of numbers for new keys.
    But if really needed, it is possible to reclaim keys from deleted objects with a simle database script that compacts the key space - although running this script will require to take the application offline.

    Inheritance affects when using table-unique keys. If using Concrete Table Inheritance or Class Table Inheritance it is much easier with keys that are unique to the hierarchy rather than unique to each table.

1. Key size

    The size of the key may effect performance, particularly with indexes. This is dependent on the database system and how many rows there are.

## Represnting the Identity Field in the Object

The simplest form of Identity Field is a field that matches the type of the key in the database.

Compound keys are more problematic.
The best way is to make a key class. A generic key class can store a sequence of objects that act as the elements of the key.
It is also useful to get parts of the key when mapping to the database.

If use the same basic structure for all keys, all of the key handling could be done in a Layer Supertype.
Layer Supertype can have a default behavior that works for the most cases and extend it for the exceptional cases in subtypes.

As another option, have a single key class which takes a generic list of key objects, or key class for each domain class with explicit fields for each part of the key.

If importing data between different database instances, need to remember that there is a key collision.
It could be solved with scheme to separate the keys between databases or with some kind of key migration on the imports.

## Getting a New Key

There are 3 basic choices:

1. Get the database to auto-generate
2. Use a GUID/UUID
3. Generate own key

### Auto-generated Key

The auto-generate is the easiest. The database generates a unique primary key on insert.
But not all databases auto-generate key which may cause problems for object-relational mapping.

The most common auto-generation method is declaring one auto-generated field, which is incremented to a new value when inserting a row.
The problem with this scheme is that there is no easy way to determine what value got generated as the key.
This kind of auto-generation does not work when need to insert connected objects (1-to-1, 1-to-many, many-to-many).

An alternative approach is a database counter (Oracle).

### GUID

The advantage of the GUID is that it is guaranteed to be unique across all machines.

The disadvantage is that a resulting key string is big:

- it is hard to type and read;
- it may also lead to performance issues, particularly with indexes.

### Generating own key

#### Table scan

Use table scan (using SQL MAX function) to find the largest key in the table and then add one to use it.

The drawbacks:

- read-locks while searching the key which affects the performance when having multiple inserts concurrently on the same table;
- need to ensure the complete isolation between transactions - otherwise they will get the same ID value.

#### Key table

Separate key table which have 2 columns: name and next available value.
This table will have just one row for database-unique keys; row per each table in the database for table-unique keys.

Key table usage:

1. Read row and not the value.
1. Increment the number.
1. Write it back to the row.

This technique allows to grab many keys at a time by adding a suitable number when updating the key table. This cuts down on expensive database calls and reduces connection on the key table.

Good idea is to have separate transaction for key table update from transaction to insert value into desired table.
By having this the lock on key table will be much shorter and it will not block others who need a key.
The downside is the lost key value in case of rollback - but that is not a big dealbreaker.

Using key table affects affects the choice of database-unique vs table-unique keys:

- need to add a row to the key table every time when adding new table to the database;
- database-unique keys won't work well if cannot arrange key table update to be in a separate transaction.

It's good to separate the code for getting a new key into its own class, as it makes easier to build a Service Stub for unit testing purposes.

## When to Use

Identity Field works when there is a mapping between in-memory objects and rows in a database: Domain Model, or Row Data Gateway.

Identity Field is not needed when using Transaction Script, Table Module, or Table Data Gateway.

For small objects with value semantics (money, date range objects) that don't have its own table, it is better to use Embedded Value.

Serialized LOB will suit better for a complex graph of objects that doesn't need to be queried within the relation database - it is usually easier to write and has better performance.

Alternative to Identity Field is to extend Identity Map to maintain the correspondance - Identity Map will return key for an object, and return object by the key.
