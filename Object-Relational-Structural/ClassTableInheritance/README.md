# Class Table Inheritance

Class Table Inheritance - Represents an inheritance hierarchy of classes with one table for each class.

Relational databases don't support inheritance,
but you want database structure that map clearly to the objects and
allow links anywhere in the inheritance structure.

Class Table Inheritance supports this by using one database table per class in the inheritance structure.

## How It Works

There is one table per class in the domain model.
The fields in the domain class map directly to columns in the corresponding tables.
As with the other inheritance mappings the fundamental approach of Inheritance Mappers applies.

One issue is how to link the corresponding rows of the database tables:

1. Use common primary key value.
   Rows corresponds to the same domain object when row in the "parent" table and "child" table have the same key value.
   Since the superclass table has a row for each row in other tables, the primary keys are going to be unique across the tables.
1. Use foreign keys.
   Each table has its own primary key and foreign key into the superclass table to link the rows together.

The biggest implementation issue is how to bring data back from multiple tables in an efficient manner.
Solution is to use join across the various component tables, but joins for more that 3 tables tend to be slow.

Another issue is that in query it is unknown which tables to join exactly:

a. do an outer join, which is nonstandard and slow.
a. read the root table first and then use a code to figure out what tables to read next.
   But this approach involves multiple queries.

## When To Use It

Class Table Inheritance is one of the options for mapping the fields in an inheritance hierarchy.
The alternatives are Single Table Inheritance and Concrete Table Inheritance.

It is not required to use only one form of inheritance mapping for the whole hierarchy - it is fine to mix them.
For example, use Class Table Inheritance for the classes at the top of the hierarchy and Concrete Table Inheritance for those lower down.

## Pros & Cons

Pros:

- All columns are relevant for every row so tables are easier to understand and don’t waste space.
- The relationship between the domain model and the database is very
straightforward.

Cons:

- You need to touch multiple tables to load an object, which means a join or multiple queries and sewing in memory.
- Any refactoring of fields up or down the hierarchy causes database changes.
- The supertype tables may become a bottleneck because they have to be accessed frequently.
- The high normalization may make it hard to understand for ad hoc queries.

## Example Notes

- `PlayerMapper.Find` method reads twice from superclass table:
	1. to find row and read the type code;
	1. to read the common information from superclass table in the specific data mapper.
	
  ADO.NET data set caches data into memory, so 2-nd read will not cost so much.

- `PlayerMapper.Insert` method inserts 2 rows:
    1. into superclass table;
	1. into specific table.

- `PlayerMapper` does not implement some methods since load/save/etc. operations are done by specific mappers.

