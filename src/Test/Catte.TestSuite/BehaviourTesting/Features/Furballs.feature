Feature: Furballs are launched as projectiles from the Cat
	In order to defeat the Alien
	As the Cat
	I want my Furballs to kill the Alien when they collide


Scenario: Should fly accross the screen
	Given there are no Furballs
	When I launch a Furball
	Then a Furball will fly accross the screen

Scenario: Should kill the Alien when they collide
	Given I am lined up with the Alien
	When I launch a Furball
	And the Furball collides with the Alien	
	Then the Alien will be killed
