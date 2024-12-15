# Dependent Mapping

Dependent Mapping - has one class to perform the database mapping for a child class.

When object is naturally appear in the context of other objects (meaning they are not referenced by any other object/table) it is make sense to map the depenedent object in the scope of master/main mapping.
Dependent Mapping allows to simplify the mapping procedure by having an extra mapping of dependent object.

## How It Works

The basic idea behind Dependent Mapping is that one class (the dependent) relies upon some other class (the owner) for its database persistence.
Each dependent can have only one owner and must have one owner.

For **Active Record** and **Row Data Gateway** the dependent class won't contain any database mapping code - its mapping code is located in the owner.
For **Data Mapper** there is no mapper for the dependent class - the mapping code is located in the mapper for the owner.
For **Table Data Gateway** there won't be a dependent class at all, all the handling of the dependent is done in the owner.

In most cases every load of the owner loads the dependents as well. If the dependencies are expensive to load use **Lazy Load** to avoid loading the dependents until need them.

Dependent does not have an **Identity Field** and it is not stored in an Identity Map.
There is no finder methods (by ID) for a dependent since all finds are done with the owner.

A dependent may be the owner of another dependent - in this case owner of the first dependent is also responsible for the persistence of the second dependent.

Only an owner in-memory object should have a reference to a dependent object.

In a UML, it's appropriate to use composition to show the relationship between an owner and its dependents.

When updating a collection of dependents it is safe to delete all rows that link to the owner and then reinsert all the dependents since there no outside references to dependents.

Dependents are very similar to **Value Objects**, but they don't need the full mechanics of Value Objects (like overriding equals).

Dependent Mapping complicates change tracking  of the owner: any change to a dependent needs to mark the owner as changed so that the owner will write the changes to the database.
This can be simplified by making the dependent immutable, so that any change to it needs to be done by removing it and adding a new one - this can make the in-memory model more complicated, but it simplifies the database mapping.

## When To Use

Use Dependent Mapping when:

- there is an object that is only referred by another object;
- the owner has a collection of references to its dependents but there is no back pointer.

Preconditions for Dependent Mapping:

1. Dependent mush have exactly one owner.
1. There no references from any other object except the owner to the dependent.

## Pros & Cons

Pros:

- Simplifies persistence of dependent objects.

Cons:

- **Dependent Mapping** does not work well with **Unit Of Work**

  The delete and reinsert strategy does not work if Unit Of Work is keeping a track of objects. Also it can lead to problems like orphan rows since Unit Of Work is not controlling the dependents.

## Example Notes

- Omitted actual DB calls for example simplicity