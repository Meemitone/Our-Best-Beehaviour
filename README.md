
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

[![YouTube](https://i.ytimg.com/vi/th-IaddH6yU/maxresdefault.jpg)](https://youtu.be/Ed8B2kmqq64)

## Screenshots

![image](https://github.com/Meemitone/Our-Best-Beehaviour/assets/114917172/6f10cbb5-e58c-453e-ad7c-b8d41cfc3538)
![image](https://github.com/Meemitone/Our-Best-Beehaviour/assets/114917172/db862006-474c-45f6-8e54-685cc8826535)
![image](https://github.com/Meemitone/Our-Best-Beehaviour/assets/114917172/d71f5f22-fb5c-42fa-8f02-fc930d6c2be0)
![image](https://github.com/Meemitone/Our-Best-Beehaviour/assets/114917172/ea6ac8b8-7bc6-498a-b5d9-172071947765)

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

My role on this project was to produce all of the art for the game. So each element had to be modelled, textured and some animated. I did all of this by myself in blender and photoshop. Having little experience texturing models I had to learn quickly to pull things together, but everything worked out well, even if the texture sheet is a mess. Initially this project was to have hornet predators which would hunt down the bees and provide an adversary to their evolution. I bega the project writing a navigation script for these boids, it relied on flocking behaviours however did not employ cell space partitioning, as such, the number of these boids capped out at around 200 before the simulation slowed down. This taught me the significant value in cell space partitioning and how important it is not to have many objects checking large lists of other objects every frame. After abandoning this script, for it's code to be salvaged and repurposed in the bee script, I turned towards the art side of the project. 

I began modelling everything, ensuring the plant models were broken down into seperate prefabs for use by the plant growth. I created a model for the bees, making sure the poly count was low enough to make it inexpensive. I then UV mapped the models and hand-drew a texxture for the model. I repeated this process for the mushrooms, creating many different variations to make the scene more decorated.

After this, I hopped into unity to begin pulling all of the elements of the game together. At this point, some of my teammates work was ready to go, so I began scripting a world generator to build and decorate a world for the simulation. I also made player-controls and UI scripts such that the player could interact with the generation and simulation. I created hive scripts to manage the pollen count of hives and to allow the hives to die off if unfed. As a final touch I made an orbital camera script to allow players to look at individual bees.

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
| Stalk.cs | Self written | PlantPart that sways |
| MutateGlobal.cs | Self written | A simple singleton to hold mutation values. Honestly should be a scriptable object |
| WindDisplay.cs | Self written | Debug script to display wind at point |
| D20.asset | Self written | Mesh I made through script, originally as a placeholder |
| HexTorus.asset | Self written | Placeholder mesh made via script |
| Unit_Hexagonal_Prism.asset | Self written | Placeholder mesh made via script |
| Kate's Default Material.mat | Self made | Render pipeline dislikes Unity's default for some reason |

So the third commit in the repo (Kate Import) was me importing the work I had started outside the project because we hadn't settled on a unity version yet (it was very early days), so this project doesn't have my mesh generators in it (the D20, a isocahedron, was difficult). So I had started work quite early on the flowers, but they weren't reliant on much of anything else. I created the stalk first, messing around with a harness that set it to random rotations and a long segment only plant looked like a tentacle, which disturbed people I showed it to. I'm glad that the behaviours I made for it later made it less creepy. Next up I focused on the leaves, I did some math using a hexagon as a base (because bees), so they use 7 raycasts to determine how much of their light is blocked, and the center raycast is more important than the outer ones. Next I looked at the seed, which isn't a plantPart. The seed is the main controller of the entire plant, it tracks the energy used by the plant (every plantPart uses energy, using the formula size\*constant\*isGrowing?growingRate:1, but the leaves also produce energy by override this upkeep function), it grows the plant and ungrows it if energy runs out or the flower produces seeds. Finally, I set to work on the flower. I'd done the genetics for the flower at this point, as part of the seed, so this was mostly just making them spawn seeds, trigger the seed to ungrow, and having space for the bees to interact.
Incorporating the wind and sun behaviours into the plant to make a bunch of flowers that behave similarly but are a little bit off from each other in a natural way was very satisfying when it all panned out, and I learned a lot about applying polymorphic principles via the base PlantPart class.

## Rhys(Bee Genetics)
| Class/asset | Source | Purpose |
|-----------|-----------|-----------|
| HiveGeneticsManager.cs | Self written | Managment script for bee genetics |
| BeeneticAlgorithm.cs | Self written | Holding place for individual bee stats |

The majority of the work was placed in the hive managment script. the hive was where each bees genetics were stored when a bee entered a hive and they were used when deploying bees from the hive or reproducing new bees. the beeneticAlgorithm script simply held the individual bees genes and was referenced by other scripts on the bee to assign stats such as their speed, weight and strength. My proudest achievement in this project was creating the realistic reproduction, It worked by taking two bees currently in the hive and picking random stats from each until all the components needed for a new bee were present, it then added a mutation factor to avoid a bee having the same genetic code as a single parent but also to simulate evolution as desirable mutations were more likely to succeed and reproduce further. If theres one thing i learned from this project it is how to code in a way that allows for oher scripts to read information from it. Through the many iterations of the hive script i learned ways to improve my code so that it would lend itself better to communicating with other scripts and reading information from them.

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

