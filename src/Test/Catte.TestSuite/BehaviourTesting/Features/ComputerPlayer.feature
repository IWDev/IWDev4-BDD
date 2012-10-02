Feature: Computer player can control the Alien
	In order to enjoy the Game
	As a Player
	I want the computer to control the Alien


Scenario: Should be able to move the Alien
	Given the Alien starts in one location
	When the computer moves the Alien up and down
	Then the Alien will be in a different location

Scenario: Should be able to shoot an Acid blob
	Given there are no Acid blobs
	When the Alien moves in a different direction
	Then there will be an Acid blob