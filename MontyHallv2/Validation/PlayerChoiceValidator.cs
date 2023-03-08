using MontyHallv2.Messages;

namespace MontyHallv2.Validation;

public static class PlayerChoiceValidator
{
    public static bool ValidateUserDoorSelection(string? userDoorSelection) => int.TryParse(userDoorSelection, out var result) && result is >= 1 and <= 3;

    public static bool ValidateUserSwitchOrStayChoice(string? userSwitchOrStayChoice) => userSwitchOrStayChoice is "y" or "n";
}
