# EditorUI

**Immediate-mode editor UI framework for MonoGame.**

EditorUI is a lightweight, immediate-mode UI library designed specifically for building in-game editor tools and custom interfaces in **MonoGame** projects. It provides a layer-based rendering system, custom controls, and flexible layout options to make building editor tools fast and structured.

---

## Features

- Immediate-mode architecture
- Layer-based rendering
- Custom control system
- Layout management for editor panels
- Input handling utilities
- Fully MonoGame-compatible

---

## Getting Started

1. Add the EditorUI project to your solution, or install via NuGet:

```bash
dotnet add package EditorUI --version 1.0.0
```

2. Initialize a new Instance of ```UIManager```. Pass in a reference to Game1, and the name of a ```DefaultFont``` that will be used by the UI. Don't forget to call the 
```UIManager.Instance.Update(gameTime);``` or the ```UIManager.Instance.Draw()```. There is no need to wrap ```UIManager.Instance.Draw``` between spritebatch.begin & spritebatch.end() 

```csharp
new UIManager(this, "font");

//Update();
UIManager.Instance.Update(gameTime);

//Draw()
UIManager.Instance.Draw();
```

3. Only ```Window``` & ```Menu``` controls can be added directly to ```UIManager```. The below code adds a Window with a Button & Dropdown. 

```csharp
        Window _testWindow = new Window("Test Window")
        {
            Position = new Point(100, 100),
            Size = new Point(400, 300),
            Layout = new RowLayout(),
            Padding = 5
        };

        UIManager.Instance.Add(_testWindow);//add the widow to the UIManager


        Button _button = new Button
        {
            Text = "This is a Button"
        };
        _testWindow.Children.Add(_button);//add a button to the Window

        DropDown _dropDown = new DropDown();

        for (int i = 0; i < 5; i++)//add some items to the DropDown
        {
            _dropDown.Add($"Item: {i}");
        }
        _testWindow.Children.Add(_dropDown);//add a dropdown to the Window
```


Windows can be ```Docked``` to the ```Left, Right, Top, Bottom, Fill, None``` of the screen. Dragging a window will automatically set it's 
```Dock = None```

To set a Window's dock property: 

```csharp
        Window _testWindow = new Window("Test Window")
        {
            Position = new Point(100, 100),
            Size = new Point(400, 300),
            Layout = new RowLayout(),
            Dock = DockStyle.Left, //this docks the window to the left of the screen
            Padding = 5
        };
```


A wiki is in the works!