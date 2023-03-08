using MontyHallv2.DoorCreation;

namespace MontyHallv2.Interfaces;

public interface IStrategy
{
    public void ToSwitchOrStay(List<Door> doors);
}