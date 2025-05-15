# Front Controller

Front Controller - a controller that handles all requests for a Web site.

The Front Controller consolidates all request handling by channeling requests through a single handler object.
This object can carry out common behavior, which can be modified at runtime with decorators.
The handler then dispatches to command objects for behavior particular to a request.

## How It Works

Front Controller handles all calls for a Web site, and is usually structured in 2 parts:
a Web handler, and a command hierarchy.
The Web handler is the object that actually receives POST/PUT/GET requests from the Web server.
It pulls just enough information from the URL and request to decide what kind of action to initiate
and then delegates to a command to carry out the action.

The Web handler is almost always implemented as a class rather than as a server page,
as it doesn't produce any response.
The commands are also classes rather than server pages; and in fact don't need any
knowledge of the Web environment, although they're often passed the HTTP information.

The Web handler can decide which command to run either statically or dynamically.
The static version involves parsing the URL and using conditional logic.
The dynamic version usually involves taking a standard piece of the URL
and using dynamic instantiation to create a command class.

The static approach has the advantage of explicit logic, compile time error checking on the dispatch.
The dynamic case allows to add new commands without changing the Web handler.

With dynamic invocation you can put the name of the command class into URL
or you can use a properties file that binds URLs to command class names.

Intercepting Filter is particularly useful pattern to use in conjuction with Front Controller.
Intercepting Filter is a decorator that wraps the handler of the front controller
allowing to build a filter chain to handle issues such as authentication, logging, etc.
Using filters allows to dynamically set up the filters to use at configuration time.

Both the handler and the commands are part of the controller.
As a result the commands should choose which view to use for the response.
The only one handler's responsibility is in choosing which command to execute.

## When To Use It

If the Web server is akward to configure the Front Controller could be considered since
only 1 Front Controller has to be configured into the Web server,
and the Web handler does the rest of the dispatching.

With dynamic commands you can add new commands without changing Web handler.

There is no need to worry about making the command classes thread-safe
since new command object is created with each request.
However, need to ensure that there aren't shared objects (such as model objects).

Front Controller allows to factor out duplicated code in Page Controller.

Front Controller can be easily enhanced at runtime with decorators (for authentication, loggin, etc.).

## Example Notes

ASP.NET Core Middleware could be considered as Front Controller / Intercepting Filter pattern application.
