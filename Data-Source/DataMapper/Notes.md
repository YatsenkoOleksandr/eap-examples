# Data Mapper

Data Mapper - a layer of Mappers that moves data between objects and a database while keeping them independent of each other and the mapper itself.

When you build an object model with a lot of business logic it’s valuable to use these mechanisms to better organize the data
and the behavior that goes with it. Doing so leads to variant schemas; that is, the object schema and the relational schema don’t match up.
The Data Mapper is a layer of software that separates the in-memory objects from the database.

Its responsibility is to transfer data between the two and also to isolate them from each other. With Data Mapper the in-memory objects
needn’t know even that there’s a database present; they need no SQL interface code, and certainly no knowledge of the database schema.

The separation between domain and data source is the main function of a Data Mapper.

A simple Data Mapper would just map a database table to an equivalent in-memory class on a field-to-field basis. Mappers need a variety of strategies to handle classes that turn into multiple fields, classes that have multiple tables, classes with inheritance.

When it comes to inserts and updates, the database mapping layer needs to understand what objects have changed, which new ones have been created, and which ones have been destroyed. It also has to fit the whole workload into a transactional framework.
The Unit of Work (184) pattern is a good way to organize this.

Mapping layers have techniques to reduce pulling data from the database while minimizing the impact on the in-memory objects, using Lazy Load.

If mappers are hardcoding, it's best to use one for each domain class or root of a domain hierarchy.

If using Metadata Mapping, then it would be use a single mapper class. With a large application it can be too much to have a single mapper with lots of find methods, so it makes sense to split these methods up by each domain class or head of the domain hierarchy.

As with any database find behavior, the finders need to use an Identity Map (195) in order to maintain the identity of the objects read from the database. Either you can have a Registry (480) of Identity Maps (195), or you can have each finder hold an Identity Map (195).

In order to work with an object, you have to load it from the database.
Usually the presentation layer will initiate things by loading some initial objects.
Then control moves into the domain layer at which point the code will mainly move from object to object using associations between them.
This will work effectively providing that the domain layer has all the objects it needs loaded into memory or that you use Lazy Load (200) to load in additional objects when needed.

On occasion you may need the domain objects to invoke find methods on the Data Mapper.
With a good Lazy Load (200) it is possible to avoid this.
It is not worth to try to manage everything with associations and Lazy Load in simple applications.

Still, it is not recommended to have a dependency from domain objects to Data Mapper.
This could be solved by Separated Interface: put any find methods needed by the domain code into an interface class that you can place in the domain package.

Mappers need access to the fields in the domain objects which could be a problem because public methods are needed for mapper only, but not for the domain logic. Options to do the mapping:

- (not recommended) Lower level of visibility by packaging the mapper closer to domain objects

  But this confuses since the system which knows domain object will know the mapper.

- (not recommended) Use reflection

  Bypassing visibility rules via reflection is a slow process.

- rich constructor
  Domain object will be created wih all mandatory data.
  

- empty constructor + public setters
  
## Example Notes