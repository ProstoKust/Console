# Console
An easy to setup console for Unity 6 on the new input system.

![ConsolePreview](https://github.com/user-attachments/assets/811eb0dc-abc5-414a-a9b8-06ccf3c56574)

# Getting Started
## Installation
1. If your project does not have Text Mesh Pro or Unity Input System, you need to install them if you do not want to break the project.
2. Download the latest unity package of the console in the releases.
3. Add console to scene, with add the `Console/ReadyConsole.prefab` or you can add the console to an existing Canvas with a `Console/Prefabs/Console.prefab`.

Great! Try to opening the console, the default key is **~**
## Customization
- To change the appearance of the console, go to the `Console/Prefabs/Console.prefab` and edit what you want.
- To change the colors of console commands, go to the `Console/Prefabs/Console.prefab` and edit the color variables of the Console.cs script in the Console inspector.
- To change the start message, when starting the console, go to the `Console/Prefabs/Console.prefab` and edit the Start Message variable of the Console.cs script in the console inspector.
- To change the switcher key, go to the `Console/Scripts/Switcher.cs` script and change the `Keyboard.current.backquoteKey.wasPressedThisFrame` for any others key.
- To add a command send button, go to `Console/Prefabs/Console.prefab` and follow the path `Console/Window/Field/Input/Send`, then switch active to true

# Documentation
## Adding message to console
To add a message to the console, we will need to use WriteConsole from the Console.cs script.

When using the command `Console.instance.WriteConsole("Hello World!");` in the start method, the console will output the word Hello World! when the console starts working.

If you want to show a message without displaying the time, use `Console.instance.WriteConsoleIgnore("Hello World!");`

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

For example, if you write:
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

You can full use Text Mesh Pro text markup. Find out more here: https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichText.html

## Adding command to console

To add a new command, create a public method with the [Command] attribute that contains the actions when the command is invoked.

For example, if you write:
```
[Command]
public void example()
{
    Console.instance.WriteConsole("Hello World!");
}
```
The name of the method will be the name of the command. Save the script and try to check if it works.

Your command should be added to the list of commands grace the [Command] attribute and when it is called, the actions specified in the method.

You can also create commands with additional attributes, such as text input, which will be displayed in Debug.Log
```
[Command(typeof(string))]
public void write(string Text)
{
    Debug.Log(Text);
}
```

You can make a simple command to destroy objects.
```
[Command(typeof(Transform))]
public void destroy(Transform Obj)
{
    Destroy(Obj.gameObject);
}
```

Or you can create command to teleport objects.
```
[Command(typeof(Transform), typeof(float), typeof(float), typeof(float))]
public void teleport(Transform Obj, float x, float y, float z)
{
    Obj.position = new Vector3(x,y,z);
}
```

If the attributes are invalid input, the command will not be executed, and an attributes error message will appear in the console.
