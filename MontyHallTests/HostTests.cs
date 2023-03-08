using System.Resources;
using MontyHallv2.DoorCreation;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Random;
using Moq;

namespace TestProject1.moq;

public class HostTests
{
    private List<Door> _doors;
    private readonly Host _host;
    private readonly GameMaster _gameMaster;
    private readonly Mock<IRandom> _mockRandom;

    public HostTests()
    {
        _mockRandom = new Mock<IRandom>();
        _doors = new List<Door>();
        _host = new Host();
        _gameMaster = new GameMaster(_mockRandom.Object);
    }


    [Fact]
    public void GivenHostOpensADoorIsCalled_WhenDoorsAreGiven_ThenHostWillOpenDoors()
    {
        //Arrange
        _doors = _gameMaster.GetDoorsIncludingCarDoor();
        
        //Act
        _host.HostOpensDoor(_doors);
        var openedDoor = _doors.Any(door => door.IsDoorOpened());
        
        //Assert
        Assert.True(openedDoor);

    }
    
    [Fact]
    public void GivenHostOpensADoorIsCalled_ThenHostOpensADoor()
    {
        //Arrange
        _doors = _gameMaster.GetDoorsIncludingCarDoor();

        //Act
        _host.HostOpensDoor(_doors);
        var doorsOpened = _doors.FindAll(door => door.IsDoorOpened());
        var numOfDoorsOpened = doorsOpened.Count;
        
        //Assert
        Assert.Equal(1, numOfDoorsOpened);
    }
    
    [Fact]
    public void GivenHostOpensADoorIsCalled_WhenTheDoorsArePickedByPlayerOrContainsACar_ThenTheHostWillNotPickThatDoor()
    {
        //Arrange
        const int carDoor = 2;
        const int playerPickedDoor = 1;
        const int expectedDoorToBeOpened = 0;
        
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(carDoor);
        _doors = _gameMaster.GetDoorsIncludingCarDoor();
        
        //Act
        _doors[playerPickedDoor].PlayerPickedDoor();
        _host.HostOpensDoor(_doors);
        
        //Assert
        Assert.True(_doors[expectedDoorToBeOpened].IsDoorOpened());
    }
    
    [Fact]
    public void GivenHostGameOutcomeIsCalled_WhenTheDoorThatContainsACarAndIsPickedByPlayer_ThenReturnTrue()
    {
        //Arrange
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        _doors = _gameMaster.GetDoorsIncludingCarDoor();
        _doors[2].PlayerPickedDoor();
        
        Assert.True(_doors.First(door => door.HasPlayerPicked()).HasCar());
    }
    
    [Fact]
    public void GivenHostGameOutcomeIsCalled_WhenTheDoorThatContainsACarAndIsNotPickedByPlayer_ThenReturnFalse()
    {
        //Arrange
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        _doors = _gameMaster.GetDoorsIncludingCarDoor();
        _doors[1].PlayerPickedDoor();
        
        Assert.False(_doors.First(door => door.HasPlayerPicked()).HasCar());
    }
}