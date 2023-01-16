
## Kata name:

Monty Hall

## Kata description link:

https://github.com/MYOB-Technology/General_Developer/blob/main/katas/kata-monty-hall/kata-monty-hall.md

# Testing Strategy

### Testing approach and integration testing.

I approached this kata in a classical TDD manner.
where I would use real world objects whenever i could however
I did experiment with a mockiest style. Biggest difference I noticed was when I
introduced a bug into the system any test that used the object that contained this bug would fail. However with mock testing
the only test that would fail are the test whose SUT contains the bug. This gave me the impression that being a mockiest tester proved better test isolation.
However when I wrote a mockiest test and ensure the SUT is talking properly to its dependencies. This differs a lot with a classic test where I would only care about the final state of the SUT and not how the state was derived.
the mock tests are therefore more coupled to the implementations of the SUT dependencies.
changing the nature of calls to dependencies will cause the mock tests to break.

what I found the most useful when choosing to use a mock or a classic test was determining if the SUT has very awkward dependencies.

As you can see in the commits after all refactors I ended up just preferring to use regular tests when possible and used mocks when using a test double was necessary. I am not really a fan of stubs and fakes, I don't like 
writing fixture setups as I believe its too much effort and prefer making mocks with every test. However I can see that with Fixture Setups you can reuse them with multiple test. But I can't get over the broad capabilities a mocking framework gives you
over writing your own stubs and fakes. I believe the best tested program is when you verify the state of the SUT and the behaviour I found that using mocks best achieves this.





# Self-scored competency levels

------------------------------------
### Test Doubles

Level 4

In this Monty Hall kata, I have used Used mocks where SUT dependencies were awkward, entirely in the higher level classes.
The Mocks I used were to be able to take control of randomised variables in order to test exactly what i wanted. I also inserted dummies and spies into some mocks
when I needed to fill parameters and verify some data within a process respectfully.

Some mock tests I have implemented were similar to stubs where i would mock part of the interface to test the SUT and verify a process. seen here: 
_mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
.Callback<List<Door>>(doors => doors[0].PlayerPickedDoor()) I mock part of gameMode to giving it a dummy to test if user is prompted to choose a door.

When i am needed to verify if a process is working within a block of production code, I have used mocks similar to fakes to simulate the dependencies how i want and verify if a process has done what its intended.
seen here:
"_mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
.Callback<List<Door>>(doors => doors[0].PlayerPickedDoor());

_mockGameMode.Setup(gameMode => gameMode.PlayerSwitchOrStayDoor(It.IsAny<List<Door>>()))
.Verifiable();"
 I mock the entire gameMode interface to take a dummy to verify if the player is prompted to switch doors or not.

Spies were used when I needed to verify some specific data within a process. This was done by injecting a spy within mocks to simplify the process needed to test and extract the required data needed. for example in this callback method 
".Callback<List<Door>>(doors => isADoorOpened = doors.Any(door => door.IsDoorOpened()))" I am injecting a boolean to check weather a door is opened or not.


### Keeping Things Small

Level 4

all my classes and methods are absolutely single responsibility. I achieved this by making sure to name methods and classes to reveal intent situated at doing just one thing.
this helped me identify and abstract out any code blocks that were not doing exactly what the name of the method/class was revealing

keeping methods and classes small is relatively easy as following the single responsibility rule It is very important that classes and methods are written
in a way that is easily readable and understandable, this task is significantly easier when there is only one goal in mind for the method/class
code blocks should be as small as possible to achieve this, however with justification as long as the method/class absolutely cannot be abstracted anymore and logic written is valid for the method/class responsibility then code blocks can be large.


### Removing Duplication

Level 4

To combat domain duplication in my code, I always made sure to apply domain sense to the most logical places to ensure each and one of my domains has its own responsibility. 
however for domain logic, I used Door objects that was passed around to the different classes that needed it. Therefore it was challenging to justify single responsibility whilst manipulating door objects.
Although this strategy allowed me to manipulate my door objects in a single place which also avoids domain duplication. 
To avoid code duplication, again I would make sure each piece of code written is uniquely completing a task for example If needed a method to create some doors and open one as well another method that does the same thing but instead injects a car into one
I would instead have a method to create the doors and another to either open a door or inject a car.



### Object Composition

Level 4

I did not use any inheritance in this kata, I saw that the disadvantage of child class dependency would most likely cause problems for me in the future. Especially if I were to refactor the base class or another class in the middle of the inheritance tree.
Instead I inject interfaces in dependent class's constructors and I don't allow any class dependency to be injected other than interfaces.
If I am feeling like there are too many dependencies I would rethink my design in terms of class responsibilities.
However I would only use Inheritance when it makes sense on a significant level, such as when I know for sure i will never need to change base class behaviours.
Reason being why i choose interface dependencies is because of Testability, Removing unnecessary coupling, clear contracts between objects and better code isolation.

in terms of Testability:
Mocking dependencies is a life saver for me.

Removing unnecessary coupling: 
To be able to move things around easier.

clear contracts:
use of interfaces will force me to think in terms of interaction between objects.

better code isolation: 
interfaces will remove nasty side effects around your code as changing an interface implementation won't affect other classes since they depend only on the interface abstraction.

### Command Query Separation

Level 4

I understood that it is important to make sure every single method either be a command that performs a task or a query that returns data to the caller and not both.
with single responsibility it was easy to distinguish this. for example most of my methods were to manipulate doors objects and some methods return data such as a bool depending on the data contained within the door objects.



### Revealing Intent

Level 4

Made sure every single name of Variables/Methods/Classes/Tests clearly gives an idea of what the point of that object is and why it exist.
In other words when someone reads the name of one of my declarations they should already have a clear idea of what that declaration is going to do or what its responsible for.
I found this very easy to achieve by following the single responsibility rule.



