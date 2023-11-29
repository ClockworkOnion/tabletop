# Introduction (to the paper and some concepts)

This section will give a brief introduction into the history of online computer gaming, with a focus on the adaption of tabletop games into a computer format.
It will also contrast computer games with physical tabletop games, and touch upon some concepts that are specific to a subset of tabletop games: tabletop RPGs. A basic understanding of tabletop RPGs is required, as many aspects of this paper deal with the adaption of RPG rules into a virtual environment.

Ever since computers have been networked to each other, beginning with mainframes in the 70s, there have been games implemented on them. As such the history of online gaming dates back to the 70s.
> [citations, examples from CRPG Book]

Since graphics weren't available in terminals, the graphics-rich games that are popular today weren't possible yet, these early examples of multiplayer games often emulated tabletop RPGs, with their heavy emphasis on representing the game world in numbers and textual descriptions. [give examples]

With ever growing options in terms of graphics, the online gaming landscape has developed towards players directly controlling an avatar within a virtual world, the rules of which are governed by the computer itself. Players cannot change or circumvent the basic rules of the game, at least not without employing considerable effort such as manipulating data sent over the network.

Physical Board games, by contrast, rely on the players agreeing upon the games rules, possible deviations from rules as laid down in the game's manual, and things like turn order.
Since players have complete physical control over their gaming materials, it's possible to change the rules, or make up additional gameplay mechanics and elements.
Tabletop RPGs rely on a central player, a game master, coming up with gameplay scenarios and making ad hoc decisions about the complex and sometimes ambiguous rules.

Also called "Pen and Paper", these games make heavy use of the eponymous tools to facilitate a lot of their gameplay. The flexibility of a pencil and eraser for, to put it in broad terms, "storing and manipulating data" on a sheet of paper, and the ease of making these data available to other players is deceptively difficult to replicate in a virtual environment.
Examining the ways this can be done, while at the same time incorporating the strengths of a computer to deal with calculations

## On the concept of playfulness

example: Hearthstone clicking on stuff on the table, Diablo clicking on the cows

# Terms used

- Game master
- Token (anything inside the virtual environment that represents a physical object, e.g. playpieces, dice, cards ...)
- Dice notation


# Existing virtual tabletops

This section will introduce [number] existing virtual tabletop systems and examine their standout features, advantages as well as shortcomings.


## Udonarium

Udonarium is available via a web browser and doesn't require a login or user accounts. It allows users to easily upload assets and connect to each other via a session ID. As there is no central server to host play sessions, the only way to store a session is to download the current state of the session as a whole and save it locally, for which the UI provides an option.

There are no "roles" attributed to users. This means there is no concept of ownership of individual tokens, and Udonarium doesn't distinguish between players and a gamemaster in regards to who is allowed to manipulate which token. This, of course, reflects the conditions of a physical table, where each player can pick up any token, but it's a noteable difference between Udonarium and other virtual tabletops.

Udonarium further sets itself apart by presenting the table in a 3D view. Although simplistic, this much closer to a physical table than the 2-dimensional top-down view of other virtual tabletops.
The mouse allows camera placement and the manipulation of tokens by "grabbing" them with the cursor. The placement of tokens and rolling of dice, by way of double-clicking, is accompanied by sound effects that help making interaction feel tactile. To elaborate: The way Udonarium handles token manipulation creates a strong association between the movement of tokens inside the virtual environment and and an actual, physical environment.
Udonarium sets itself apart from other virtual tabletops by not having a way to process formulas. That is, every bit of arithmetic needs to be done by players outside of Udonarium. The system itself only provides text areas to note down the results, and random number generators by way of dice tokens. Since many common formulas in tabletop RPGs involve a number of dice, the playing area will quickly become littered with dice. And since no restriction is placed on the manipulation of dice, players not on their turn will often spend idle time by "playing" with unused dice: lining them up, rolling or sorting them.
These activities bear no influence on the game itself, but they are a central part of the "playfulness" mentioned in the introduction section.


While only a Japanese language version exists, the interface is simple enough to use at a basic level even without the ability to read Japanese text. Besides camera movement and the positioning and rotation of tokens, users can create new token or dice, create and edit text boxes that exist within the coordinate system of the environment.
In addition, each token has a number of attributes that can be freely edited to store game related information such as points or currency.
What's missing is a way to combine the stored attributes using formulas, or a way to automate common calculations, such as attack rolls in an RPG.


### Advantages:
- Easy and intuitive to use
- High "play factor"

### Disadvantages:
- Fixed pivot point
- Limited 3D capabilities
- Very limited options to store and manipulate data

## Roll20

### Advantages
### Disadvantages

## Foundry

### Advantages
### Disadvantages

## Tabletop simulator, OneDnD official...?
TBD

### Perhaps a table listing core features of each system?

# More ideas

- Increase playfulness: Idle games, direct token manipulation to "act" with the token
What could be ways to "have fun" with the environment? (Hitting trees. See Hearthstone for example), Cows that moo when double clicked

- Combine tactile dice rolling and formulas: Hit a formula -> Be prompted to roll a dice -> Roll gets inserted into the formula!