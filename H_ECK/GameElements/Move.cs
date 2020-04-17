using H_ECK.BoardElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
