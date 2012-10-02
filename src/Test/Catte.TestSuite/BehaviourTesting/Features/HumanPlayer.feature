Feature: Player can control the Cat
	In order to play the Game
	As a Player
	I want to control the Cat

Scenario: Should be able to move the Cat
	Given the Cat starts in one location
	When I move the Cat up and down
	Then the Cat will be in a different location

Scenario: Should be able to shoot a Furball
	Given there are no Furballs
	When I make the Cat shoot a Furball
	Then there will be a Furball