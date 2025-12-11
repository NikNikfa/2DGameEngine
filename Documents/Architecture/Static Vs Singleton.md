## 1. Introduction

During the early stages of engine development, certain systems such as the `Renderer` and `AssetLoader` are implemented as **static classes** rather than as fully structured singleton-based engine systems. This document explains the rationale behind this temporary choice and outlines the plan for transitioning to a more robust architecture after the completion of the foundational sprints.

---

## 2. Why Static Classes Are Used at the Current Stage

### 2.1 Simplicity and Reduced Boilerplate

Static classes provide a direct and minimal way to expose functionality without requiring instance management, constructors, or dependency injection.  
During initial prototyping, this reduces code overhead and allows us to focus on validating core rendering and asset-loading behaviors.

### 2.2 Alignment With Early MonoGame Tutorials and Examples

Most MonoGame tutorials and starter templates use static helpers for rendering and asset loading. These patterns offer a clear and convenient way to interact with SpriteBatch and ContentManager while the engine structure is still forming.

### 2.3 Engine Architecture Not Yet Finalized

At this stage, major architecture systems (ECS, Scene Management, Rendering Pipeline, Asset Management, Event System) are still being defined.  
Introducing singleton-based system objects too early would require reworking large sections of the code as these systems evolve.

### 2.4 Focus on Sprint Goals

The current sprints are dedicated to:

- Establishing basic engine functionality
    
- Defining architecture
    
- Creating foundational systems
    
- Documenting patterns and principles
    

Implementing full singleton-based systems prematurely would shift focus away from sprint deliverables.

---

## 3. Limitations of Static Classes

While static classes are useful for early development, they impose structural limitations that are incompatible with the long-term engine design:

- Cannot implement interfaces
    
- Cannot participate in polymorphism
    
- Cannot be instantiated or replaced (e.g., for testing or backend changes)
    
- Cannot maintain controlled state lifecycle
    
- Cannot be injected as dependencies into systems or scenes
    
- Cannot be extended through inheritance
    

These constraints make static classes unsuitable for final engine subsystems such as rendering, input, physics, and asset management.

---

## 4. Future Plan: Transition to Singleton-Based Engine Systems

After completing the current core sprints (architecture, ECS, rendering groundwork, input system), we will refactor static classes into proper engine systems.

The plan includes:

### 4.1 Replace `Renderer` Static Class

Replace with a singleton-based `RenderingSystem` that:

- Implements an `IRenderer` interface
    
- Maintains internal render state (cameras, layers, pipelines)
    
- Wraps SpriteBatch in a structured system
    
- Supports testing, mocking, and backend abstraction
    

### 4.2 Replace `AssetLoader` Static Class

Replace with a singleton-based `AssetManager` that:

- Implements an `IAssetProvider` or `IAssetManager` interface
    
- Provides caching and lifecycle management
    
- Integrates with the Content Pipeline
    
- Allows potential hot-reloading or custom asset backends
    

### 4.3 Controlled Initialization

Singleton systems allow:

- Initialization order control
    
- Context-aware construction (injecting GraphicsDevice, ContentManager, etc.)
    
- Safe teardown and memory cleanup
    

### 4.4 Integration With ECS and Scene Management

Once converted to systems, these components will become part of the engine’s central update and rendering flow, making them consistent with the rest of the architecture.

---

## 5. Conclusion

Static classes are being used temporarily because they simplify early development, align with prototype needs, and reduce architectural burden during foundational sprints. However, they are not suitable for a fully modular, scalable engine.

After completing the core architecture sprints, these static classes will be refactored into proper singleton-based systems that:

- Support interfaces
    
- Allow dependency injection
    
- Maintain internal state
    
- Integrate cleanly into the ECS and Scene systems
    
- Follow SOLID principles and engine design best practices
    

This two-stage approach allows rapid early progress while ensuring long-term architectural correctness.