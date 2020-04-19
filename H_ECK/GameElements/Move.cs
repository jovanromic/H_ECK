using H_ECK.BoardElements;

namespace H_ECK.GameElements
{
    public class Move
    {
        public Field Start { get; set; }
        public Field End { get; set; }

        public Move(Field start, Field end)
        {
            Start = start;
            End = end;
        }

        public static int ColumnToIndex(char columnLetter)
        {
            return columnLetter - 'a';
        }

        public static int RowToIndex(char rowNumber)
        {
            int row = int.Parse(rowNumber.ToString());
            return row - 1;
        }
    }
}
