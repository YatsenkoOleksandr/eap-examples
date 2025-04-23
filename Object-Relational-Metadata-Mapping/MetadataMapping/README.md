# Metadata Mapping

Metadata Mapping - holds details of object-relational mapping in metadata.

Much of the code that deals with object-relational mapping describes how fields
in the database correspond to fields in in-memory objects.
The resulting code tends to be tedious and repetitive to write.

A Metadata Mapping allows developers to define the mappings in a simple tabular form,
which can then be processed by generic code to carry out the details of
reading, inserting, and updating the data.

## How It Works

The biggest decision in using Metadata Mapping is how the information in the metadata
manifests iteself in terms of running code.
There are 2 main options in manifesting the information:

- code generation;
- reflective programming.

With code generation you write a program whose input is the metadata
and whose output is the source code of classes that do the mapping.
These classes are entirely generated during the build process, usually just prior to compilation.
The resulting mapper classes are deployed with the server code.

If you use code generation, you should make sure that it's fully integrated into build process.
The generated classes should never be edited by hand
and thus shouldn't be included into source control system.

Reflective programming is usually slow, but it is quite appropriate for database mapping.
Since you are reading in the names of fields and methods from a file,
you are taking full advantage of reflection's flexibility.

Code generation is a less dynamic approach since any changes to the mapping require recompiling
and redeploying.
With a reflective approach you can just change the mapping data file and the existing classes
will use the new metadata.

Both approaches can be difficult to debug.
Generated code is more explicit so you can see what is going in the debugger.
Usually code generation is easier for less experienced developers.

On most occasions you keep the metadata in a separate file format (XML or JSON).
A loading step takes this metadata and turns it into a programming language structure,
which then drive either the code generation output or the reflective mapping.

In simpler cases you can skip the external file format and
create the metadata representation directly in source code (reflective attributes??).
However, it will make editing the metadata somewhat harder.

Another alternative is to hold the mapping information in the database otself,
which keeps it together with the data.
If the database schema changes, the mapping information is updated.

When deciding which way to hold the metadata information,
you can mostly neglect the performance of access and parsing.
For the code generation, access and parsing take place only during the build
and not during execution.
If using reflective programming, access and parse happens only once during the system startup;
then you can keep the in-memory representation.

Another biggest decision is how complex metadata will be.
There are a lot of different factors to keep in the metadata
when facing with a relational mapping problem.
It is worth evolving design as needs grow - start with simple, general scheme,
and then add new capabilities to metadata-driven software.

A simple metadata scheme ofthen work well 90 percent of the time,
there are often special cases that are more tricky.
To handle these minority cases you often have to add a lot of complexity to metadata.
A useful alternative is to override the generic code
with subclasses where the special code is handwritten.
Such special-case subclasses would be subclasses of either the generated code or the reflective routines.
As you need the overriding, alter the generated/reflective code to isolate a single method
that should be overriden and then override it in special case.
Also, it is better to handle special cases one-by-one.

## When To Use It

Metadata Mapping can greatly reduce the amount of work needed to handle database mapping.
However, some setup work is required to prepare the Metadata Mapping framework.
Also, while it's often easy to handle most cases, there are could be some exceptions that
complicate the solution.

Commercial (and OSS libraries and frameworks) object-relational mapping tools use Metadata Mapping.

If building an own system, need to evaluate the trade-offs.
Compare adding new mappings using handwritten code with using Metadata Mapping.
If you use reflection, look into its consequences for perofrmance - sometimes it could cause slowdowns.

The extra work of hand-coding can be greatly reduced by creating a good Layer Supertype
that handles all the common behavior.

Metadata Mapping can interfere with the refactoring, particularly if you are using automated tools.
If you change the name of a private field, it can break an application unexpectedly.

On the other hand, Metadata Mapping can make refactoring the database easier, since
the metadata represents a statement of the interface of your database schema.
Thus, alterations to the database can be contained by changes in the Metadata Mapping.

## Pros & Cons

Pros:

- Metadata Mapping can reduce the amount of work needed to handle database mapping.

Cons:

- Required some setup work to prepare the Metadata Mapping framework.

- There could be tricky mapping issues which will require a special handling.

    So it is recommended to use a Layer Supertype to handle a common behavior and then
    create a subclasses to resolve mapping issues as a special case.

- Automated refactoring may ignore metadata mapping code and cause failure.

- If using own code generation metadata mapping will need to update building scripts to generate the code.

- The startup may take longer due to metadata parse if using reflective metadata mapping.

## Example Notes

- [Data Annotations](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt)
  could be considered as example of Metadata Mapping.
- Original example was in Java, so it was converted to C#.
- Example demonstrates a one-to-one relationship between tables and classes using reflection.
- Omitted Unit Of Work usage for a simplicity.
