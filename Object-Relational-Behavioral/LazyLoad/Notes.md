# Lazy Load

Lazy Load - an object that doesn't contain all of the data you need but know how to get it.

Loading one object can have the effect of loading a huge number of related objects which are actually unneeded.
Lazy Load interrupts this loading process for the moment, leaving a marker in the object structure so that if the data is needed it can be loaded only when it is used.

There are 4 Lazy Load implementations:

- Lazy Initialization
- Virtual Proxy
- Value Holder
- Ghost

Lazy Load is a good candidate for aspect-oriented programming.
Lazy Load behavior can be put into a separate aspect which allows to change the load strategy separately as well as freeing the domain developers from having to deal with lazy loading issues.

If you need to use different lazy load strategies depending on domain object and there is already a Data Mapper then you could have a another one data mapper object: one loads item immediately, and another one loads lazily.

## Lazy Initialization

Every access to the field checks first to see if it is `null`. If so, it calculates the value of the field before returning the field.
The field should be self-encapsulated - all access to the field (even within the class) is done through a getting method.

Using a `null` to signal a not loaded field works well, unless `null` is a legal (business/domain valid) field value.
In this case need to use something else for signal, or need to use a Special Case for the null value.

Lazy initialization forces dependency between the object and the database.
So, it works best for Active Record, and Table/Row Data Gateway.

The best use case for Lazy Load is when an access to the field/data requires an extra call and the pulling data is not used when the main object is used.

## Virtual Proxy

Virtual Proxy (Gang of Four) - is an object that looks like the object that should be in the field, but does not actually contain anything. It loads the correct object when one of its methods is called.

Virtual Proxy can cause an identity problem when more than one virtual proxy used for the same real object.
To prevent it need to override the equality methods and remember to use it instead of identity method.
Another one possible issue - is the necessity to create lots of virtual proxies, one for each proxying class.

To avoid those problems - use virtual proxies for collections only, since collections are Value Objects and their identity does not matter; and you only have a few collection classes to write virtual collections for.

Virtual Proxy is more suitted for Data Mapper.

## Value Holder

Value Holder - is an object that wraps some other object. Value Holder pulls the data on the first access to the value.

The disadvantages of the value holder are that the class needs to know that it’s present and that you lose the explicitness of strong typing. You can avoid identity problems by ensuring that the value holder is never passed out beyond its owning class.

## Ghost

Ghost - is the real object in a partial state.

When object is loaded from the database it contains an ID only. An access to the field loads full state of the object.

There is no need to load all the data in one go - there is an option to group load operations in groups that are commonly used together.

Ghost could be put in Identity Map - this way it maintains identity and avoids all problems due to cyclic references when reading in data.

## Pros & Cons

Pros:

- Allows to load the data when you really need it in one call.

Cons:

- Does not work well with inheritance - when using ghost/virtual proxy it is required to know what type of ghost to create which is unknown without loading all things properly.
- Can cause more database calls than needed (ripple loading) - a read of collection with Lazy Loads leads to a database call for each object instead of single call to read all of the objects. To prevent it make a collection as a Lazy Load and when needed load all the contents in a one call.
- Adding Lazy Load adds complexity.

## Example Notes

### Lazy Initialization Example

- `Supplier.GetProducts` - omitted actual data loading for example simplicity.

### Virtual Proxy Example

- `ProductLoader.Load` does not load products from the external resource
- `SupplierMapper.Load` does not load supplier from the external resource
- `VirtualList` made as generic and it acts as a proxy for `IEnumerable<T>`

### Value Holder Example

- `IValueLoader` made as generic interface
- `ProductLoader.Load` does not load products from the external resource for example simplicity
- `SupplierMapper.Load` does not load supplier from the external resource
- `Supplier.LazyProducts` shows usage of the built-in `Lazy<T>` C# implementation

### Ghost Example

- `DomainObject` is a Domain Class Supertype with loading state
- `Employee` is a specific Domain Class which loads itself in every field accessor
- `MapperRegistry` is a registry which loads domain object using appropriate `Mapper`
- `Mapper` is a Data Mapper Supertype which has `Mapper.Load` method to load domain object if it is a ghost
- `EmployeeMapper` is a Data Mapper for `Employee` to load rest of the `Employee` fields
