using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Symbol = 'Q';
            else Symbol = 'q';
        }

        public override bool ValidMove(Move move, Board board)
        {
            Rook rook = new Rook(White);
            Bishop bishop = new Bishop(White);

            return (rook.ValidMove(move, board) || bishop.ValidMove(move, board));
        }
    }
}
