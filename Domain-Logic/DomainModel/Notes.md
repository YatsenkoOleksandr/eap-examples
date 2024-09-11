# Domain Model

Domain Model - an object model of the domain that incorporates both behavior and data.

A Domain Model creates a web of interconnected objects, where each object represents some meaningful individual, whether as large as a corporation or as small as a single line on an order form.

Domain Model - objects that mimic the data in the business and objects that capture the rules the business uses.

Mostly the data and process are combined to cluster the processes close to the data they work with.

A simple Domain Model looks very much like the database design with mostly one domain object for each database table.

A rich Domain Model can look different from the database design, with inheritance, strategies, and other [Gang of Four] patterns, and complex webs of small interconnected objects.

An OO domain model will often look similar to a database model, yet it will still have a lot of differences. A Domain Model mingles data and process, has multivalued attributes and a complex web of associations, and uses inheritance.

Simple Domain Model can use Active Record.
Rich Domain Model requires Data Mapper.

Bloat class vs usage-specific class:

- Duplication can quickly lead to more complexity and inconsistency, but bloating occurs much less frequently than predicted;
- Advice: not to separate usage-specific behavior, put it all in the object that's the natural fit; fix the bloating when, and if, it becomes a problem.

When you use Domain Model you may want to consider Service Layer (133) to give your Domain Model a more distinct API.

---
Example notes:

- Did not use `Money` pattern object, but created a helper class `Money` to split the decimal value.
- Example does not show how the objects are retrieve from, and written to, the database:
  - mapping domain model is hard, so omitted it for the simplicity;
  - the whole point of a Domain Model is to hide the database.
- There are no conditionals in the calculation, developer set up the decision path when creating the product with the appropriate strategy:

  ```
  // Each product has its own strategy
  Product word = Product.NewWordProcessor("Thinking Word");
  Product calc = Product.NewSpreadsheet("Thinking Calc");
  Product db = Product.NewDatabase("Thinking DB");

  // Calculate revenue recognition
  Contract wordContract = new Contract(word, 25, new DateTime(2024, 8, 12));
  wordContract.CalculateRevenueRecognitions();
  var recognizedRevenue = wordContract.RecognizedRevenue(DateTime.Today);
  ```
