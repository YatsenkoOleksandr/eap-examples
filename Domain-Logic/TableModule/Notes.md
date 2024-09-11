# Table Module

Table Module - A single instance that handles the business logic for all rows in a database table or view.

Table Module organizes domain logic with one class per table in the database, and a single instance of a class contains the various procedures that will act on the data.
The primary distinction with Domain Model (116) is that, if you have many orders, a Domain Model (116) will have one order object per order while a Table Module will have one object to handle all orders.

Usually you use Table Module with a backing data structure that’s table oriented.
The tabular data is normally the result of a SQL call and is held in a Record Set (508) that mimics a SQL table.
The Table Module gives you an explicit method-based interface that acts on that data.
Grouping the behavior with the table gives you many of the benefits of encapsulation in that the behavior is close to the data it will work on.

The Table Module may include queries as factory methods.
The alternative is a Table Data Gateway (144), but the disadvantage of this is having an extra Table Data Gateway (144) class and mechanism in the design.
The advantage is that you can use a single Table Module on data from different data sources, since you use a different Table Data Gateway (144) for each data source.

When you use a Table Data Gateway (144) the application first uses the Table Data Gateway (144) to assemble data in a Record Set (508). You then create a Table Module with the Record Set (508) as an argument. If you need behavior from multiple Table Modules, you can create them with the same Record Set (508).
The Table Module can then do business logic on the Record Set (508) and pass the modified Record Set (508) to the presentation for display and editing using the table-aware widgets.

Table Module is very much based on table-oriented data, so obviously using it makes sense when you’re accessing tabular data using Record Set (508). It also puts that data structure very much in the center of the code, so you also want the way you access the data structure to be fairly straightforward.
Table Module works better than a combination of Domain Model (116) and Active Record (160) when other parts of the application are based on a common table-oriented data structure.

---

Example notes:

- In the .NET architecture a data set object provides an in-memory representation of a database structure.
  It thus makes sense to create classes that operate on this data set.
  Each Table Module class has a data member of a data table, which is the .NET system class corresponding to a table within the data set.

Untyped data set used here because these are more common on different platforms, there’s a strong argument (page 509) for using .NET’s strongly typed data set.
