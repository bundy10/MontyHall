using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;

namespace MontyHallv2.GameModes;

public class Simulator1000
{
    private int _gamesWon;

    public void Simulate1000(IGameMode gameMode, IRandom random)
    {
        _gamesWon = 0;
        var game = new GamePlay(gameMode, random);
        for (var i = 0; i < 1000; i++)
        {
            game.PlayGame();
            if (game.GetOutComeOfGame())
            {
                _gamesWon++;
            }

            
        }
        var losses = ((1000 - _gamesWon) / 1000.0) * 100;
        var gamesWon = (_gamesWon / 1000.0) * 100;
        Console.WriteLine($"{gamesWon}% games won and {losses}% games lost");
    }
    
}