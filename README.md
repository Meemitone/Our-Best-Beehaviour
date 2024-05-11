
# Our Best Beehaviour
## Group Members
Thomas "Tom" Grumley - C21410212
Kate Johnston - C21762619
Rhys Mac Gregor-Mason - C21314853
Donnacha Lohan Davy - D22125453

Class Group:TU-984/3 (Game Design Year 3 for the whole group)

# Description
A large simulation of bees and plants in evolutionary harmony. Beehive use pollen to produce bees, bees fetch pollen from plants, plants produce pollen from sunlight. Both bees and plants reproduce using genetic algorithms, allowing them to evolve over the course of the simulation.


## Video:

[![YouTube](http://img.youtube.com/vi/J2kHSSFA4NU/0.jpg)](https://www.youtube.com/watch?v=J2kHSSFA4NU)

## Screenshots

# Instructions
All inputs appear as on-screen buttons.

# How it works
Bees, hives and plants decay over time. Bees pollinate plants to keep them alive, they bring the pollen to hives to keep them alive as well. Then the hives produce more bees using this pollen.

# List of classes/assets
## Tom (Level Generation and Modelling)
| Class/asset | Source | Purpose |
|-----------|-----------|-----------|
| WorldGenerator.CS | Self Written | Creates and populates the world |
| Beehive.CS | Self Writtern | Manages hive pollen count and decay |
| HornetNavigation.CS | Self Written | (Unused, was for predators) |
| ControlPanel.CS | Self Written | Manages player inputs and sliders |
| OrbitalPlayer.CS | Self Written | Orbital camera for following bees |

| Control Panel Asset | Self made in unity | Control panel and hotbar for player inputs before and during simulation |

| Bee Model | Self modelled on blender | Used in bee prefab |
| Bee Texture | Self drawn on photoshop | Used in bee prefab |
| Bee animations | Self animated in unity | (Walk, fly, idle) |
| Plant segment models | Self modelled | Used in plant growth |
| Plant segment textures | Self drawn on photoshop | Used in plants |
| 2 Mushroom Models | Self modelled | Used in mushroom variants |
| 4 Mushroom Textures | Self drawn in photoshop | Used in mushrooms |
| Skybox material | Self-made in unity | Used in simulation world |
| Basic materials | Self-made in unity | Used in beehive and world |


## Kate (Flowers)
| Class/asset | Source | Purpose |
|-----------|-----------|-----------|
| PlantPart.cs | Self written | Base Class for plant bits |
| PlantSeed.cs | Self written | Main controller for plants |
| FlowerData.cs | Self written | Class to hold plant genetic info |
| Leaf.cs | Self written | Plant Leaf inherits PlantPart |
| FollowSun.cs | Self written | Boid Behaviour to seek the sun (Environmental Data) |
| Flower.cs | Self written | Flower Head, makes pollen for bees, spawns seeds |
| WindMotion.cs | Self written | Behaviour that makes a xz force for wind |
| EnvironmentalData.cs | Self written | Holder for sun and wind info |
| Stalk.cs | Self written |  |
| MutateGlobal.cs | Self written |
| WindDisplay.cs | Self written |
| D20.asset | Self written |
| HexTorus.asset | Self written |
| Unit_Hexagonal_Prism.asset | Self written |
| Kate's Default Material.mat | Self written |

## Rhys(Bee Genetics)

## Donnacha (Bees)

| Class/asset | Source | Purpose |
|-----------|-----------|-----------|
| BasicBoid.cs | Self written | Basic boid script to be inherited, calculates total force, holds base class for Behaviour |
| BeeBoid.cs | Self written (inherit: BeeBoid) | Adds movement to object and rotation based on force |
| Arrive.cs | Self written (inherit: Behaviour) | Makes a force vector based on a location and distance from it |
| Flock.cs | Self written (inherit: Behaviour | Makes a force vector based on the nearest 3 bee's currect direction |
| HoldInArea.cs | Self written (inherit: Behaviour) | Makes a force vector to push the bee back into the simulation area if they leave |
| Seek.cs | Self written (inherit: Behaviour) | Makes a force towards a flower when one is in range and is available |
| Wander.cs | Self written (inherit: Behaviour | Makes a slight turning force based on the current direction the object is facing |
| BoxGenerator.cs | Self written | Generates boxes to map out the total space of the simulation |
| Box.cs | Self written | Basic container script that holds information about neighbours and objects contained |

   I created the scripts relating to bees movement and AI in the project, requiring them to do a number of things, including hunting down and interacting with flowers. The bees also had a habit of escaping the bounds of the simulation, so I created an additional behaviour that causes them to return to the simulation. My most proud creation of this project is the seek behaviour as it doubles as a state machine and control center for the boid. The method of doing this is checking if flowers are in the area, if found it removes the wandering, otherwise keeps the wandering going. Once in short range of the flower, it turns on the arrive behaviour to complete the landing of the boid onto the flower. A major learning point for this project is after the fact I learnt a few methods to lower the amount of stress on the computer. A grid system helps only have the bee check a minimum amount of objects to avoid unncessicary amount of calculations and avoid excessive raycasting which is costly on preformance. Some additional performance enhancements could have been preformed, for example bees giving their flocking neighbours themselves to cut down on extra calculations.


# References
* Item 1
* Item 2

# From here on, are examples of how to different things in Markdown. You can delete.  

## This is how to markdown text:

This is *emphasis*

This is a bulleted list

- Item
- Item

This is a numbered list

1. Item
1. Item

This is a [hyperlink](http://bryanduggan.org)

# Headings
## Headings
#### Headings
##### Headings

This is code:

```Java
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

So is this without specifying the language:

```
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

This is an image using a relative URL:

![An image](images/p8.png)

This is an image using an absolute URL:

![A different image](https://bryanduggandotorg.files.wordpress.com/2019/02/infinite-forms-00045.png?w=595&h=&zoom=2)

This is a youtube video:

[![YouTube](http://img.youtube.com/vi/J2kHSSFA4NU/0.jpg)](https://www.youtube.com/watch?v=J2kHSSFA4NU)

This is a table:

| Heading 1 | Heading 2 |
|-----------|-----------|
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |

