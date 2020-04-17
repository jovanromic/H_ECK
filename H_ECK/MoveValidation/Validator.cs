﻿using H_ECK.BoardElements;
using H_ECK.GameElements;
using H_ECK.GameUI;
using H_ECK.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H_ECK.MoveValidation
{
    class Validator
    {
        public static bool ValidMove(Board board, Move m, bool white, IGameDisplay display)
        {
            int startX = m.Start.X;
            int startY = m.Start.Y;
            int endX = m.End.X;
            int endY = m.End.Y;

            if (board.Fields[startX][startY].Piece == null)
            {
                display.DisplayMessage("Invalid move! No piece selected.\n");
                return false;
            }

            else if (startX == endX && startY == endY)
            {
                display.DisplayMessage("Invalid move! End field must differ from start field.");
                return false;
            }

            else if (board.Fields[startX][startY].Piece.White != white)
            {
                display.DisplayMessage("Invalid piece selected!");
                return false;
            }

            else if (board.Fields[endX][endY].Piece != null &&
                board.Fields[endX][endY].Piece.White == white)
            {
                display.DisplayMessage("Invalid move! Overlapping pieces.");
                return false;
            }

            else
            {
                bool valid = board.Fields[startX][startY].Piece.ValidMove(m, board);
                if (!valid)
                    display.DisplayMessage("Invalid move!");
                return valid;
            }
        }

        public static void SelectPromotionPiece(Board board, int startX, int startY, bool white)
        {
            IGameDisplay display = new ConsoleDisplay();
            string legend = "\nQueen: \'Q\'\nRook: \'R\'\nBishop: \'B\'\nKnight: \'N\'\n";
            display.DisplayMessage("Promotion! Choose the piece to promote to:" + legend);

            Match match;
            string input;
            do
            {
                display.DisplayMessage("Please enter a letter from the legend ");
                input = Console.ReadLine().ToLower();
                Regex regex = new Regex(@"[qrbn]");
                match = regex.Match(input);
            } while (!match.Success);

            char letter = input.ToCharArray()[0];
            Piece p = null;
            switch (letter)
            {
                case 'q':
                    p = new Queen(white);
                    break;

                case 'r':
                    p = new Rook(white);
                    break;
                case 'b':
                    p = new Bishop(true);
                    break;
                case 'n':
                    p = new Knight(white);
                    break;
            }

            board.Fields[startX][startY].Piece = p; 
        }

        public static bool ShortCastlingPossible(Board board, bool white)
        {
            if(white)
            {

            }
        }
    }
}
