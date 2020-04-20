﻿using H_ECK.BoardElements;
using H_ECK.GameUI;
using H_ECK.MoveValidation;
using System;
using System.Collections.Generic;

namespace H_ECK.GameElements
{
    public class Game
    {
        public Board Board { get; set; }
        public Player[] Players { get; set; }
        public IGameDisplay GameDisplay { get; set; }
        public Clock Clock{ get; set; }


        public Game()
        {
            Board = new Board();
            Players = new Player[2];
            Players[0] = new Player(true);
            Players[1] = new Player(false);
            GameDisplay = new ConsoleDisplay();
            Clock = new Clock();
        }

        public void StartGame()
        {
            Board.Initialize();
            GameDisplay.DisplayBoard(Board);
            int i = 0;



            while (!Validator.CheckMate(Board,i))
            {

                Players[i].PerformMove(Board,GameDisplay);

                GameDisplay.DisplayBoard(Board);
                Clock.Reset();
                Clock.Start();
                //DisplayAttackers(Board.Fields[4][4]);

                i = i ^ 1;
            }

            GameDisplay.DisplayMessage("H-eck mate!");

        }

        

        //privremena fja
        public void DisplayAttackers(Field f)
        {
            List<Field> attackers = new List<Field>();
            attackers.AddRange(BoardExplorer.FieldAttackers(Board, f, true));

            foreach(Field fld in attackers)
            {
                Console.WriteLine("X:{0} Y:{1} {2}\n", fld.X, fld.Y, fld.Piece.Symbol);
            }
        }
    }
}
