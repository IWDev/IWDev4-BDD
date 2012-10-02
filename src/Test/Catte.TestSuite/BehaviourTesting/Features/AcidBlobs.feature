Feature: Acid blobs are launched as projectiles from the Alien
	In order to defeat the Cat
	As the Alien
	I want my Acid spit to kill the Cat when they collide


Scenario: Should fly accross the screen
	Given there are no Acid blobs
	When I launch an Acid blob
	Then an Acid blob will fly accross the screen

Scenario: Should kill the Cat when they collide
	Given I am lined up with the Cat
	When I launch an Acid blob
	And the Acid blob collides with the Cat	
	Then the Cat will be killed
