# Single Table Inheritance

Single Table Inheritance - represents an inheritance hierarchy of classes as a single table that has columns for all the fields of the various classes.

Relational databases don't support inheritance. So there is an issue how to represent inheritance structure in relational tables.
When mapping to a relational database, we try to minimize joins that can quickly mount up when processing an inheritance structure in multiple tables.

Single Table Inheritance maps all fields of all classes of an inheritance structure into a single table.

## How It Works

There is only one table that contains all the data for all classes in the inheritancehierarchy.
Each class stores the data that's relevant to it on one table row.
Any columns that aren't relevant are left empty.

There is also an extra field which indicates which class should be used to instantiate the object.
This field could be the name of the class, or a code field.

On loading data read the code (name) first to figure out which subclass to instantiate.
On saving the data the code (name) needs be written out by the superclass in hierarchy.

## When To Use

Single Table Inheritance is one of the options for mapping the fields in an inheritance hierarchy.
The alternatives are Class Table Inheritance and Concrete Table Inheritance.

It is not required to use only one form of inheritance mapping for the whole hierarchy - it is fine to mix them.
For example, map similar classes into a single table, and use Concrete Table Inheritance for any class that has a lot of specific data.

## Pros & Cons

Pros:

1. single table in the database;
1. no joins in retrieving the data;
1. any refactoring which moving fields up and down the hierarchy does not require database change.

Cons:

1. empty columns may confuse people who works directly on a table;
1. unused (empty) columns may lead to wasted space in the database;
1. single table may end up being too large (with many indexes and frequent locking) which may effect the performance;
1. need to ensure that the column names are unique (e.g. use compound names with class name as a prefix).

## Example Notes

- `Player` - is a superclass in hierarchy.
- `Cricketer` - is a specific class in hierarchy. For an example simplicity used only one class in hierarchy.
- `PlayerMapper` - Data Mapper which is supposed to use for DB operation in `Player` hierarchy.
- `TableName`, `CreateDomainObject` and `Save` are not implemented in `PlayerMapper` since `Player` is an abstract class.
  Those methods are implemented in specific Player mapper (like `CricketerMapper`).