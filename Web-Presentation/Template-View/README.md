# Template View

Template View - renders information into HTML by embedding markers in an HTML page..

## How It Works

The basic idea of Template View is to embed markers into a static HTML page when it's written.
When the page is used to service a request, the markers are replaced by the results of some computation.
The markers then communicate with real programs to put in the results.

On of the most popular forms of Template View is a server page such as ASP, JSP, or PHP.
These actually go a step further than the basic form of a Template View in that they allow you to embed arbitraary programming logic, referred to as scriplets, into the page.

The biggest problems of embedding scriptlets into the page come from the fact that a page is poor module for a program.
Even with an object-oriented language the page construct loses you most of the structural features that make it possible to do a modular design either in OO or in procedural style.
When domain logic starts turning up on server pages it becomes far too difficult to structure it well and far too easy to duplicate it across different server pages.

The key to avoiding scriplets is to provide a regular object as a helper to each page.
This helper has all the real programming logic. The page only calls into it, which simplifies the page and makes it a more pure Template View.

Primary function of Template View is to play the view in Model View Controller.

## When to Use It

The strength of Template Voew is that it allows you to compose the content of the page by looking at the page structure.
In particular it nicely supports the idea of a graphic designer laying out a page with a programmer working on the helper.

Template View has 2 significant weaknesses:
- the common implementation make it tooeasy to put complicated logic in the page, thus making it hard to maintain;
- Template View is harder to test than Transform View.

## Examples

ASP.NET page, Razor page could be considered as Template View.

React, Angular also implement Template View pattern (on the client-side).
