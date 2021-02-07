# Denxorz.InputOutputSnappingCanvas

[![Build status](https://github.com/denxorz/InputOutputSnappingCanvas/workflows/.NET%20Core/badge.svg)](https://github.com/denxorz/InputOutputSnappingCanvas/actions) [![NuGet](https://buildstats.info/nuget/Denxorz.InputOutputSnappingCanvas)](https://www.nuget.org/packages/Denxorz.InputOutputSnappingCanvas/) [![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/denxorz/InputOutputSnappingCanvas/blob/master/LICENSE.md)


## What does it do?
A WPF Canvas which allows easy connecting elements. Each element on the canvas can have zero or multiple in and outputs. These inputs and outputs will snap together.


## Example

![Denxorz.InputOutputSnappingCanvas sample gif](https://github.com/denxorz/InputOutputSnappingCanvas/raw/master/sample.gif "Denxorz.InputOutputSnappingCanvas sample gif")


## Tools and Products Used

* [WPF.JoshSmith.Controls.DragCanvas](https://github.com/denxorz/WPF.JoshSmith.Controls.DragCanvas) which is distributed under 'The Code Project Open License (CPOL) 1.02'
* [Microsoft Visual Studio Community](https://www.visualstudio.com)
* [Icons8](https://icons8.com/)
* [NuGet](https://www.nuget.org/)
* [GitHub](https://github.com/)
* [ScreenToGif](https://www.screentogif.com/)
* [AppVeyor](https://www.appveyor.com/)


## Versions & Release Notes

version 1.5.1:
* Add net5.0 target

version 1.5:
 * Fixes #2, "removed links" are no longer used to build groups (caused InvalidOperationException)

version 1.4: 
 * Expose snapping thresholds
 * Add a method to create links whithout snapping (so you can have groups after loading)
 * Fix bug where changed events are raised when no change occured

version 1.3: 
 * Fix bug that caused imprecision while snapping inputs
 * Add a method to retrieve groups

version 1.2: 
 * Connections can now be Cancelled
 * Expose Input and Ouput changed events

version 1.1: 
 * Update DragCanvas so that Buttons/Comboboxes/etc can be used on a draggable control

version 1.0: 
 * First version
