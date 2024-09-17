# Row Data Gateway

Row Data Gateway - an object that acts as a Gateway to a single record on a data source. There is one instance per row.

Embedding database access code in in-memory objects have few disadvantages:

- database manipulation code increases complexity;
- making worse a testing (slower to run because of the database access).

Row Data Gateway gives objects that look exactly like the record in the record structure, but can be access with the regular mechanisms of programming languages.
All details of data source access are hidden behind that interface.

Row Data Gateway acts as an objects that mimics a single record (database row).
Each column in the database becomes one field.
This approach works well for Transaction SCripts.

There are 2 ways how to organize find operations:

1. Static find methods
   It precludes polymorphism when there is a need to substitute finder methods for different data sources.
2. Separate finder objects.
   So that each table will have one finder class and one gateway class for the results.

Active Record = Row Data Gateway + domain logic.

Row Data Gateway should contain only database access logic. Moving repeating business logic to Row Data Gateway will gradually turn it into an Active Record, which is good as it reduces business logic duplication.

There is a possibility that having 2 (and more) Row Data Gateway on the same underlying table may corrupt the update operation: second instance update may undo the changes on the first instance. There is no general prevention way: developers just have to be aware of it, or do not provide update operations.

Row Data Gateways are very good candidate for code generations based on a Metadata Mapping. This way database access code can be automatically built during build process.

Row Data Gateway + Transaction Script - nicely factors out the database access code and allows it to be reused easily by different Transaction Scripts.

Row Data Gateway does not suite for Domain Model:

1. if the mapping is simple, Active Record does the same job without an additional layer of code;
2. if the mapping is complex, Data Mapper works better.
3. Row Data Gateway could be used to shield the domain objects from the changing database structure
   BUT, on a large scale it leads to having three data representations: one in the business logic, one in the Row Data Gateway, and one in the database - that's too many.
   So it is better to have Row Data Gateway which mirrors the database structure.

Row Data Gateway + Data Mapper - seems like an extra work, but it can be effective if the Row Data Gateways are automatically generated from metadata while the Data Mappers are done by hand.

## Example Notes

- `DB` class actes as a facade for DB communication, public contract taken as-is from the book.
- Used Registry pattern (`Registry` class) to hold Identity Maps, for the example simplicity Registry is not implemented.
- Example usage of gateway in a Transaction Script:
  ```
  var finder = new PersonFinder();
  var responsibles = finder.FindResponsibles();
  foreach (PersonGateway person in responsibles)
  {
   ...
  }
  ```