Welcome to `ToTypeScriptD`
====

You can now generate [TypeScript](http://typescriptlang.org) definition (`*.d.ts`) 
files from either `.Net` or `.winmd` assembly files. Allowing you to leverage these 
libraries in your TypeScript/JavaScript `WinJS` or other client side software 
applications with all the type safety and benefits of [TypeScript](http://typescriptlang.org).

## Can you tell me why I would use this?

I know of two main scenarios where I think this could be useful.

1. If you build a 'Modern' (come on, we still call it Metro) Windows 8 app 
 with `WinJS` and want to leverage `TypeScript`, wouldn't it be nice to get 
 a set of TypeScript Definition files that reflect the native API's you're 
 calling in the platform without manually creating the definition files?
2. Say your building an MVC/WebAPI server application. It would be useful if 
 your C.I./Build system could spit out a set of TypeScript interfaces that 
 were based on the server objects used to render your API. This can provide
 not only useful for client side JavaScript/TypeScript libraries that 
 need to consume these objects, but could also provide a simple way to 
 document the structure of your service API results.

## Install

Install via [Chocolatey](http://chocolatey.org)

    > cinst ToTypeScriptD

You can see the Chocolatey package here: [ToTypeScriptD package](https://chocolatey.org/packages/ToTypeScriptD)

## How to use?

TODO: give quick how-to call the commandline tool (once it's built).


## How does it work?

By loading the assembly metadata with [Mono.Cecil](http://www.mono-project.com/Cecil) 
which can read any [Ecma 355](http://www.ecma-international.org/publications/standards/Ecma-335.htm) 
Common Language Infrastructure (CLI) file, we can generate a set of TypeScript definition 
files that allow us to project the type system from these assemblies into TypeScript.

