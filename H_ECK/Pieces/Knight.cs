using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;
using H_ECK.GameElements;

namespace H_ECK.Pieces
{
    class Knight : Piece
    {
        public Knight(bool white)
            :base(white)
        {
            if (white)
                Symbol = 'N';
            else Symbol = 'n';
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
