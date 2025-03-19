# Serialized LOB

Serialized LOB - saves a graph of objects by serializing them into a single large object (LOB), which it stores in a database field.

Object models often contain complicated graphs of small objects with the links between them.
It is not so easy to put this structure into a relational schema:
the basic schema is simple - a table with a self-reference foreign key, however,
its manipulation of the schema requires many joins, which are slow and awkward.

The solution - serialization, where a whole graph of objects is written out as a single large object (LOB) in a table.
This Serialized LOB then becomes a form of memento.

## How It Works

There are 2 serialization ways:

1. Binary characters (BLOB).
1. Textual characters (CLOB).

When using Serialized LOB beware of identity problems.
For example, we need to store the customer details on an order as Serialized LOB:

1. Don't put the customer LOB in the order table - it will lead to duplicating customer details and may cause a problem when need to update customer details.
1. Put the LOB in a customer table so order table can link to it. Table will have ID column and a single LOB column for the data.

In general, be sure that Serialized LOB data can't be reached from anywhere but a single object that acts as the owner of Serialized LOB.

### BLOB

The BLOB is often the simplest approach since many platforms include the ability to automatically serialize an object graph.

BLOB Advantages:

- is simple to program;
- it uses the minimum of space.

BLOB Disadvantages:

- database must support a binary data type for it;
- it is hard to visually represent the field value;
- versioning - if class was changed you may not be able to read previous serializations.

### CLOB

In this case object graph is serialized into a text string.

CLOB Advantages:

- text string can be read easily by human;

CLOB Disadvanatages:

- text approach will usually need more space;
- may need to create own parser for textual formatting;
- may be slower than a binary serialization.

Many of the disadvantages can be overcome with XML: XML parsers are commonly available so there no need to develop own parser.
Unfortunately, XML requires even more space.
To resolve space issue is to use a zipped XML as BLOB - however you lose the direct human readability.

## When to Use

Serialized LOB works when you can chop out the piece of the object model and use it to represent the LOB.

## Pros & Cons

Pros:

- allows to persist a complex structure of objects with references between them into a single field.

Cons:

- can't query the structure (of Serialized LOB) using SQL;
- does not work when there are LOB references outside of it. 

## Example Notes

1. `Department` - represents a graph of objects with hierarchy to be serialized as LOB.
1. `Customer` - Active Record which uses XML format to serialize departments as Serialized LOB.
1. Actual DB connection and execution is skipped for example simplicity.
