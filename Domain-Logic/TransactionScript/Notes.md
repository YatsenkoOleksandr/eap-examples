# Transaction Script

Transaction Script organizes business logic by procedures where each procedure handles a single request from the presentation.

Domain logic organized by the transactions that carry out with the system.
The most common is to have several Transaction Scripts in a single class, where each class defines a subject area of related Transaction Scripts.

The other way is to have each Transaction Script in its own class, using the Command pattern [Gang of Four].
In this case you define a supertype for your commands that specifies some execute method in which Transaction Script logic fits.

```
abstract class TransactionScript
{
	public abstract object Run();
}

class RecognizedRevenue: TransactionScript
{
	constructor(contractNumber: int, asOf: DateTime)
}
```

---
Example notes:

- Used `IDataReader` ADO.NET class instead of Java `ResultSet`.
- Did not use `Money` pattern object, but created a helper class `Money` to split the decimal value.
