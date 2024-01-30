The first section is an introduction to tabletop gaming on the computer, to give an outline of the topic this paper deals with, as well as introduce some of the vocubulary used in the context of tabletop roleplaying.

In section [3], a number of existing tabletop systems will be examined and compared, with the goal to find the advantages of each of these systems.

Building on the results of section [3], section [4] will then introduce the project I implemented for this paper and outline the goals as well as design decisions of the implementation.

Finally, in section [5], the implemented project will be critically examined and compared to the systems examined in [3].

# Introduction (to the paper and some concepts)

This section will give a brief introduction into the history of online computer gaming, with a focus on the adaption of tabletop games into a computer format.
It will also contrast computer games with physical tabletop games, and touch upon some concepts that are specific to a subset of tabletop games: tabletop RPGs. A basic understanding of tabletop RPGs is required, as many aspects of this paper deal with the adaption of RPG rules into a virtual environment.

Ever since computers have been networked to each other, beginning with mainframes in the 70s, there have been games implemented on them. As such the history of online gaming dates back to the 70s.
> [citations, examples from CRPG Book]

Since bitmap graphics weren't available in terminals, the graphics-rich games that are popular today weren't possible yet. These early examples of multiplayer games often emulated tabletop RPGs, with their heavy emphasis on representing the game world through textual descriptions and numbers. [give concrete examples? CRPG book?]

Virtual tabletop systems as they exist today occupy a space that has some overlap with both physical tabletop games, as well as computer games or video games.

With ever growing options in terms of graphics, the online gaming landscape at large has developed towards players directly controlling a single avatar within a virtual world, the rules of which are governed by the computer itself. Players cannot change or circumvent the basic rules of the game, at least not without employing considerable effort such as manipulating data sent over the network.

Physical board games, by contrast, rely on the players agreeing upon the games rules, or possible deviations from the rules as laid down in the game's manual, and things like turn order.
Since players have complete physical control over their gaming materials, it's possible to change the rules, or make up additional gameplay mechanics and elements.
Tabletop RPGs rely on a central player, a game master, coming up with gameplay scenarios and making ad hoc decisions about the complex and sometimes ambiguous rules.

Also called "Pen and Paper", these games make heavy use of the eponymous tools to facilitate a lot of their gameplay. The flexibility of a pencil and eraser for, to put it in broad terms, "storing and manipulating data" on a sheet of paper, and the ease of making these data available to other players is deceptively difficult to replicate in a virtual environment.
Examining the ways this can be done, while at the same time incorporating the strengths of a computer to deal with calculations

## On the concept of playfulness

example: Hearthstone clicking on stuff on the table, Diablo clicking on the cows

# Terms used

This section will define a number of essential terms, which are necessary to understand the following sections.
Most of these terms are common in the lingo of tabletop RPGs, and this paper's definition won't deviate from their common use. However, this section will go over some of the distinctions between the usage of these terms when referring to physical tabletops, versus their use in a virtual tabletop.

- Game master
Unlike traditional board games, where all participants are players with equal rights and roles, in tabletop RPGs there exists an asymmetry between players and the Game Master, or GM for short.
The GM prepares and organizes the session, and leads through it acting as a judge or referee regarding the rules. 
A GM may chose to extend or change the rules laid out in the game's manual or rulebook as they see fit.
In a virtual tabletop, (footnote: depending on the system used) the GM is often the user with more rights than regular players, comparable to a super user or admin, to allow them to lead the session.

- Character sheet
Each player is represented inside the game world through their character. This characters physical or mental attributes as well as fictional backstory or any other characteristics and their changes over time are written down on the character sheet. In traditional, physical RPGs this is usually a sheet of paper copied from a template, with predefined fields depending on the ruleset. In a virtual tabletop environment, the character sheet usually emulates this by allowing players to bring up a form window where the respective fields and values are displayed and edited.

- Token
In a physical tabletop environment, any play piece might be referred to as a token. This might include chess pieces or each players representation in a game of Monopoly.
Inside a virtual tabletop environment, the word token refers to the representation of a physical object. This extends not only to play pieces, but also to dice or playing cards, things that are not usually called a "token" in a physical space.

- Dice notation
Tabletop RPGs use a variety of dice. Besides the common six sided die, four, eight, ten, twelve and twenty-sided die are all common. Dice notation is written as "xdy", where x represents the number of dice rolled, and y represents the number of sides on the die. The x can be omitted when only a single die is rolled. Thus 1d6 (or d6) means the role of a single, six-sided die, while 3d8 means rolling three eight-sided die, and summing up their results.
In a virtual environment this can lead to rolls such as 1d5, essentially a random number between one and five, even when a five-sided dice are very uncommon, while 1d2 can be used to represent a coin toss.

