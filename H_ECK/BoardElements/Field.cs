using H_ECK.Pieces;

namespace H_ECK.BoardElements
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Piece Piece { get; set; }

        public Field()
        {

        }

        public Field(int x, int y, Piece piece)
        {
            X = x;
            Y = y;
            Piece = piece;
        }

    }
}
