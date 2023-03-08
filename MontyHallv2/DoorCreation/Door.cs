namespace MontyHallv2.DoorCreation;

public class Door
{
    private bool _opened;
    private bool _hasCar;
    private bool _playerPicked;

    public void OpeningDoor()
    {
        _opened = true;
    }

    public bool HasPlayerPicked()
    {
        return _playerPicked;
    }

    public void PlayerPickedDoor()
    {
        _playerPicked = true;
    }

    public void PlayerUnpickDoor()
    {
        _playerPicked = false;
    }
    public void InjectCarToDoor()
    {
        _hasCar = true;
    }

    public bool HasCar()
    {
        return _hasCar;
    }

    public bool IsDoorOpened()
    {
        return _opened;
    }
}