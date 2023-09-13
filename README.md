
# 3d Sharp

A C# library to handle and render 3d objects using winforms.


## Features

- Fully working camera
- Simple 3d object generator (cubes and planes)
- 3d renderer
- Customizable resolution
- Camera with variable FOV
- Scenes manager
- Multi-camera support
- Runs reasonably smooth


## How it works

Honestly speaking, the camera is the heart of the entire library.
It takes in it's position and angle in 3d space, and according to it's FOV, generates a projection screen.

The rendering is quite straight forward: we take a line from the point we want to render to the exact position of the camera object. The point in which this line intersects the simulated projection screen is the point we need to see in our final render.

Although it sounds simple, I guarantee you it's much easier said than done.

The rotation of the camera is made to work like a common 1st person game, consisting mainly of pitch and yaw (vertical and horizontal movement). But the library can handle roll movement as well (rotating the field of view around it's own center), and changing the pitch and yaw according to the new rotation angle.
## Basic Structures

The library works around some general objects
- Point3d
- Triangle
- Angle
- Ratio
- Camera
- Renderer

#### Point3d

The *Point3d* is the basis of the library. It contains 3 doubles, one for each 3d axis.

#### Triangle

The triangle is the 2d polygon with the smallest number of edges possible. Therefore, it is the ideal polygon to use when constructing 3d objects.

The *Triangle* object consists of an array of Point3d objects. A triangle can be created simply by passing 3 Point3d objects as parameters.

*Every single triangle created is automatically added to the global scene that contains the "world".*

#### Angle

The *Angle* object is responsible for storing rotation values. It contains 3 doubles (one for each rotation axis).

It has public methods to change it's values and guarantee they are kept in reasonable values (from 0 to 360).

#### Ratio

The *Ratio* object contains 4 ints. They are width, height, scrWidth, and scrHeight.
The variable names are reasonable self explanatory, but to avoid any confusion:
- The width and height are values passed directly to the camera. They are the dimention of the simulated projection screen inside the scene.
- The scrWidth and scrHeight are the size of the final rendered image. So, if your program runs in HD, you should put 1280 as width and 720 as height.

#### Camera

The *Camera* object is the main part of the library. It has multiple variables that define its behavior.
Since it's too long to describe here, I recommend taking a look yourself at it.

#### Renderer

The *Renderer* object is exactly what the name implies: a renderer.
It does the entire analysis of points to be rendered in a camera, and returns a list of triangles to be drawn on screen.
