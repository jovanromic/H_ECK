using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;
using H_ECK.GameElements;
using H_ECK.MoveValidation;

namespace H_ECK.Pieces
{
    class Pawn : Piece
    {
        public Pawn(bool white)
            : base(white)
        {
            if (white)
                Symbol = 'P';
            else Symbol = 'p';
        }

        public override bool ValidMove(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            int endX = move.End.X;
            int endY = move.End.Y;

            bool white = board.Fields[startX][startY].Piece.White;

            if (startX == endX)
                return false;
            if (Math.Abs(endY - startY) > 1)
                return false;
            if (Math.Abs(endX - startX) > 2)
                return false;
            Validator.EnPassant = IsEnPassant(board, startX, startY, endX, endY, white);
            bool canMove = CanMove(board, startX, startY, endX, endY, white);
            bool canEat = CanEat(board, startX, startY, endX, endY, white);
            bool canMoveTwice = CanMoveTwice(board, startX, startY, endX, endY, white);
            Validator.Promotion = (IsPromotion(endX, white) && (canMove || canEat));

            if (Validator.Promotion)
            {
                Piece p = Validator.SelectPromotionPiece(board, startX, startY, white);
                Validator.PromotionPiece = p;
            }

            return (Validator.Promotion || Validator.EnPassant ||
                canMove || canEat || canMoveTwice);
        }

        public bool CanEat(Board board, int startX, int startY, int endX, int endY, bool white)
        {
            int coef = white ? 1 : -1;

            if (Math.Abs(endY - startY) == 1 && (endX - startX) == coef)
            {
                if (board.Fields[endX][endY].Piece != null &&
                    board.Fields[endX][endY].Piece.White != white)
                    return true;
                else return false;
            }
            else return false;
        }

        public bool CanMove(Board board, int startX, int startY, int endX, int endY, bool white)
        {
            int coef = white ? 1 : -1;

            if ((startY - endY) == 0 && (endX - startX) == coef)
            {
                if (board.Fields[endX][endY].Piece == null)
                    return true;
            }
            return false;
        }

        public bool CanMoveTwice(Board board, int startX, int startY, int endX, int endY, bool white)
        {
            int coef = white ? 2 : -2;
            int rowX = white ? 1 : 6;

            if ((startY - endY) == 0 && startX == rowX && (endX - startX) == coef)
            {
                List<Field> path = white ? BoardExplorer.
                    ExploreNorth(board, board.Fields[startX][startY],
                    board.Fields[endX][endY]) :
                    BoardExplorer.ExploreSouth(board, board.Fields[startX][startY],
                    board.Fields[endX][endY]);
                if (path[path.Count - 1].Piece == null)
                    return true;
            }
            return false;
        }

        public override void Move(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;

            if (Validator.EnPassant)
            {
                int lastX = board.LastMove.End.X;
                int lastY = board.LastMove.End.Y;

                board.Fields[lastX][lastY].Piece = null;
            }
            else if (Validator.Promotion)
            {
                board.Fields[startX][startY].Piece = Validator.PromotionPiece;
            }
            base.Move(move, board);
        }

        public bool IsEnPassant(Board board, int startX, int startY, int endX, int endY, bool white)
        {
            //da li je poslednji potez bio pionom koji je presao 2 polja
            Type pawn = new Pawn(true).GetType();
            if (board.LastMove != null &&
                board.LastMove.End.Piece.GetType() == pawn &&
                Math.Abs(board.LastMove.End.X - board.LastMove.Start.X) == 2)
            {
                int x = board.LastMove.End.X;
                int y;
                for (int i = -1; i <= 1; i = i + 2)
                {
                    y = board.LastMove.End.Y + i;
                    if (BoardExplorer.WithinBoundaries(x, y))
                    {
                        //da li je pocetno polje mog piona u istoj vrsti i susednoj koloni
                        if (x == startX && y == startY)
                        {
                            int coef = white ? 1 : -1;
                            //da li je krajnje polje mog piona u istoj koloni i susednoj vrsti
                            if (endY == board.LastMove.End.Y && endX == (x + coef))
                            {
                                //jedi protivnickog piona
                                //board.Fields[x][board.LastMove.End.Y].Piece = null;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool IsPromotion(int endX, bool white)
        {
            int row = white ? 7 : 0;

            return (endX == row);

        }
    }
}
