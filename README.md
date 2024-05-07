
# Our Best Beehaviour
## Group Members
Thomas "Tom" Grumley - C21410212
Kate Johnston - C21762619
Rhys Mac Gregor-Mason - C21314853
Donnacha Lohan Davy - D22125453

Class Group:TU-984/3 (Game Design Year 3 for the whole group)

# Description

## Video:

[![YouTube](http://img.youtube.com/vi/J2kHSSFA4NU/0.jpg)](https://www.youtube.com/watch?v=J2kHSSFA4NU)

## Screenshots

# Instructions

# How it works

# List of classes/assets
## Tom (Level Generation and Modelling)

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

| Class/asset | Source |
|-----------|-----------|
| MyClass.cs | Self written |
| MyClass1.cs | Modified from [reference]() |
| MyClass2.cs | From [reference]() |

Each team member or individual needs to write a paragraph or two explaining what they contributed to the project

- What they did
- What they are most proud of
- What they learned

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

