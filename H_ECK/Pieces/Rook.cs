using H_ECK.BoardElements;
using H_ECK.GameElements;
using H_ECK.MoveValidation;
using System.Collections.Generic;

namespace H_ECK.Pieces
{
    class Rook : Piece
    {
        //potrebno zbog ispitivanja validnosti rokade
        public bool HasMoved { get; set; }

        public Rook(bool white)
            : base(white)
        {
            if (white)
                Symbol = (char)Symbols.WhiteRook;
            else Symbol = (char)Symbols.BlackRook;
            HasMoved = false;
        }

        public override void Move(Move move, Board board)
        {
            base.Move(move, board);
            HasMoved = true;
        }

        public override bool ValidMove(Move move, Board board)
        {
            int startX = move.Start.X;
            int startY = move.Start.Y;
            int endX = move.End.X;
            int endY = move.End.Y;
            List<Field> inTheWay;

            if (startX != endX && startY != endY)
                return false;

            //trazenje vertikalno
            else if(startY == endY)
            {
                //grana iznad
                if (endX > startX)
                    inTheWay = BoardExplorer.ExploreNorth(board, move.Start,move.End);
                //grana ispod
                else inTheWay = BoardExplorer.ExploreSouth(board,move.Start,move.End);
            }
            //trazenje horizontalno
            else
            {
                if (endY > startY)
                    inTheWay = BoardExplorer.ExploreEast(board, move.Start,move.End);
                else inTheWay = BoardExplorer.ExploreWest(board, move.Start,move.End);
            }
            
            return BoardExplorer.IsPathClear(inTheWay[inTheWay.Count-1], move.End);
        }
    }
}
