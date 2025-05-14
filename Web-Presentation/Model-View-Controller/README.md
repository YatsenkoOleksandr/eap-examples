# Model View Controller

Splits user interface interaction into 3 distinct roles.

MVC started as a framework for the Smalltalk platform.

## How It Works

**Model** - is an object that represents some information about the domain.
It is the object that contain all the data and behavior other than that used for the UI.
In its most pure OO form the model is an object within Domain Model.

**View** - represents the display of the model in the UI.
The view is an HTML page (web) or a frame of UI widgets (desktop).

**Controller** - takes user input from the view,
manipulates the model, and causes the view to update appropriately.

MVC has 2 principal separations:

- separating the presentation form from the model;
- separating the controller from the view.

A key point is the direction of the dependencies: the presentation depends on the model.
With a rich-client interface of multiple windows it’s likely that
there will be several presentations of a model on a screen at once.
If a user makes a change to the model from one presentation,
the others need to change as well.
To do this without creating a dependency you usually need an implementation
of the Observer pattern, such as event propagation or a listener.
The presentation acts as the observer of the model: whenever the model changes
it sends out an event and the presentations refresh the information.

## Example Notes

ASP.NET (Core) MVC could be considered as an example of MVC pattern application.
