

![[High-Level Architecture-UML.png]]


🟢  Main Engine Layers

The engine consists of 5 main layers, each with a specific responsibility:

| Layer             | Responsibility                                                                 |
|------------------|-------------------------------------------------------------------------------|
| Game Loop         | The heart of the engine. Manages the game cycle: Initialize → LoadContent → Update → Draw |
| Core Engine       | Manages Entities and Components, Event System for communication between parts |
| Rendering System  | Draws Sprites and manages animations                                          |
| Input System      | Receives user input: keyboard, mouse, gamepad                                 |
| Physics System    | Applies movement, collisions, and physical rules                               |
| Assets            | Manages resources: textures and sounds                                        |
| Game Layer        | Game logic: Player, Enemy, Level, and interaction with Core and Assets        |

🟢 Classes and Components of Each Layer

Each layer is represented as a class or package. Here is a list of classes and their main responsibilities:

1. **Game Loop**

Methods:
- `Initialize()` → Sets up the engine and resources  
- `LoadContent()` → Loads textures, sounds, and levels  
- `Update()` → Updates the state of Entities, Physics, and Input  
- `Draw()` → Renders elements on the screen  

2. **Core Engine**

Classes/Components:
- `EntityManager` → Manages all Entities  
- `Component System` → Manages Components of each Entity  
- `Event System` → Sends and receives events between systems  

3. **Rendering System**

Classes:
- `SpriteRenderer` → Draws Sprites  
- `AnimationManager` → Manages animation frame changes  

4. **Input System**

Classes:
- `KeyboardInput` → Keyboard input  
- `MouseInput` → Mouse input  
- `GamepadInput` → Gamepad input  

5. **Physics System**

Classes:
- `Collision Detection` → Detects collisions  
- `Movement` → Applies movement and physics to Entities  

6. **Assets**

Classes:
- `TextureManager` → Manages texture loading  
- `SoundManager` → Manages sounds  

7. **Game Layer**

Classes:
- `Player` → Main character  
- `Enemies` → Enemy characters  
- `Levels` → Environment and levels
