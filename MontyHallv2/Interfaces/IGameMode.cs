using MontyHallv2.DoorCreation;
using MontyHallv2.Messages;

namespace MontyHallv2.Interfaces;

public interface IGameMode
{
    public void PlayerChooseDoor(List<Door> doors);
    public void PlayerSwitchOrStayDoor(List<Door> doors);
    public bool GetGameOutCome(List<Door> doors);
}