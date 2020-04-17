using H_ECK.GameElements;
using H_ECK.GameUI;
using H_ECK.MoveValidation;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace H_ECK
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
        }
    }
}
