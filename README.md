# state-machine

by torque-games



manifest line: 

> "com.torque-games.state-machine": "https://github.com/trqgms/state-machine.git",

### Usage

In order to use the state machine, we must inherit StateMachineBase and implement
the missing members. Then, attach this new component to the root game object of
interest (for example, the root player game object) in the scene.

In one hand we have the CharacterStateBase that we must inherit and implement
for each possible state. Similarly, there is the StateAbility class, which
represents "abilities" or "capacities" available in a certain state. We must also
create our own abilities by inheriting StateAbility. The relationship between states
and abilities is many-to-many (a certain state can be linked to many abilities, and 
a certain ability can be associated with many states).

####Cuando extendemos StateMachineBase, tendremos que implementar:
CreateAbilities:

DeclareStates:

DeclareInitialState:

DefaultState