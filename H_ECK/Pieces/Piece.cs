using H_ECK.BoardElements;
using H_ECK.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H_ECK.Pieces
{
    abstract class Piece
    {
        public bool White { get; set; }
        public char Symbol { get; set; }

        public abstract bool ValidMove(Move move, Board board);
        public abstract void Eat(Field field);
        public abstract void Move(Move move);

        public Piece(bool white)
        {
            White = white;
        }
    }
}
