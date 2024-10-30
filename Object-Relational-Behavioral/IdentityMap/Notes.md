# Identity Map

Identity Map - ensures that each object gets loaded only once by keeping every loaded object in a map.
Looks up objects using the map when referring to them.

Identity Maps help to avoid having multiple objects which represent the same record thus:

- to improve performance;
- to prevent conflict issues during database writing when one of those object was updated;

Identity Map keeps record of all objects that have been read from the database in a single transaction. Whenever need an object, check the Identity Map to see if it is already loaded.

In a simple case with an isomorphic schema there will be one Identity Map per database table.

Also, since Identity Map interact with concurrency management, Optimistic Offline Lock should be considered as well.

Usually (surrogate) primary key of the database table is used as Key in the Map when primary key is a single column.
The Key should be a simple data type so the comparison will work correctly.

Explicit Identity Map:

- has distinct methods for each kind of the object (e.g. `GetPerson(int id)`)
- provides compile-time checking in a strongly typed language
- easier to see which maps are available and their callers
- adding method for new object kind leads to adding a new map

Generic Identity Map:

- has a single method for all kinds of objects (e.g. `Get(string objectName, int id)`, or `Get<T>(int id)`)
- supports a generic map with a generic and reusable objects
- easy to construct a reusable Registry that work for all kinds of objects and does not need updating when adding a new map
- generic map could be used if all objects have the same type of key.

Single Map for the session:

- works if there are database-unique keys

Multiple Maps:

1. one map per class/table when database schema and object models are the same
2. one map per object when database schema differs from the object model
3. one map per inheritance tree - but keys should be unique across the tree
4. separate map per specific (inheritant) object

Identity Map usually tied for a session (map per session).

If Unit Of Work is already used then put Identity Map to Unit Of Work since it is the main place to keep track on data coming in/out.
Otherwise, put Identity Map to Registry that is tied to the session.

Identity Map could be tied to the process when dealing with read-only objects (read-only Map per process + updatable Map per session).

Identity Map also acts as a cache for database reads.

Identity Map is not needed for immutable objects, and Dependent Mapping.

## Pros & Cons

Pros:

- helps to avoid update conflict within a single session by loading an object only once and "caching" it later
- improves performance by "caching" loaded object and returning it instead of querying database
- works well with Unit Of Work

Cons:

- does not do anything to handle conflicts cross sessions

## Example Notes

- `Person` represents a domain object
- Used non-static methods in `IdentityMap` for simplicity
- Did not use map thread sharing for simplicity