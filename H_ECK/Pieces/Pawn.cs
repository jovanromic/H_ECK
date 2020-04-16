using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;
using H_ECK.GameElements;

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

        public override void Eat(Field field)
        {
            throw new NotImplementedException();
        }

        public override void Move(Move move)
        {
            throw new NotImplementedException();
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
            if (CanMove(board, startX, startY, endX, endY, white) ||
                CanEat(board, startX, startY, endX, endY, white) ||
                CanMoveTwice(board,startX,startY,endX,endY,white))
                return true;

            return false;
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
                if ((white ? board.ExploreNorth(board.Fields[startX][startY],
                    board.Fields[endX][endY]) :
                    board.ExploreSouth(board.Fields[startX][startY], 
                    board.Fields[endX][endY])) == null)
                    return true;
            }
            return false;
        }
    }
}
