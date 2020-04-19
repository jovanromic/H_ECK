using H_ECK.BoardElements;
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
        public static bool EnPassant = false;
        public static bool Promotion = false;
        public static Piece PromotionPiece = null;


        public static bool ValidMove(Board board, Move m, bool white, IGameDisplay display)
        {
            int startX = m.Start.X;
            int startY = m.Start.Y;
            int endX = m.End.X;
            int endY = m.End.Y;

            if (board.Fields[startX][startY].Piece == null)
            {
                display.DisplayMessage("No piece selected.\n");
                return false;
            }

            else if (startX == endX && startY == endY)
            {
                display.DisplayMessage("End field must differ from start field.");
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
                display.DisplayMessage("Overlapping pieces.");
                return false;
            }

            else
                return board.Fields[startX][startY].Piece.ValidMove(m, board);
            
        }

        public static Piece SelectPromotionPiece(Board board, int startX, int startY, bool white)
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

            //board.Fields[startX][startY].Piece = p;
            return p;
        }

        public static bool ShortCastlingPossible(Board board, Move move)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            //end pozicija kralja
            int endX = move.End.X;
            int endY = move.End.Y;

            if (!board.Fields[startX][startY].Piece.GetType().Equals(new King(true).GetType()))
                return false;
            if (!board.Fields[endX][endY + 1].Piece.GetType().Equals(new Rook(true).GetType()))
                return false;

            if (((King)board.Fields[startX][startY].Piece).HasMoved ||
                ((Rook)board.Fields[endX][endY + 1].Piece).HasMoved)
                return false;
            else
            {
                for (int dy = 0; dy <= 2; dy++)
                {
                    if (dy != 0 && board.Fields[startX][startY + dy].Piece != null)
                        return false;
                    List<Field> attackers = BoardExplorer.FieldAttackers(board,
                        board.Fields[startX][startY + dy],
                        !board.Fields[startX][startY].Piece.White);
                    if (attackers.Count != 0)
                        return false;
                }
                return true;
            }
        }

        public static bool LongCastlingPossible(Board board, Move move)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            //end pozicija kralja
            int endX = move.End.X;
            int endY = move.End.Y;

            if (!board.Fields[startX][startY].Piece.GetType().Equals(new King(true).GetType()))
                return false;
            if (!board.Fields[endX][endY - 2].Piece.GetType().Equals(new Rook(true).GetType()))
                return false;

            if (((King)board.Fields[startX][startY].Piece).HasMoved ||
                ((Rook)board.Fields[endX][endY - 2].Piece).HasMoved)
                return false;
            else
            {
                for (int dy = 0; dy <= 2; dy++)
                {
                    if (dy != 0 && board.Fields[startX][startY - dy].Piece != null)
                        return false;
                    List<Field> attackers = BoardExplorer.FieldAttackers(board,
                        board.Fields[startX][startY - dy],
                        !board.Fields[startX][startY].Piece.White);
                    if (attackers.Count != 0)
                        return false;
                }
                return true;
            }
        }

        public static bool TryMove(Board originalBoard, Move move, bool white, IGameDisplay display)
        {
            //ispitivanje pseudo-legalnog poteza na kopiji table
            Board testBoard = new Board(originalBoard);
            if (!ValidMove(testBoard, move, white, display))
                return false;
            //potez je pseudo-legalan, ispitivanje da li potez ostavlja naseg kralja u sahu 
            else
            {
                testBoard.Fields[move.Start.X][move.Start.Y].Piece.Move(move, testBoard);
                if (KingAttackers(testBoard, white).Count != 0)
                {
                    //display.DisplayMessage("Invalid move! King is in Check.");
                    return false;
                }
                else return true;
            }
        }

        public static List<Field> KingAttackers(Board board, bool white)
        {
            int index = white ? 0 : 1;
            List<Field> attackers = BoardExplorer.FieldAttackers(board,
                board.CurrentKingFields[index], !white);

            return attackers;
        }
        public static bool CheckMate(Board board, int i)
        {
            //ako je kralj u sahu:
            //1. proveriti ima li slobodnih nenapadnutih polja => nije mat
            //2. proveriti da li je moguce nositi napadacku figuru
            //3. proveriti da li je moguce blokirati napadacku figuru
            //proveriti da li je kralj i dalje u sahu nakon 2. ili 3.
            //ako vise figura daje sah, jedina opcija je kretanje kraljem - Escape
            bool white = (i == 0);

            List<Field> blockers = new List<Field>();

            List<Field> kingAttackers = KingAttackers(board, white);
            if (kingAttackers.Count == 0)
                return false;
            else
            {
                if (BoardExplorer.CanEscape(board, white))
                    return false;

                //ako vise figura daju sah, nosenje neke od njih ne otklanja sah/mat
                else if (kingAttackers.Count == 1)
                {
                    //tacno jedna figura daje sah, ispituje se mogucnost nosenja te figure
                    //traze se napadaci figure koja daje sah - defenders

                    List<Field> defenders = BoardExplorer.FieldAttackers(board,
                        kingAttackers[0], white);
                    if (defenders.Count > 0)
                    {
                        for (int j = 0; j < defenders.Count; j++)
                        {
                            Move defendingMove = new Move(defenders[j], kingAttackers[0]);
                            //ako neki defender moze da nosi attackera bez ostavljanja kralja u sahu
                            //nije mat
                            if (TryMove(board, defendingMove, white, new ConsoleDisplay()))
                                return false;
                        }
                    }
                    //nosenje napadajuce figure je nemoguce, ispitati blokiranje
                    //validnost se ispituje unutar funkcije
                    else if (BoardExplorer.CanBlockAttacker(board,
                            kingAttackers[0], white))
                        return false;       
                }
                //nemoguce odigrati validan potez kraljem
                //nemoguce blokirati ili nositi napadacku figuru bez ostavljanja kralja u sahu
                //mat je
                return true;
            }
        }
    }
}
