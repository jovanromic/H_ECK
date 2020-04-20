using H_ECK.BoardElements;
using H_ECK.GameElements;

namespace H_ECK.Pieces
{
    public abstract class Piece
    {
        public bool White { get; set; }
        public char Symbol { get; set; }

        public enum Symbols
        {
            //WhiteRook = '♖',
            //BlackRook = '♜',
            //WhiteQueen = '♕',
            //BlackQueen = '♛',
            //WhitePawn = '♙',
            //BlackPawn = '♟',
            //WhiteKnight = '♘',
            //BlackKnight = '♞',
            //WhiteKing = '♔',
            //BlackKing = '♚',
            //WhiteBishop = '♗',
            //BlackBishop = '♝'

            WhiteRook = 'R',
            BlackRook = 'r',
            WhiteQueen = 'Q',
            BlackQueen = 'q',
            WhitePawn = 'P',
            BlackPawn = 'p',
            WhiteKnight = 'N',
            BlackKnight = 'n',
            WhiteKing = 'K',
            BlackKing = 'k',
            WhiteBishop = 'B',
            BlackBishop = 'b'
        }

        public abstract bool ValidMove(Move move, Board board);
        public virtual void Move(Move move,Board board)
        {
            board.Fields[move.End.X][move.End.Y].Piece =
                board.Fields[move.Start.X][move.Start.Y].Piece;

            board.Fields[move.Start.X][move.Start.Y].Piece = null;
        }

        public Piece(bool white)
        {
            White = white;
        }
    }
}
