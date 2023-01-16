
using MontyHallv2.DoorCreation;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using Moq;

namespace TestProject1.moq;

public class GameMasterTests
{
    private readonly Mock<IRandom> _mockRandom;
    private readonly GameMaster _gameMaster;
    public GameMasterTests()
    {
        // Arrange
        _mockRandom = new Mock<IRandom>();
        _gameMaster = new GameMaster(_mockRandom.Object);
    }

    [Fact]
    public void GivenCreateDoorsAndInjectCarToRandomDoorIsCalled_ThenThreeDoorsShouldBeCreated()
    {
        //Arrange
        const int expectedNumberOfDoors = 3; 
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(1);
        
        
        //Act
        var doors = _gameMaster.GetDoorsIncludingCarDoor();
        
        
        var actualNumberOfDoors = doors.Count;
        
        
        //Assert
        Assert.Equal(expectedNumberOfDoors, actualNumberOfDoors);
    } 
    [Fact]
    public void GivenCreateDoorsAndInjectCarToRandomDoorIsCalled_WhenThreeDoorsAreCreated_ThenADoorShouldContainACar()
    {
        //Arrange
        const int expectedNumberOfDoorsWithACar = 1;
        _mockRandom.Setup(num => num.GetNumberBetweenRange(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
        
        
        //Act
        var doors = _gameMaster.GetDoorsIncludingCarDoor();
        var listOfDoorsWithACar = doors.FindAll(door => door.HasCar());
        var actualNumberOfDoorsWithCar = listOfDoorsWithACar.Count;
        
        
        //Assert
        Assert.Equal(expectedNumberOfDoorsWithACar, actualNumberOfDoorsWithCar);
    }
}
