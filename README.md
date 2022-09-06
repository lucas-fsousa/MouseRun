# Mouse Run

A helper for mouse manipulation involving clicks and movements.

## Installation

To install, just run the C# compiler to generate the .dll file and once the file has been generated, just add the reference to the project or use [Nuget](https://www.nuget.org/packages/PublicUtility.MouseRun) or in nuget console, use the following command:


```bash
install-Package PublicUtility.MouseRun
```

## Usage

```csharp
using PublicUtility.MouseRun;

var pos = Mouse.GetPosition(); // gets the current cursor position.

Mouse.MoveTo(500, 40); // moves the mouse to the X and Y point of the screen

Mouse.MoveToAndClick(500, 40); // moves the mouse to the X and Y point of the screen and executes a click

Mouse.Drag(new PointIntoScreen(10, 10), new PointIntoScreen(50,50)); // performs the action of dragging the mouse from one point to another point

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
