using MontyHallv2.DoorCreation;
using MontyHallv2.GameModes;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Random;
using MontyHallv2.Strategies;
using Moq;

namespace TestProject1.moq;

public class SimulatorTests
{
    private readonly Mock<IStrategy> _mockStrategy;
    private SimulatorGame _simulatorGame;
    private readonly Mock<IRandom> _mockRandomNum;
    private readonly List<Door> _doors;
    private const int CarDoor = 0;

    public SimulatorTests()
    {
        _mockStrategy = new Mock<IStrategy>();
        _mockRandomNum = new Mock<IRandom>();
        _simulatorGame = new SimulatorGame(_mockStrategy.Object, _mockRandomNum.Object);

        _doors = Enumerable.Range(1, 3)
            .Select(_ => new Door())
            .ToList();
        
        _doors[CarDoor].InjectCarToDoor();
    }
    
    [Fact]
    public void GivenPlayerChooseDoorIsCalled_WhenSimulatorPlayerChoosesDoor_ThenADoorWillHaveBeenPicked()
    {
        
        //Act
        _simulatorGame.PlayerChooseDoor(_doors);
        var isADoorPicked = _doors.Any(door => door.HasPlayerPicked());

        //Assert
        Assert.True(isADoorPicked);

    }

    [Fact]
    public void GivenPlayerSwitchOrStayDoorIsCalled_ThenSimulationPlayerShouldBePromptedToSwitchOrStayAtADoor()
    {
        //Arrange
        _mockStrategy.Setup(simPlayer => simPlayer.ToSwitchOrStay(It.IsAny<List<Door>>())).Verifiable();
        
        //Act
        _simulatorGame.PlayerSwitchOrStayDoor(It.IsAny<List<Door>>());
        
        //Assert
        _mockStrategy.Verify();
    }
    
    [Fact]
    public void
        GivenPlayerSwitchOrStayDoorIsCalled_WhenTwoDoorsAreEitherPickedAlreadyOrOpened_ThenTheLatterDoorWillBePicked()
    {
        _doors[0].PlayerPickedDoor();
        _doors[1].PlayerPickedDoor();
        
        _mockStrategy.Setup(simPlayer => simPlayer.ToSwitchOrStay(_doors))
            .Callback<List<Door>>(door => door[2].PlayerPickedDoor());
        
        _simulatorGame.PlayerSwitchOrStayDoor(_doors);
        
        Assert.True(_doors[2].HasPlayerPicked());
    }

    [Fact]
    public void GivenToSwitchOrStayIsCalled_WhenPlayerOptsToStay_ThenTheOriginalDoorPickedWillStayPicked()
    {
        //Arrange
        _mockRandomNum.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
        
        _simulatorGame = new SimulatorGame(new ToStay(), _mockRandomNum.Object);
        
        
        //Act
        _simulatorGame.PlayerChooseDoor(_doors);
        _simulatorGame.PlayerSwitchOrStayDoor(_doors);
        var isOriginalDoorStillPicked = _doors[0].HasPlayerPicked();

        //Assert
        Assert.True(isOriginalDoorStillPicked);

    }
    
    [Fact]
    public void GivenGetGameOutComeIsCalled_WhenPlayerChoosesTheCarDoorAndOptsToStay_ThenPlayerWillWinTheCar()
    {
        //Arrange
        _mockRandomNum.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
        _simulatorGame = new SimulatorGame(new ToStay(), _mockRandomNum.Object);

        //Act
        _simulatorGame.PlayerChooseDoor(_doors);
        _doors[1].OpeningDoor();
        _simulatorGame.PlayerSwitchOrStayDoor(_doors);
        var hasWon = _simulatorGame.GetGameOutCome(_doors);

        //Assert
        Assert.True(hasWon);

    }
    
    [Fact]
    public void GivenGetGameOutComeIsCalled_WhenPlayerChoosesANonCarDoorAndOptsToSwitch_ThenPlayerWillWinTheCar()
    {
        //Arrange
        _mockRandomNum.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        _simulatorGame = new SimulatorGame(new ToSwitch(), _mockRandomNum.Object);

        //Act
        _simulatorGame.PlayerChooseDoor(_doors);
        _doors[1].OpeningDoor();
        _simulatorGame.PlayerSwitchOrStayDoor(_doors);
        var hasWon = _simulatorGame.GetGameOutCome(_doors);

        //Assert
        Assert.True(hasWon);

    }
    
}
