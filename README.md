# SMT.CheatTime

**Functionality**

- [+] - Add Money
- [+] - Add Franchise Points
- [=] - No-Clip
- [+] - Speed Hack | Not added yet
- [?] - Random Cheat "I am lucky ?" | Not added yet

**Overview**
A toolkit for Supermarket Together that handles real-time resource modification, physics overrides, and custom in-game menus.

**How it works**

* **Initialization**: Uses a `winhttp.dll` proxy to load **BepInEx** into the game process during startup. This injects our code directly into the game's memory space.
* **Data Access**: Employs **C# Reflection** to locate the active `GameData` instance in RAM. It then overwrites internal game variables (funds/points) in real-time.
* **Engine Integration**: Hooks into Unity's main loop:
    * `Update`: Processes keyboard inputs and the No-Clip movement logic.
    * `OnGUI`: Draws the visual menu overlay directly over the game screen.
    * **Physics**: Toggles `Collider` and `Rigidbody` components at runtime to enable No-Clip mode.

**Installation**
Place the mod files in your `SupermarketTogether` root directory (the same folder as the .exe). The mod loads automatically on launch.

*Disclaimer: For private/educational use only. Use at your own risk.*
