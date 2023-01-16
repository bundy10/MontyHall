using System.Xml.Serialization;
using MontyHallv2.DoorCreation;
using MontyHallv2.GameModes;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Strategies;
using Moq;

namespace TestProject1.moq;

public class GamePlayTests

{
    private readonly Mock<IGameMode> _mockGameMode;
    private readonly GamePlay _gamePlay;
    private readonly Mock<IRandom> _mockRandom;

    public GamePlayTests()
    {
        _mockGameMode = new Mock<IGameMode>();
        _mockRandom = new Mock<IRandom>();
        _gamePlay = new GamePlay(_mockGameMode.Object, _mockRandom.Object);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenThreeDoorObjectsAreCreated_ThenPlayerPromptedToChooseADoor()
    {
        //Arrange
        _mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(doors => doors[0].PlayerPickedDoor())
            .Verifiable();
        
        //Act
        _gamePlay.PlayGame();
        
        //Assert
        _mockGameMode.Verify();
    }


    [Fact]
    public void GivenPlayGameIsCalled_WhenHostOpensADoor_ThenPlayerPromptedToSwitchToADoor()
    {
        //Arrange
        _mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(doors => doors[0].PlayerPickedDoor());

        _mockGameMode.Setup(gameMode => gameMode.PlayerSwitchOrStayDoor(It.IsAny<List<Door>>()))
            .Verifiable();
        
        //Act
        _gamePlay.PlayGame();

        //Assert
        _mockGameMode.Verify();
    }
    
    
    [Fact]
    public void GivenAPlayerHasPickedADoor_WhenThePlayerIsPromptedToSwitchOrStayDoor_ThenADoorShouldBeOpenedByTheHost()
    {
        //Arrange
        var isADoorOpened = false;
        
        _mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(doors => doors[0].PlayerPickedDoor());

        _mockGameMode.Setup(gameMode => gameMode.PlayerSwitchOrStayDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(doors => isADoorOpened = doors.Any(door => door.IsDoorOpened()));

        //Act
        _gamePlay.PlayGame();

        //Assert
        Assert.True(isADoorOpened);
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_WhenTheDoorWithACarIsPickedByAPlayer_ThenPlayerHasWonTheCar()
    {
        //Arrange
        var hasWon = false;
        
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        
        _mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(door => door[2].PlayerPickedDoor());

        _mockGameMode.Setup(gameMode => gameMode.GetGameOutCome(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(door => hasWon = door[2].HasCar() && door[2].HasPlayerPicked());
        //act 
        _gamePlay.PlayGame();
        
        //Assert
        Assert.True(hasWon);
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_WhenTheDoorWithACarIsNotPickedByAPlayer_ThenPlayerHasLostTheCar()
    {
        //Arrange
        var hasWon = false;
        
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        
        _mockGameMode.Setup(gameMode => gameMode.PlayerChooseDoor(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(doors => doors[1].PlayerPickedDoor());
        
        _mockGameMode.Setup(gameMode => gameMode.GetGameOutCome(It.IsAny<List<Door>>()))
            .Callback<List<Door>>(door => hasWon = door.Any(winningDoors => winningDoors.HasCar() && winningDoors.HasPlayerPicked()));
        //Act
        _gamePlay.PlayGame();
        
        //Assert
        Assert.False(hasWon);
    }
}