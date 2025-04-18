# Inheritance Mappers

Inheritance Mappers - a structure to organize database mappers that handle inheritance hierarchies.

Inheritance Mappers allows to minimize amount of the code needed to save and load the data to the database
when mapping from an object-oriented inheritance hierarchy in memory to a relational database.
Also, Inheritance Mappers provide both abstract and concrete mapping behaviors that allows
to save or load a superclass or a subclass.

## How It Works

There are 2 options how to organize mappers with a hierarchy:

1. Each domain class has a mapper that saves and loads the data for that domain class.
1. There are also mappers for abstract classes, which can be implemented with mappers that
are actually outside of the basic hierarchy, but delegate the call to the appropriate concrete mappers.

Basic behavior for concrete mappers are the find, insert, update and delete operations.

Find methods are declared on the concrete subclasses because
they return a concrete class, not an abstract superclass.
There is an option to return an abstract class but that will force
mapper clients to downcast the result which is better to avoid.

The basic behavior of the find method method is to find the appropriate row in the database,
instantiate an object of the correct type (decided by the subclass)
and then load the object with data from the database.
The load method is declared in an abstract mapper superclass and implemented by each mapper in the hierarchy
which loads the behavior for its corresponding domain object.

The insert and update methods operate in a similar way using a save method.
Here you can define the interface on the superclass (on a Layer Supertype).
The insert method creates a new row and then saves the data from the domain object
using the save hook methods. The update method just saves the data using the save hook as well.

This scheme makes it easy to write the appropriate mappers to save the information needed
for a concrete class in the hiearchy.

The next step is to support loading and saving an abstract class in the hierarchy (abstract super class).
While concrete mapper classes can just use the abstract mapper's insert and update methods,
the hierarchy abstract class mapper's insert and update methods need to overried those to call a concrete mapper instead.
This combination of generalization and composition may be hard to understand and maintain.

So, there is an alternative solution to separate mappers into 2 classes.
The abstract hierarchy class mapper is responsible for loading and saving the specific data to the database.
This is an abstract class whose behavior is just used only by the concrete mapper objects.

A seperate mapper class is used for the interface for operations at the hierarchy abstract class level.
That mapper class provides a find method and overrides the insert and update methods.
For all of those operations mapper should determine which concrete mapper to use and then delegate to it.


## When To Use It

This general scheme makes sense for any inheritance-based database mapping.
There are no good alternatives to this pattern:

a. duplicating superclass mapping code across the concrete mappers
a. folding hierarchy into a single abstract class mapper

## Pros & Cons

Pros:

- allows to reuse the code where specific mapper need to define the load and save operations for a specific domain object.

Cons:

- having separate mapper classes may confuse at the beginning.

## Example Notes

See examples in [Single Table Inheritance](../SingleTableInheritance),
[Class Table Inheritance](../ClassTableInheritance) and
[Concrete Table Inheritance](../ConcreteTableInheritance) projects.

