using System.ComponentModel.DataAnnotations;
using MontyHallv2.DoorCreation;
using MontyHallv2.GameModes;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Random;
using MontyHallv2.Strategies;

namespace MontyHallv2
{
    internal static class Program
    {
        public static void Main()
        {
            var game = new Simulator1000();
            var consoleGame = new GamePlay(new ConsoleGame(), new RandomNum());
            game.Simulate1000(new SimulatorGame(new ToStay(), new RandomNum()), new RandomNum());
            game.Simulate1000(new SimulatorGame(new ToSwitch(), new RandomNum()), new RandomNum());
            consoleGame.PlayGame();
        }
    }
}
    