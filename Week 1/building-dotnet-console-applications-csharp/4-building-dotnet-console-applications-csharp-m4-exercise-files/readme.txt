The NuGet package binaries are not included in the demos.zip solutions.

To auto restore these packages when building following the instructions below.


In the Visual Studio 2012 Quick Launch (Ctrl+Q) type: "package manager general" and hit enter.

In the Package Manager, General options:

- Enable Allow NuGet to download missing packages
- Enable Automatically check for missing packages during build in Visual Studio


Rebuilding the solution will now automatically download the NuGet packages.
