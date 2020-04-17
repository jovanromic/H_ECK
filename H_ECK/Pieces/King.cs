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
    class King : Piece
    {
        public bool HasMoved { get; set; }
        public King(bool white)
            : base(white)
        {
            if (white)
                Symbol = 'K';
            else Symbol = 'k';
            HasMoved = false;
        }

        public override void Move(Move move, Board board)
        {
            base.Move(move,board);
            HasMoved = true;
        }

        public override bool ValidMove(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            int endX = move.End.X;
            int endY = move.End.Y;

            int dx = Math.Abs(endX - startX);
            int dy = Math.Abs(endY - startY);

            if (dx > 1)
                return false;
            else if (dy > 1)
                return false;
            else if (dx == dy && dx == 0)
                return false;
            else
            {
                List<Field> attackers = BoardExplorer.FieldAttackers(board, 
                    board.Fields[endX][endY],
                    !board.Fields[startX][startY].Piece.White);

                return (attackers.Count == 0);
            }
        }

        //public bool CastlingPossible(Board board, int startX,int startY,int endX, int endY, bool white)
        //{
        //    if (HasMoved)
        //        return false;
        //    else
        //    {
        //        if (white)
        //        {
        //            if (board.Fields[endX][endY])
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
    }
}
