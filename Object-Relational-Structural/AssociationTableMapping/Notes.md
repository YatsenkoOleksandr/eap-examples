# Association Table Mapping

Association Table Mapping - saves an association as a table with foreign keys to the tables that are linked by association.

Association Table Mapping allows to handle many-to-many association - it maps the multivalued field to the link table.

The basic idea is to use a link table to store association. This link table has no corresponding in-memory object.

To load data from the link table need to conceptually perform 2 queries:

1. Query to link table to find all related rows.
1. Query to "entity" table to find related object for each row in the link table from the previous result.

As an option, use join to get all the data in a single query.

Updating the link data involves many of the issues in updating a many-valued field.
One of the resolution is to treat the link table as Dependent Mapping.

## When to Use

The canonical case is a many-to-many association.

Association Table Mapping can be used when need to add link between existing tables, but it is not possible to add new collumns to those tables.

Sometimes association table that carries information about the relationship really corresponds to a true domain object (e.g. person/company table which contains information about person's employment within the company).

## Example Notes

1. Only `Employee` has a link to related `Skill`, while `Skill` does not have any reference to `Employee`.
1. Update of the `Employee` deletes all existing associations and creates new ones.
1. Omitted Mapper Registry for the example simplicity.
1. Mapper use ADO.NET and do not optimize querying of related `Skill`.
