# Console
An easy to setup console for Unity 6 on the new input system.

![ConsolePreview](https://github.com/user-attachments/assets/811eb0dc-abc5-414a-a9b8-06ccf3c56574)

# Quick start
## Installation
1. If your project does not have Text Mesh Pro or Unity Input System, you need to install them if you do not want to break the project.
2. Download the latest unity package of the console in the releases.
3. Add console to scene, with add the prefab `Console/Prefabs/ConsoleCanvas.prefab` or you can add the console to an existing Canvas with a prefab `Console/Prefabs/Console.prefab`.

Great! Try to opening the console, the default key is `~`.
## Customization
- To change the appearance of the console, go to the prefab `Console/Prefabs/Console.prefab` and edit what you want.
- To change the colors of console commands, go to the prefab `Console/Prefabs/Console.prefab` and edit the color variables of the Console.cs script in the Console inspector.
- To change the start message, when starting the console, go to the prefab `Console/Prefabs/Console.prefab` and edit the Start Message variable of the Console.cs script in the console inspector.
- To change the switcher key, go to the `Console/Scripts/Switcher.cs` script and change the `Keyboard.current.backquoteKey.wasPressedThisFrame` for any others key.

# Documentation
## Adding message to console
To add a message to the console, we will need to use WriteConsole from the Console.cs script.

When using the command `Console.instance.WriteConsole("Hello World!");` in the start method, the console will output the word Hello World! when the console starts working.

For example, if you write:
```
void Update()
{
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        Console.instance.WriteConsole("Hello World!");
    }
}
```
Then, when you press the space bar, a new Hello World! message is displayed in the console.

You can output anything to the console, as in Debug.Log, for example:
```
public int count;

void Update()
{
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        count++;
        Console.instance.WriteConsole($"+1\nCurrent Balance is {count}");
    }
}
```
Then, when you press the space bar, new message is output to the console with the count number and adds 1 to count.
