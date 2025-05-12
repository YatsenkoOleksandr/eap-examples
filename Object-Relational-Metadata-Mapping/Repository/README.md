# Repository

Repository - mediates between the domain and data mapping layers
using a collection-like interface for accessing domain objects.

A system with a complex domain model often benefits from a layer (like Data Mapper)
that isolates domain objects from details of the database access code.
In such system it can be worthwhile to build another layer of abstraction over the mapping layer
where query construction code is generated.
This layer helps to minimize duplicating query logic, especially when there are a large number
of domain classes or heavy querying.

A Repository mediates between the domain and data mapping layers,
acting like an in-memory domain object collection.
Client objects construct query specifications declaratively and submit them to Repository.
Objects can be added to and then removed from the Repository, as they can from a
simple collections of objects, and the mapping code encapsulated by the Repository
will carry out the appropriate operations behind the scenes.
Conceptually, a Repository encapsulates the set of objects persisted in a data store
and the operations performed over them, providing a more object-oriented view of the
persistance layer.
Repository also supports the objective of achieving a clean separation and one-way dependency
between the domain and data mapping layers.

## How It Works

Repository is a sophisticated pattern - it looks like an object-oriented database and in
that way it's similar to Query Object, which development teams may be more likely to encounter
in an ORM tools than build themselves.
However, if team has already built Query Object, it isn't huge step to add Repository capability.

Repository presents a simple interface. Clients create a criteria or query object.
Then that object is provided to the repository method to return a list of domain objects which
satisfied given criterias.
Various convenience methods similar to matching (criteria) can be defined in abstract repository.
When it is expected to return a single object (like search by identifier)
the repository method can return a found object instead of collection.

To code that uses a Repository, it appears as a simple in-memory collection of domain objects.
The fact that the domain objects themselves typically aren't stored directly in the Repository
is not exposed to the client code.

Repository replaces specialized finder methods on Data Mapper classes with
a specification-based approach for object selection.
With a Repository, client code constructs the criteria and then passes it to the Repository,
asking it to select those of its objects that match.
So from the client code's perspective, there is no notion of query "execution".

Under the hood, Repository combines Metadata Mapping with a Query Object
to automatically generate SQL code from the criteria.
Whether the criteria know how to add themselves to a query,
the Query Object knows how to incorporate criteria objects,
or the Metadata Mapping itself controls the interaction is an implementation detail.

The object source for the Repository may not be a relational database at all.
So Repository may be especially useful in systems with multiple
database schemas or sources for domain objects,
as well as in unit testing when using in-memory objects.

Repository also improves readability and clarity in the code that uses querying extensively.

## When To Use It

Repository reduces the amount of the code related to querying
in a large system with many domain object types and many possible queries.
Repository promotes the Specification pattern,
which encapsulates the query to be performed in a pure object-oriented way.
Clients can write code purely in terms of objects and never think in SQL terms.

Repository is extremely handy in situations with multiple data sources,
e.g. having an in-memory data source for unit tests.

Repository might be useful when a data feed is used as a source of domain objects -
XML using SOAP, etc.

## Pros & Cons

Pros:

- Allows to remove duplicating querying logic - by creating a criteria/query object and reusing it.
- Repository clients don't know about storage specification (in-memory, database, etc.)
  and treat repository as a collection of domain objects and can write code purely
  in terms of objects which is useful in DDD. 
- Allows to have multiple storage simultaneously (e.g. in-memory repository for unit testing).

Cons:

- The pattern is sophisticated and implementation will require additional time.
- Requires Domain Model + Data Mapper patterns implemented,
  and may need Metadata Mapping and/or Query Object patterns.

## Example Notes

- [`IRepository`](./Repositories/IRepository.cs) defines a collection-like interface
  with basic CRUD operations and query by given query object.
- There is an in-memory repository and relation database repository which uses Data Mapper.
- Repository uses Query Object instead of Criteria in this example.
- [`PersonRepository`](./Repositories/PersonRepository.cs) is a decorator
 over another repository implementation and implements
  unique method for person repository.
- Unit Of Work, DB layer and in-memory storage layer are simplified for an example simplicity.
