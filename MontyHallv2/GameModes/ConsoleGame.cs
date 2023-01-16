
using MontyHallv2.DoorCreation;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Messages;
using MontyHallv2.Strategies;
using MontyHallv2.Validation;

namespace MontyHallv2.GameModes;

public class ConsoleGame : IGameMode
{
    private int _playerChoice;
    private string? _playerSelection;
    
    public void PlayerChooseDoor(List<Door> doors)
    {
        _playerSelection = GetDoorSelectionFromUser();
        while (!PlayerChoiceValidator.ValidateUserDoorSelection(_playerSelection))
        {
            Dialog.InvalidDoorSelectionInputMessage();
            _playerSelection = GetDoorSelectionFromUser();
        }

        if (_playerSelection != null) _playerChoice = int.Parse(_playerSelection) - 1;
        doors[_playerChoice].PlayerPickedDoor();
    }

    public void PlayerSwitchOrStayDoor(List<Door> doors)
    {
        
        var strategy = new ToSwitch();
        _playerSelection = GetSwitchOrStayChoiceFromUser();

        while (!PlayerChoiceValidator.ValidateUserSwitchOrStayChoice(_playerSelection))
        {
            Dialog.InvalidSwitchOrStayChoiceInputMessage();
            _playerSelection = GetSwitchOrStayChoiceFromUser();
        }
        
        if (_playerSelection == "y")
        {
            strategy.ToSwitchOrStay(doors);
        }
    }
    
    public bool GetGameOutCome(List<Door> doors) => Dialog.HostGameOutcome(doors);

    public string? GetDoorSelectionFromUser()
    {
        Dialog.PromptPlayerToPickADoorMessage();
        return Console.ReadLine();
    }
    
    public string? GetSwitchOrStayChoiceFromUser()
    {
        Dialog.PromptPlayerToStayOrSwitchDoor();
        return Console.ReadLine();
    }
}