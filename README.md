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

####When we inherit StateMachineBase, we must implement:
CreateAbilities:
    Here you declare all the abilities that can be used in the state machine through the DeclareAbility method,
passing a new ability instance and using the gameObject attached to the state
machine as constructor parameter (this is the context for abilities).

DeclareStates:
    You declare all the states used in the state machine through the DeclareState method.
This will link all the abilities associated to each state according to the state specification.

DefaultState: Here you must define the default state to start with giving the
type of the desired state.

### When extending CharacterSateBase for our custom states, we want
to override some methods to add functionality to the state, for example:
Update (where you can execute each frame a certain code if this is the
current sate), EnterState and ExitState (to do certain code before and
after perform the update code in the current state).

### When extending StateAbility for our custom abilities, we want
to override some methods to add functionality to the ability, for example:
Update (where you can execute each frame a certain code if this ability is able
in the current state), Enter and Exit (to do certain code before and
after perform the update code in the current ability).


### Samples

#### Cube Example
You are given two Cube gameobjects floating on a ground
with each having a state machine. Through input keys as 'M' (moving), 
'Q' (quit) or 'B' (for both), you can change dynamically the states
of the cubes. From the moving and 'both' state, you can only go back
to the default state through 'Q' input key. In default state, you can
go to moving or 'both' state freely.