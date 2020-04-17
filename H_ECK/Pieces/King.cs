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

            if(move.GetType().Equals(new ShortCastling(null,null).GetType()))
            {
                board.Fields[move.End.X][7].Piece.Move(
                    new Move(new Field(move.End.X, 7, null), new Field(move.End.X, 5, null)),
                    board);
            }
            else if (move.GetType().Equals(new LongCastling(null, null).GetType()))
            {
                board.Fields[move.End.X][0].Piece.Move(
                    new Move(new Field(move.End.X, 0, null), new Field(move.End.X, 3, null)),
                    board);
            }

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

            if (move.GetType().Equals(new ShortCastling(null, null).GetType()) &&
                Validator.ShortCastlingPossible(board, move))
                return true;

            if (move.GetType().Equals(new LongCastling(null, null).GetType()) &&
                Validator.LongCastlingPossible(board, move))
                return true;

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
    }
}