- Rolls
The [Dice Notation] is used to perform various rolls to determine the outcome of actions or events. The exact type of roll can vary greatly from game to game. Since this paper uses the ruleset of Dungeons & Dragons as a common example, the terms "attack roll" and "damage roll" are often used to illustrate a two-stage dice rolling process, to determine the outcome of one fictional character attacking another one in combat. [cite players handbook?]
Since the "attack roll" and "damage roll" are such common occurences, but their exact meaning can't be inferred from their names, they are described here in greater detail to avoid confusion.
The attack roll, in Dungeons & Dragons, means rolling a d20 and adding a modifier to the result, depending on the attacking characters attributes. The resulting value is then compared against the defense values of the character being attack. If the roll meets or exceeds the defense value, the attack is considered a hit.
Should the attack connect, a damage roll is then performed in order to determine the strength of the attack.


# Existing virtual tabletops

This section will introduce a number existing virtual tabletop systems and examine their standout features, advantages as well as shortcomings.


## Udonarium

Udonarium is available via a web browser and doesn't require a login or user accounts. It allows users to easily connect to each other via a session ID, as well as upload image or sound assets and use them as backgrounds or graphics for tokens.
As there is no central server to host play sessions, the only way to store a session is to download the current state of the session, including all uploaded assets, as a whole and save it locally, for which the UI provides an option.

There are no "roles" attributed to users. This means there is no concept of ownership of individual tokens, and Udonarium doesn't distinguish between players and a gamemaster in regards to who is allowed to manipulate which token. This, of course, reflects the conditions of a physical table, where each player can pick up any token, and participants have to agree among themselves on the roles and rights that each of them holds. This is a noteable difference between Udonarium and all other virtual tabletop systems examined in this section.

Udonarium further sets itself apart by presenting the table in a 3D view. Although simplistic, this is much closer to a physical table than the 2-dimensional top-down view of other virtual tabletops.
The mouse allows camera placement and the manipulation of tokens by "grabbing" them with the cursor. The placement of tokens and rolling of dice, by way of double-clicking, is accompanied by sound effects that help making interaction feel tactile. The way Udonarium handles token manipulation creates a strong association between the movement of tokens inside the virtual environment and and an actual, physical environment.

Udonarium sets itself apart from other virtual tabletops by not having any way to automate calculations. That is, every bit of arithmetic needs to be done by players outside of Udonarium. The system itself only provides text areas to note down the results, and random number generators by way of dice tokens. Since many common calculations in tabletop RPGs involve a number of dice, the playing area can quickly become littered with dice. And since no restriction is placed on which player can pick up or manipulate which dice, players not on their turn can often be observed spending idle time by "playing" with unused dice: lining them up, rolling or sorting them.
These activities bear no influence on the game itself, but they are a central part of the "playfulness" mentioned in the introduction section.

While only a Japanese language version exists, the interface is simple enough to use at a basic level even without the ability to read Japanese text. Besides camera movement and the positioning and rotation of tokens, users can create new token or dice, create and edit text boxes that exist within the coordinate system of the environment.
In addition, each token has a number of attributes that can be freely edited to store game related information such as points or currency.
What's missing is a way to combine the stored attributes using formulas, or a way to automate common calculations, such as attack rolls in an RPG. Thus these rolls, often involving three of more dice, must all be done by rolling individual dice and performing arithmetics outside of Udonarium. Although true to the nature of a physical tabletop game, this method is time consuming and relatively error prone.

### Advantages:
- Easy and intuitive to use
- High "play factor"

### Disadvantages:
- Fixed pivot point
- Limited 3D capabilities
- Very limited options to store and manipulate data

## Roll20
Roll20 is a free web application with a focus on tabletop roleplaying games.
It presents the table in a two-dimensional top down view, with tokens represented as icons aligned on a grid.
To play, users must register a free account. Game assets as well as the state of the game is stored persistently on Roll20s servers.
A new game session must be created by a super user, or GM, with full access to the stored assets and the option to manage rights for all other users.
Upon creation of a game, Roll20 offers a wide selection of character sheet templates for many popular rulesets, such as Dungeons & Dragons, Shadowrun, Call of Cthulu or GURPS.
Each character sheet is implemented through a scripting language (https://wiki.roll20.net/Building_Character_Sheets) that is capable of allowing users easy access to common shortcuts.
For example in Dungeon & Dragons, the common attack roll [link to explanation], after the characters attributes are set up in the character sheet, can be executed via the press of a button.



### Advantages
- Many supported rulesets
- Persistent games
### Disadvantages


## Foundry

### Advantages
### Disadvantages


## TaleSpire

### Advantages
### Disadvantages
Low flexibility
relatively costly (20EUR)


## Tabletop simulator

### Advantages
### Disadvantages


## OneDnD official...?

### Advantages
### Disadvantages

TBD

### Perhaps a table listing core features of each system?

# More ideas

- Increase playfulness: Idle games, direct token manipulation to "act" with the token
What could be ways to "have fun" with the environment? (Hitting trees. See Hearthstone for example), Cows that moo when double clicked

- Combine tactile dice rolling and formulas: Hit a formula -> Be prompted to roll a dice -> Roll gets inserted into the formula!