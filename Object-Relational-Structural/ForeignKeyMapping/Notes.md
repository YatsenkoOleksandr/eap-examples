# Foreign Key Mapping

Foreign Key Mapping - maps an association between objects to a foreign key reference between tables.

Foreign Key Mapping maps an object reference to a foreign key in the database.

## How Foreign Key Mapping Works

Three options how to handle update on collection of connected objects:

1. Delete and Insert - works if connected objects are Dependent Mappings, which means there no outside reference to another object/table.
1. Add a back pointer - put a link from the dependent object so that the association is biderctional.
1. Diff the collection - make a diff between database current state or first time read to object state.

Watch out for cycle references in links. E.g.:

```
Order => Customer => Payment => Order
```

Load an order for payment may trigger another database call and cycle load, but order is actually already loaded.

To avoid reference cycles:

1. Creation method should load all data and place Lazy Load at appropriate points to break the cycles
1. Creation method creates empty object and puts into Identity Map, so when cycle back the object is already loaded and cycle is ended.

## When to Use Foreign Key Mapping

Foreign Key Mapping can be used for almost all associations between classes.

But, Foreign Key Mapping is not applicable for many-to-many associations. Instead need to use Association Table Mapping.

If collection field does not have back pointer then consider "many" side to be a Dependent Mapping.

If related object is a Value Object then consider Embedded Value.

## Example Notes

### Single Value

- Skipped actual database calls in `AbstractMapper` and `AlbumMapper`.
- Skipper mapper registry.

### Collection of References

- `DataSetHolder` instance is not initialized in `AbstractMapper` for simplicity.
- Skipped mapper registry.
- Did not implement all CRUD-methods for simplicity.
- Update does not do any diff, so need to update both `Team` instances when moving `Player` to another team.
