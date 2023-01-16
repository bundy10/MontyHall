using MontyHallv2.DoorCreation;
using MontyHallv2.GameModes;
using MontyHallv2.Validation;

namespace TestProject1.moq;

public class PlayerChoiceValidatorTests
{
    private readonly ConsoleGame _consoleGame = new();

    [Fact]
    public void GivenGetDoorSelectionFromUserIsCalled_WhenUserDoorChoiceIsInvalid_ThenValidatorWillReturnFalse()
    {
        //Arrange
        var invalidDoorChoice = "4\n2";
        var stringReader = new StringReader(invalidDoorChoice);
        Console.SetIn(stringReader);

        //Act
        var userDoorSelection = _consoleGame.GetDoorSelectionFromUser();
        var isUserDoorSelectionValid = PlayerChoiceValidator.ValidateUserDoorSelection(userDoorSelection);

        //Assert
        Assert.False(isUserDoorSelectionValid);
    }

    [Fact]
    public void GivenGetSwitchOrStayChoiceFromUserIsCalled_WhenUserInputIsInvalid_ThenValidatorWillReturnFalse()
    {
        //Arrange
        var invalidUserInput = "c\ny";
        var stringReader = new StringReader(invalidUserInput);
        Console.SetIn(stringReader);

        //Act
        var userSwitchOrStayChoice = _consoleGame.GetSwitchOrStayChoiceFromUser();
        var isUserSwitchOrStayChoiceIsValid = PlayerChoiceValidator.ValidateUserSwitchOrStayChoice(userSwitchOrStayChoice);

        //Assert
        Assert.False(isUserSwitchOrStayChoiceIsValid);
    }
    
    [Fact]
    public void GivenGetDoorSelectionFromUserIsCalled_WhenUserDoorChoiceIsValid_ThenValidatorWillReturnTrue()
    {
        //Arrange
        var doorChoice = "2";
        var stringReader = new StringReader(doorChoice);
        Console.SetIn(stringReader);

        //Act
        var userDoorSelection = _consoleGame.GetDoorSelectionFromUser();
        var isUserDoorSelectionValid = PlayerChoiceValidator.ValidateUserDoorSelection(userDoorSelection);

        //Assert
        Assert.True(isUserDoorSelectionValid);
    }

    [Fact]
    public void GetSwitchOrStayChoiceFromUserIsCalled_WhenUserInputIsValid_ThenValidatorWillReturnTrue()
    {
        //Arrange
        var yesToSwitch = "y";
        var stringReader = new StringReader(yesToSwitch);
        Console.SetIn(stringReader);

        //Act
        var userSwitchOrStayChoice = _consoleGame.GetSwitchOrStayChoiceFromUser();
        var isUserSwitchOrStayChoiceIsValid = PlayerChoiceValidator.ValidateUserSwitchOrStayChoice(userSwitchOrStayChoice);

        //Assert
        Assert.True(isUserSwitchOrStayChoiceIsValid);
    }
}
