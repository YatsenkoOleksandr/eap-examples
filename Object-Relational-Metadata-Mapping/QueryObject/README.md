# Query Object

Query Object - an object that represents a database query.

Many developers aren't particularly familiar with SQL, so you start creating special finder methods
that hide the SQL inside parameterized methods.
But that will make it difficult to form more ad hoc queries, and also lead to duplication
in the SQL statements.

A Query Object is an interpreter - a structure of objects that can form itself into a SQL query.
This query can be created by referring to classes and fields rather than tables and columns.
In this way you can write the queries independently if the database schema changes,
since changes to the schema can be localized in a single place.

## How It Works

A Query Object is an application of the Interpreter pattern which represent a SQL query.
The primary roles are to alow a client to form queries and then
to turn those object structures into a SQL string.

In order to represent any query, you need a flexible Query Object.
Query Object won't be able to represent full power of SQL, but it can satisfy basic needs.
It is recommended to create a minimally functional Query Object for current needs
and evolve it as those needs grow.

A common feature of Query Object is that it can represent queries
in the language of the in-memory objects.
It means that, instead of using table and column names, you can use object and field names.
It can be useful if database structure differs from object structure.
In order to perform this change of view, the Query Object needs to know how the database
structure maps to the object structure, a capability that really needs Metadata Mapping.

For multiple databases you can design Query Object so that it produces different SQL
depending on which database the query is running against.

A more complex use of Query Object is to eliminate redundant queries against a database.
If the same query has already been run earlier in a session, you can use it to select objects
from Identity Map and avoid call to database.
A more sophisticated approach can detect whether one query is a particular case of an earlier query.

A variation on the Query Object is to allow a query to be specified by an example domain object.
You might have a person domain object whose last name is set but all other attributes are set to null.
This object can be treated like a query by example - that will return all people whose last name
is the same as in example object.

## When To Use It

Query Object is pretty sophisticated pattern, so most projects don't use it
if they have a handbuilt data source layer.
Query Object is needed when using with Domain Model and Data Mapper;
also Metadata Mapping is needed in this case to have the most benefits.

The advantages of Query Object come with more complex needs:
keeping database schema encapsulated, supporting multiple databases and/or schemas,
optimizing and avoiding multiple queries.

Usually it is more effective to use 3-rd party tool which provides Query Object
instead of developing its own.

## Pros & Cons

Pros:

- Allows to reuse queries to avoid duplicating.

- Encapsulate database schema in the single place so that in case of schema change
  there is no need to update query object creators.

- Supports multiple databases and/or schemas.

- May support query by example domain object.

Cons:

- The implementaion is sophisticated, and may require a lot of efforts to have a full power as a native SQL.

## Example Notes

- Query Object in an example demonstrates querying on single table based on set of "AND" criteria.
- Example shows Query Object in combination with Unit Of Work and Metadata Mapping.
- Used generics in [`Mapper`](./DataMapping/Mapper.cs), [`Unit Of Work`](./DataMapping/UnitOfWork.cs)
  and [`Query Object`](./QueryObject.cs).
- Used generics as reference to domain class in [`Data Map`](./MetadataMapping/DataMap.cs)
  and [`Column Map`](./MetadataMapping/ColumnMap.cs).
