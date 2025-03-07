# Embedded Value

Embedded Value - maps an object into several fields of another object's table.

Many small objects make sense in an OO system that don't make sense as tables in a database.
For example, currency-aware money objects and date ranges.

An Embedded Value maps the values of an object to fields in the record of the object's owner.

## How It Works

When the owning object is loaded or saved, the dependent objects (like date range or money) are loaded and saved at the same time.
The dependent classes won't have their own persistence methods since all persistence is done by the owner.
Embedded Value is a special case of Dependent Mapping where the value is a single dependent object.

## When to Use It

The simplest cases for Embedded Value are simple Value Objects like money and date range.
Since Value Objects don't have the identity, you can create and destroy them easily without worrying about such things as Identity Maps to keep all in sync.
All Value Objects should be persisted as Embedded Value.

The tricky case is whether it's worth stroing reference objects (like order and shipping object) using Embedded Value.
Questions to consider:
1. Is shipping object has any relevance outside the context of the order?
1. Is shipping object is loaded when loading the order?
1. Is there is a need to access the shipping object(s) separately through SQL.

If mapping to an existing schema, can use Embedded Value when a table contains data that is split into more than one object in memory.
In this case need to be careful that any change to the dependent marks the owner as dirty.

In most cases only use of Embedded Value on a reference object when the association between them is single valued at both ends (1-to-1).
Occasionally may use Embedded Value if there are multiple candidate dependents and their number is small and fixed.
In this case need numbered fields for each value.
This is 

## Pros & Cons

## Example Notes