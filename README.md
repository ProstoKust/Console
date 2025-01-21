# Console
An easy to setup console for Unity 6 on the new input system

<img src="https://drive.google.com/uc?id=1UcS_1ouL4ypgntw59lxsclpXZV5smW5W" alt="Console">

# Quick start
## Installation
1. Download the latest unity package of the console in the releases.
2. Add console to scene, with add the prefab `Console/Prefabs/ConsoleCanvas.prefab` or you can add the console to an existing Canvas with a prefab `Console/Prefabs/Console.prefab`.
## Customization
- To change the appearance of the console, go to the prefab `Console/Prefabs/Console.prefab` and edit what you want.
- To change the colors of console commands, go to the prefab `Console/Prefabs/Console.prefab` and edit the color variables of the Console.cs script in the Console inspector.
- To change the start message, when starting the console, go to the prefab `Console/Prefabs/Console.prefab` and edit the Start Message variable of the Console.cs script in the console inspector.
- To change the switcher key, go to the `Console/Scripts/Switcher.cs` script and change the `Keyboard.current.`**`backquoteKey`**`.wasPressedThisFrame` for any others key.
