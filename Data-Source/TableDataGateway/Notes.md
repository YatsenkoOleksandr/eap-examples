# Table Data Gateway

Table Data Gateway - an object that acts as a Gateway (466) to a database table.
One instance handles all the rows in the table.

Table Data Gateway holds all the SQL for accessing a single table or view: select, insert, update, delete.
Other code calls its methods for all interaction with the database.

Table Data Gateway has a simple interface: several find methods to get data and CRUD methods.
Each method executes SQL against database connection.
Table Data Gateway is usually stateless.

How to return value:

1. simple data structure (e.g. map)
	- forces data to be copied out of the record set that comes from the database into the map
	- defeats compile time checking and is not an explicit interface
2. Data Transfer Object
3. Record Set that comes from the SQL query
	- object is aware of SQL instance
	- may make difficult to substitute the database for a file
4. appropriate Domain Object
	- bidirectional dependencies between domain objects and the gateway
	
Usually Table Data Gateway is per table in the database, but it is OK to have a single Table Data Gateway for all tables.

When to Use: for simple applications with Table Module, Transaction Script.

Makes sense to have Data Mapper communicate to the database via Table Data Gateway - it can be effective if want to use metadata for the Table Gateways but prefer handcoding for the actual mapping to the domain objects.
One of the benefits is that the same interface can work both for using SQL to manipulate the database and for using stored procedures/views.

Classic form of Table Data Gateway doesn't quite fit in the .NET since it does not take advantage of the ADO.NET data set;
instead it uses the data reader, which is a cursor-like interface to database records.

## Example Notes

- ADO.NET
  - almost classic form of Table Data Gateway, but uses ADO.NET Data Reader
- ADO.NET DataSet
  - Table Data Gateway which uses a holder to store the data.
  - `DataGateway` - superclass for Table Data Gateway with base class to load all data and store them in the data set holder
  - `DataSetHolder` - indexes the data sets and adapters by the name of the table; data set is a container for table-oriented data.
  - To update data need tp manipulate the data set directly in the client code:

    ```
    var person = new PersonGateway();
    person.LoadAll();
    person[key]["lastname"] = "Odell";
    person.Holder.Update();
    ```  
