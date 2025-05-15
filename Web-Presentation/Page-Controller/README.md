# Page Controller

An object that handles a request for a specific page or action on a Web site.

Page Controller has one input controller for each logical page of the Web site.
That controller may be the page itself, or it may be a separate object that corresponds to that page.

## How It Works

The basic idea is to have one module on the Web server act as the controller for each page on the Web site.
In practice, it doesn't work out to exactly one module per page, since you may hit a link sometimes
and get a different page depending on dynamic information.
The controllers tie in to eacg action, which may be clicking a link or a button.

The Page Controller can be structured either as a script, or as a server page (ASP, PHP, JSP, etc.).
Using a server page usually combines the Page Controller and a Template View in the same file.
This works well for a Template View, but less well for the Page Controller,
because it requires to properly structure the module.
In such cases helper object will come in use. Server page calls the helper object to handle all the logic.
The herlper may return control to the original server page, or it may forward to a different server page
to act as the view, in which case the server page is the request handler but most of the controller logic
lies in the helper.

Another approach is to make a script the handler and controller.
The Web server passes control to the script; the script carries out the controller's responsibilities
and forwards to an appropriate view to display any results.

The basic responsibilities of a Page Controller are:

- Decode the URL and extract any form data to figure out all the data for the action.
- Create and invoke any model objects to process the data.
  All relevant data from the HTML request should be passed to the model so that
  the model objects don't need any connection to the HTML request.
- Determine which view should display the result page and forward the model information to it.

Any URLs that have little or no controller logic can be handled with a server page.
Any URLs with more complicated logic go to script.

## When To Use It

The main decision point is whether to use Page Controller or Front Controller.

Page Controller leads to a natural structuring mechanism where particular actions are handled by particular server pages.

Page Controller works well in a site with simple controller logic.
In this case most URLs can be handled with a server page and the more complicated cases with helpers.

There could be a mix of Front Controller and Page Controller patterns.

## Examples

ASP.NET Core Razor Pages framework could be considered as Page Controller pattern application.
