# Implementation Documentation / Reflection

## Overview

In this project, I created my own vector mathematics library in C++.  
I compiled it as a **DLL**.  
Then I connected it to **Unity**.

The library supports:

- **Vector2** (2D vectors)
- **Vector3** (3D vectors)
- Basic operations
- Advanced operations
- Utility functions

I also created:

- Unit tests in C++ (Visual Studio)
- A performance test system in Unity
- A comparison between my DLL and Unity built-in vectors

This project connects **C++** and **Unity** together.

---

## Why Vectors and Matrices Matter in Game Development

Vectors are everywhere in games.

We use vectors to represent:

- **Position**
- **Direction**
- **Velocity**
- **Acceleration**
- **Forces**
- **Normals**

Without vectors, we cannot move objects in 2D or 3D space.

- **Dot product** is used for:
  - Checking angles
  - **Light calculations** – *for example, diffuse lighting in Unity uses the dot product between the light direction and the surface normal to determine light intensity on each fragment.*
  - AI vision
  - Projections

- **Cross product** is used for:
  - Perpendicular directions
  - Surface normals
  - 3D rotations

- **Normalization** is important because:
  - We need unit direction vectors
  - Movement must not depend on distance

- **Interpolation (Lerp and Slerp)** is used for:
  - Smooth movement
  - Camera transitions
  - Animation blending

- **Reflection** is used for:
  - Bouncing objects
  - Physics collisions

- **Movement functions** like MoveTowards are used for:
  - AI movement
  - Player movement
  - Smooth transitions

**Matrices** are also important in games.  
They are used for:

- Transformations
- Rotation
- Scaling
- Projection

In engines like Unity, matrices allow combining multiple transformations into one. For example, to rotate and then scale a 3D model, 
the engine multiplies the rotation matrix by the scale matrix, obtaining a single matrix that applies both operations at once. 
This avoids performing two separate calculations per vertex, which is crucial for performance.  
Similarly, the camera view and projection are represented as matrices. By multiplying the model's world matrix with the view and projection matrices, 
each vertex is transformed from its local space to screen space in a single step.

Even if we do not directly see matrices, Unity uses them internally for every object's transform, for rendering, and for physics.

Vectors and matrices are the foundation of game mathematics.

---

## Implementation Details

### 1. C++ Library Design

I created two structs:

- Vec2
- Vec3

They only store float values.

I used extern "C" to avoid name mangling.  
I used __declspec(dllexport) to export functions.

All functions are **pure functions**.  
They do not modify input values.  
They return new vectors.

### 2. Operations Implemented

For both Vector2 and Vector3 I implemented:

#### Basic Operations

- Add
- Subtraction
- Scale
- Divide

#### Magnitude and Distance

- Magnitude
- MagnitudeSquared
- Distance
- DistanceSquared

I use squared magnitude when possible.  
It avoids calling sqrt.  
This improves performance.

#### Dot and Cross

- **Dot product**:  
  a.x * b.x + a.y * b.y (+ a.z * b.z)

- **Cross product in 3D**:  
  Perpendicular vector

- **Cross product in 2D**:  
  Returns a scalar.

#### Normalization

To normalize:

1. Compute magnitude  
2. Divide vector by magnitude  

I check for small values using epsilon.  
This avoids division by zero.

#### Interpolation

- **Lerp**:  
  a + (b - a) * t

- **Slerp**:  
  Uses dot product, acos, sin and cos.  
  Clamps dot between -1 and 1.  
  Slerp works on unit vectors.

#### Projection

- **Scalar Projection**:  
  dot(a, b) / |b|

- **Vector Projection**:  
  dot(a, b) / |b|² * b

I check for zero magnitude to avoid errors.

#### Reflection

Reflection formula:  
v - 2 * dot(v, n) * n
This is used for bounce effects.

#### Movement Functions

- **MoveTowards**:  
  Compute direction, clamp distance, move by maxDistance

- **ClampMagnitude**:  
  Normalize, multiply by max length

- **DirectionTo**:  
  Subtract, normalize

These functions are useful in gameplay logic.

### 3. Safety and Stability

I used epsilon checks in many functions.  
Examples:

- Division
- Normalize
- Angle
- Projection
- IsZero
- Approximately

This prevents:

- Division by zero
- Floating point errors
- Instability

Floating point precision is important in game development.

### 4. Unit Testing in Visual Studio

I created a test project.  
I used:

- assert
- Custom FloatEquals
- Epsilon comparison

Each function was tested.  
Examples:

- Add returns correct values
- Magnitude of (3,4,0) equals 5
- Dot of perpendicular vectors equals 0
- Angle between X and Y axis equals 90 degrees

All tests must pass before using the DLL in Unity.  
This ensures correctness.

### 5. Unity Integration

I created:

- C# struct Vec2
- C# struct Vec3

They match the C++ memory layout.  
I used:  
[StructLayout(LayoutKind.Sequential)]

I imported the DLL functions using:  
[DllImport("VectorMathematics")]

For bool return values, I used:  
[return: MarshalAs(UnmanagedType.I1)]

This ensures correct interop between C++ and C#.

I also created conversion functions:

- FromUnityVector3
- ToUnityVector3
- FromUnityVector2
- ToUnityVector2

This allows easy comparison with Unity’s built-in vectors.

### 6. Performance Testing in Unity

I created a PerformanceTest MonoBehaviour.  
It allows:

- Selecting which operation to test
- Setting number of iterations
- Comparing DLL vs Unity built-in

I used Stopwatch to measure time.

This allowed me to analyze:

- Overhead of DLL calls
- Cost of sqrt
- Cost of trigonometric functions
- Cost of normalization

This gave practical understanding of performance.

---

## Deeper Implementation Analysis

### DLL Call Overhead

Calling a C++ DLL from C# has overhead.  
For very small operations like Add, Unity may be faster.  
This is because:

- DLL calls cross managed/unmanaged boundary
- There is marshalling cost

For heavy math operations:

- Difference may be smaller
- Cost of math dominates

This shows that architecture matters.

### Floating Point Precision

Many functions use acos, sqrt, sin, cos.  
These functions:

- Are expensive
- Can produce small precision errors

Clamping dot between -1 and 1 is important.  
Without clamping, acos can return NaN.  

This is a common real-world issue in game math.

### Code Structure

The code is:

- Modular
- Reusable

Functions do not depend on global state.  

### Comparison with Unity

Unity already has:

- Vector2
- Vector3
- Many math functions

This project helped me understand:

- How those functions work internally
- What Unity probably does behind the scenes
- Why squared magnitude is often used

Now I better understand Unity math.

---

## Reflection on Learning

This project helped me understand:

- How vectors really work
- How dot and cross products behave
- Why normalization is important
- How floating point precision affects games

I learned how to:

- Export a DLL in C++
- Use extern "C"
- Use __declspec(dllexport)
- Use DllImport in C#
- Marshal structs correctly
- Handle bool across languages
- Test math functions properly

I also learned about:

- Performance measurement
- Clean API design
- Defensive programming with epsilon

Before this project, I used Unity vectors without thinking.  
Now I understand the mathematics behind them.

This project improved:

- My C++ skills
- My Unity knowledge
- My understanding of game mathematics
- My debugging skills
- My testing discipline

It gave me deeper confidence in low-level systems.

---

## Final Conclusion

Vectors are fundamental in game development.

Understanding them deeply makes a better game programmer.

This project helped me to understand how game engines work internally.

Now I can:

- Implement custom math systems
- Debug vector problems
- Optimize calculations
- Understand performance trade-offs
