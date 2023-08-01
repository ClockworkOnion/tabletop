## Approaches & Decisions

The default network transform component provided by Unity's multiplayer tools package is server authorative, which means that every change in value must be verified by the server and sent back to the client, before it can affect the state of the game. This is useful to prevent unwanted manipulation of values by clients, but requires more overhead for said verfication. Since a tabletop session does not allow new players to join the game at will, but instead requires a consensus about the game's rules and communication outside of the tabletop client, this implies a degree of trust between participants. Manipulating values would be comparable to a player secretly manipulating scores on their character sheets, although much more sophisticated on a technical level.
For this reason, non server-authorative netcode was used for TTS.

## Information used
Unity Multiplayer Tutorial (CodeMonkey)
https://youtu.be/3yuBOB3VrCk

How to convert mouse position to world space in Unity
https://youtu.be/5NTmxDSKj-Q

## Packages used

Netcode for GameObjects (Unity Technologies)
Version 1.2.0 - December 09, 2022

Multiplayer Tools (Unity Technologies)
Version 1.1.0 - November 14, 2022

LeanTween (Third Party, add ref!)
