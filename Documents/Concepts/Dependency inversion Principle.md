
The **Dependency Inversion Principle (DIP)** is one of the most important principles in software engineering and part of the **[[SOLID]]** principles.

## 🎯 General Definition

The **Dependency Inversion Principle (DIP)** states:

> "High-level modules should not depend on low-level modules;  
> both should depend on an **abstraction**."

## 💡 In simpler terms:

In programming, a **high-level class** usually depends on **low-level classes**.  
The problem is that if the low-level class changes, the high-level class may break.  
The Dependency Inversion Principle says this dependency should be **reversed**:  
Classes should not depend directly on each other; instead, both should depend on **an interface or abstraction**.

## ⚙️ Simple Example (without DIP ❌)

Imagine you have a **Switch** in a game that opens a **Door** when activated:

```csharp
public class Door
{
    public void Open() => Debug.Log("The door is open!");
    public void Close() => Debug.Log("The door is closed!");
}

public class Switch
{
    private Door door;

    public Switch(Door door)
    {
        this.door = door;
    }

    public void Toggle(bool isActive)
    {
        if (isActive) door.Open();
        else door.Close();
    }
}
```

🔴 The problem here is that `Switch` directly depends on the `Door` class.  
If later you want the `Switch` to activate a light or an elevator, you would have to **change the code** → violating the _Open-Closed Principle_ and _DIP_.

## ✅ Example with Dependency Inversion

We define an **interface called ISwitchable**, which anything that can be "turned on/off" will implement:

```csharp
public interface ISwitchable
{
    void Activate();
    void Deactivate();
}
```

Now, `Door` implements this interface:

```csharp
public class Door : ISwitchable
{
    public void Activate() => Debug.Log("Door opened!");
    public void Deactivate() => Debug.Log("Door closed!");
}
```

And the `Switch` class no longer depends on `Door`; it depends on `ISwitchable`:

```csharp
public class Switch
{
    private ISwitchable client;

    public Switch(ISwitchable client)
    {
        this.client = client;
    }

    public void Toggle(bool isActive)
    {
        if (isActive) client.Activate();
        else client.Deactivate();
    }
}
```

🔹 Now any object that implements `ISwitchable` (door, light, trap, laser, etc.) can work with the same `Switch` — without changing the code!  
🔹 This means: **High-level class (Switch)** and **low-level class (Door)** both depend on **an abstraction (interface)**, not on each other.


> `Source`: Level up your code with Game Programming Pattern
