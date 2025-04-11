# Concrete Table Inheritance

Concrete Table Inheritance / Leaf Table Inheritance -
represents an inheritance hierarchy of classes with one table per concrete class in the hierarchy.

Concrete Table Inheritance - is yet another mapping pattern, where there is a table for each concrete class in the hierarchy.

## How It Works

Concrete Table Inheritance uses one table for each class in the hierarchy.
Each table contains columns for the concrete class and all its ancestors,
so any fields in a superclass are duplicated across the tables of the subclasses.

The key should be unique not just to a table but to all the tables from a hierarchy.
If keys can be duplicated between the tables then you will get multiple rows for a particular key value.
Thus, need a key allocation system that keeps track of key usage across tables;
also, you can't rely on the databases's primary key uniqueness mechanism.

This becomes awkward if the database is used by other systems.
In this situation need either avoid using superclass fields, or use a compound key that involves table identifier.

As alternative is to have accessfors for the supertype in the interface but to use several private fields for each concrete type in the implemetation.
The interface then combines values from the private fields.
If the public interface is a single value, it picks whichever of the private values aren't null.
If the public interface is a collection value, it replies with the union of values from implementation fields.

For compound keys you can use a special key object as ID field for Identity Field.
This key uses both the primary key of the table and the table name to determine uniqueness.

When searching for "superclass" entity, need to look at all tables to see which ones contain the appropriate value.
This means using multiple queries or using an outer join, both of which impact the performance.

## When To Use It

Concrete Table Inheritance is one of the options for mapping the fields in an inheritance hierarchy.
The alternatives are Single Table Inheritance and Class Table Inheritance.

It is not required to use only one form of inheritance mapping for the whole hierarchy - it is fine to mix them.
For example, use Concrete Table Inheritance for one or two subclasses and Single Table Inheritance for the rest.

## Pros and Cons

Pros:

- Each table is self-contained and does not have irrelevant fields.

  This makes sense when table is used by other applications (especially which do not use objects).

- No joins when reading the data from specific mapper.

- Each table is accessed only when that class is accessed.

Cons:

- Primary keys may be difficult to handle.

- Cannot enforce database relationships to abstract classes.

- If the fields on the domain class are pushed up or down the hierarchy, still need to alter the table definitions.
  
  However, there should be less alteration as with Class Table Inheritance in such case.

- If a superclass field changes, need to update each table that has this field
  (because superclass fields are duplicated across the tables).

- A find on the superclass forces to check all the tables,
  which leads to multiple database accesses (or multiple joins).

## Example Notes

- Implemented concrete [mapper](./DataMapper/CricketerMapper.cs) for [Cricketer](./Domain/Cricketer.cs) only for the example simplicity.
- Method `GetNextID` to get the next key value is not implemented in base [mapper](./DataMapper/Mapper.cs) for the example simplicity.
