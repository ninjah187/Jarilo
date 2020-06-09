# Jarilo

## .NET Core 2.0 command line application framework.

Have you built nice library and realized adding CLI tooling would be great but you get headache whenever thinking about all the wasted time on writing boilerplate code?

Are you going to create complex command line app and looking for structured, testable architecture?

Do you want to spin up CLI apps easily and quickly?

You may found the answer.

## Motivation:

I decided to take my CLI applications development to higher level. No more spaghetti-parsing code, no more hand-written documentation, no more unability to test my apps. The goal of this project is to develop smooth and easy workflow for creating CLI applications supporting good programming practices.

I share my work with community hoping that you can also benefit from it like I do. I'd also be really happy to see criticism and feedback from any of you.

Although I eventually picked up different approach, great inspiration for Jarilo project was this article: [Creating Neat .NET Core Command Line Apps](https://gist.github.com/iamarcel/8047384bfbe9941e52817cf14a79dc34).

## Overview:

Jarilo takes MVC approach where C stands for Command instead of Controller. This architecture lets build Jarilo apps from components which are highly decoupled and testable. Jarilo supports dependency injection and automatically generated documentation.

## Usage:

### Installation:

All released Jarilo versions can be obtained via NuGet Package Manager.

| Package | Version | Build                                                                                                               |
| ------- | ------- | ------------------------------------------------------------------------------------------------------------------- |
| Jarilo  | 1.2.0   | [![Build Status](https://travis-ci.org/ninjah187/Jarilo.svg?branch=master)](https://travis-ci.org/ninjah187/Jarilo) |

### Running application:

Jarilo app can be run in just few lines of code:

```cs
static void Main(string[] args)
{
    var app = new App();
    app.Run(args);
}
```

The `App` class takes care of building and running application. Where does all the logic go?

### Commands:

Commands are entry points for user's requests. Command at its simplest form looks like this:

```cs
[Command("hello", "Greets user.")]
class Command
{
    public void Run()
    {
        Console.WriteLine("Hello, user!");
    }
}
```

Command line example:
```
> hello
Hello, user!
```

### Arguments:

Commands can have arguments. Arguments are used by defining class with `[Argument]`ed properties and injecting that class into command's `Run()` method:

```cs
class Arguments
{
    [Argument("User's name.")]
    public string Name { get; set; }
}
```
```cs
[Command("hello", "Greets user.")]
class Command
{
    public void Run(Arguments arguments)
    {
        var userName = string.IsNullOrEmpty(arguments.Name)
            ? "stranger"
            : arguments.Name;
        Console.WriteLine($"Hello, {userName}!");
    }
}
```
```
> hello
Hello, stranger!
> hello Alice
Hello, Alice!
```

### Options:

Commands can also have options. Using options is similar to using arguments:

```cs
class Options
{
    [Option("--caring", "Ask user for mood after invitation.")]
    public bool IsCaring { get; set; }
}
```

```cs
[Command("hello", "Greets user.")]
class Command
{
    public void Run(Arguments arguments, Options options)
    {
        var userName = string.IsNullOrEmpty(arguments.Name)
            ? "stranger"
            : arguments.Name;
        Console.WriteLine($"Hello, {userName}!");
        if (options.IsCaring)
        {
            Console.WriteLine("How are you today?");
        }
    }
}
```

```
> hello --caring
Hello, stranger!
How are you today?
> hello Alice --caring
Hello, Alice!
How are you today?
```

### Parsing enums:

Enum types can be used both as arguments and options. In order to tell parser how map enum fields, enum should be defined like this:

```cs
enum Title
{
    None,

    [Value("sir", "Title for gentlemans.")]
    Sir,

    [Value("madame", "Title for ladies.")]
    Madame
}
```
```cs
class Arguments
{
    [Argument("User's title.")]
    public Title Title { get; set; }
}
```
```cs
[Command("hello", "Greets user.")]
class Command
{
    public void Run(Arguments arguments)
    {
        Console.WriteLine($"Hello, {arguments.Title}!");
    }
}
```
```
> hello
Hello, Title.None!
> hello madame
Hello, Title.Madame!
```

### Views:

Presentation layer can be decoupled from commands by introducing views.  Take a look at implementation:

```cs
class View
{
    public void Render()
    {
        Console.WriteLine("Hello, stranger!");
    }
}
```
```cs
[Command("hello", "Greets user.")]
[View(typeof(View))]
class Command
{
    public void Run()
    {
    }
}
```
```
> hello
Hello, stranger!
```

### ViewModels:

Passing data from commands to views is handled by view models. View model is an object returned by command's `Run()` method and passed to view's `Render()` method:

```cs
class ViewModel
{
    public string FormattedMessage { get; set; }
}
```
```cs
class View
{
    public void Render(ViewModel viewModel)
    {
        Console.WriteLine(viewModel.FormattedMessage);
    }
}
```
```cs
[Command("hello", "Greets user.")]
[View(typeof(View))]
class Command
{
    public ViewModel Run()
    {
        var message = "Hello, stranger!";
        return new ViewModel
        {
            FormattedMessage = $"** {message} **";
        };
    }
}
```
```
> hello
** Hello, stranger! **
```

### Dependency injection:

Jarilo uses `Microsoft.Extensions.DependencyInjection` as inversion of control container. It can be used to inject services into commands. Registering dependencies is straightforward:

```cs
class GreetingService : IGreetingService
{
    public string Greet(string userName)
    {
        return $"Hello, {userName}!";
    }
}
```
```cs
static void Main(string[] args)
{
    var app = new App();
    app.Services.AddSingleton<IGreetingService, GreetingService>();
    app.Run(args);
}
```
```cs
[Command("hello", "Greets user.")]
class Command
{
    readonly IGreetingService _greetingService;

    public Command(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }

    public void Run()
    {
        var greeting = _greetingService.Greet("stranger");
        Console.WriteLine(greeting);
    }
}
```
```
> hello
Hello, stranger!
```

### Read-Eval-Print Loop (REPL)

Jarilo supports alternative run mode - REPL. It allows users for interactive sessions with the app. Jarilo app can be run in REPL mode like that:

```cs
static void Main()
{
    var app = new App();
    app.ReadEvalPrintLoop();
}
```

### Documentation

Jarilo handles special help options `-?` `-h` `--help` which can be used with any command to print auto-generated documentation. The documentation is based on application's structure and on metadata provided in attributes.

```cs
enum Title
{
    None,

    [Value("sir", "Title for gentlemans.")]
    Sir,

    [Value("madame", "Title for ladies.")]
    Madame
}
```
```cs
class Arguments
{
    [Argument("User's name.")]
    public string Name { get; set; }
}
```
```cs
class Options
{
    [Option("--caring", "Ask user for mood after invitation.")]
    public bool IsCaring { get; set; }

    [Option("--title", "User's title.")]
    public Title Title { get; set; }
}
```
```cs
[Command("hello", "Greets user.")]
class Command
{
    public void Run(Arguments arguments, Options options)
    {
        // handle command...
    }
}
```
```
> --help
Usage:
  <command> [arguments] [options]
Commands:
  hello
  
> hello --help
Command:
  Greets user.
Usage:
  hello <name> [options]
Arguments:
  <name> - User's name.
Options:
  --caring - Ask user for mood after invitation.
  --title <value> - User's title.
    Possible values:
      sir - Title for gentlemans.
      madame - Title for ladies.
```

## Style guide and recommendations:

Please check project `Jarilo.Examples` in source code for more sophisticated examples and my preferred `directory per feature` way of structuring Jarilo applications.

## Lacking features:

- `async` support.
- Shorthand options.
- Setting return code of an app.
- Exporting documentation of application (e. g. in Markdown format).
- Rich configuration options (e.g. translations, REPL prompt etc).
- Pluggable IoC container.
- Some kind of view template engine - that could be really nice feature.
- Docs and examples of testing Jarilo apps.
- Reactive views.

Got anything on your mind? I'd feel happy to see you contribute or file an issue.
