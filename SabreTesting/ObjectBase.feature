@Objects
Feature: ObjectBase
	These tests handle validating Object Inheritance and Generation

Scenario: Child Object Inherits Normally
	Given I have a Parent Object
	And I have also a Child Object
	When I make the Child Inherit Normally
	Then the Child will be the same as the Parent

Scenario: Child Object Inherits Not Maintained
	Given I have a Parent Object
	And I have also a Child Object
	When I make the Child Inherit Not Maintained
	Then the Child will be the same as the Parent

Scenario: Child Object Inherits Maintained
	Given I have a Parent Object
	And I have also a Child Object
	When I make the Child Inherit Maintained
	Then the Child will keep its non-distinct properties