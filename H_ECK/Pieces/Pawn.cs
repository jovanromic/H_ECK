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
            :base(white)
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

        public override bool ValidMove(Move move)
        {
            throw new NotImplementedException();
        }
    }
}
