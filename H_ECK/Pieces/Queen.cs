using H_ECK.BoardElements;
using H_ECK.GameElements;

namespace H_ECK.Pieces
{
    class Queen : Piece
    {
        public Queen(bool white)
            : base(white)
        {
            if (white)
                Symbol = (char)Symbols.WhiteQueen;
            else Symbol = (char)Symbols.BlackQueen;
        }

        public override bool ValidMove(Move move, Board board)
        {
            Rook rook = new Rook(White);
            Bishop bishop = new Bishop(White);

            return (rook.ValidMove(move, board) || bishop.ValidMove(move, board));
        }
    }
}
