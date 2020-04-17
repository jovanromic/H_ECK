using H_ECK.BoardElements;
using H_ECK.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H_ECK.MoveValidation
{
    class BoardExplorer
    {
        //pomocne funkcije za odredjivanje napadnutog polja
        public static Field ExploreNorth(Board board, Field current, Field end)
        {
            for (int i = current.Y + 1; i <= end.Y; i++)
            {
                if (board.Fields[current.X][i].Piece != null)
                    return board.Fields[current.X][i];
            }
            return null;
        }

        public static Field ExploreSouth(Board board, Field current, Field end)
        {
            for (int i = current.Y - 1; i >= end.Y; i--)
            {
                if (board.Fields[current.X][i].Piece != null)
                    return board.Fields[current.X][i];
            }
            return null;
        }

        public static Field ExploreEast(Board board, Field current, Field end)
        {
            for (int i = current.X + 1; i <= end.X; i++)
            {
                if (board.Fields[i][current.Y].Piece != null)
                    return board.Fields[i][current.Y];
            }
            return null;
        }

        public static Field ExploreWest(Board board, Field current, Field end)
        {
            for (int i = current.X - 1; i >= end.X; i--)
            {
                if (board.Fields[i][current.Y].Piece != null)
                    return board.Fields[i][current.Y];
            }
            return null;
        }

        public static Field ExploreNorthEast(Board board, Field current, Field end)
        {
            int i = current.X + 1;
            int j = current.Y + 1;

            while (i <= end.X && j <= end.Y)
            {
                if (board.Fields[i][j].Piece != null)
                    return board.Fields[i][j];

                i++;
                j++;
            }
            return null;
        }

        public static Field ExploreNorthWest(Board board, Field current, Field end)
        {
            int i = current.X - 1;
            int j = current.Y + 1;

            while (i >= end.X && j <= end.Y)
            {
                if (board.Fields[i][j].Piece != null)
                    return board.Fields[i][j];

                i--;
                j++;
            }
            return null;
        }

        public static Field ExploreSouthEast(Board board, Field current, Field end)
        {
            int i = current.X + 1;
            int j = current.Y - 1;

            while (i <= end.X && j >= end.Y)
            {
                if (board.Fields[i][j].Piece != null)
                    return board.Fields[i][j];

                i++;
                j--;
            }
            return null;
        }

        public static Field ExploreSouthWest(Board board, Field current, Field end)
        {
            int i = current.X - 1;
            int j = current.Y - 1;

            while (i >= end.X && j >= end.Y)
            {
                if (board.Fields[i][j].Piece != null)
                    return board.Fields[i][j];

                i--;
                j--;
            }
            return null;
        }

        private static bool IsAttackingPiece(Field inTheWay, bool opponentPieceWhite, Type piece)
        {
            //pomocna funkcija za ispitivanje figura koje se nadju na putu trazenja
            //ako se na putu nadju protivnicka kraljica ili "piece" onda te figure napadaju polje

            Type queen = new Queen(true).GetType();
            if (inTheWay != null && inTheWay.Piece.White == opponentPieceWhite &&
                (inTheWay.Piece.GetType().Equals(queen) || inTheWay.Piece.GetType().Equals(piece)))
                return true;
            return false;
        }

        private static List<Field> CrossAttacking(Board board, Field f, bool opponentPieceWhite)
        {
            //ispituje da li postoji figura koja napada po horizontalnom ili vertikalnom pravacu
            //to mogu biti kraljica i top pa se top prosledjuje kao "piece" u pomocnu f-ju

            Field inTheWay;
            List<Field> attackers = new List<Field>();
            int x = f.X;
            int y = f.Y;
            Type rook = new Rook(true).GetType();

            inTheWay = ExploreNorth(board, f, board.Fields[x][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);


            inTheWay = ExploreEast(board, f, board.Fields[7][y]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouth(board, f, board.Fields[x][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            inTheWay = ExploreWest(board, f, board.Fields[0][y]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            return attackers;
        }

        private static List<Field> DiagonalAttacking(Board board, Field f, bool opponentPieceWhite)
        {
            //ispituje napad po dijagonalama
            //u pomocnu funkciju se prosledjuje lovac

            Field inTheWay;
            List<Field> attackers = new List<Field>();
            int x = f.X;
            int y = f.Y;
            Type bishop = new Bishop(true).GetType();

            inTheWay = ExploreNorthEast(board, f, board.Fields[7][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouthEast(board, f, board.Fields[7][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouthWest(board, f, board.Fields[0][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreNorthWest(board,f, board.Fields[0][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            return attackers;
        }

        private static Field KingAttacking(Board board, Field f, bool opponentPieceWhite)
        {
            Type king = new King(true).GetType();

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (!(dx == 0 && dy == 0))
                    {
                        if (WithinBoundaries(f.X + dx, f.Y + dy) &&
                            board.Fields[f.X + dx][f.Y + dy].Piece != null &&
                            board.Fields[f.X + dx][f.Y + dy].Piece.White == opponentPieceWhite)
                        {
                            if (king.Equals(board.Fields[f.X + dx][f.Y + dy].Piece.GetType()))
                                return board.Fields[f.X + dx][f.Y + dy];
                        }

                    }
            return null;
        }

        private static List<Field> KnightAttacking(Board board, Field f, bool opponentPieceWhite)
        {
            List<Field> attackers = new List<Field>();
            int[] X = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1 };

            Type knight = new Knight(true).GetType();

            for (int i = 0; i < 8; i++)
            {
                int x = f.X + X[i];
                int y = f.Y + Y[i];
                if (WithinBoundaries(x, y))
                {
                    if (board.Fields[x][y].Piece != null &&
                        knight.Equals(board.Fields[x][y].Piece.GetType()) &&
                        board.Fields[x][y].Piece.White == opponentPieceWhite)
                        attackers.Add(board.Fields[x][y]);
                }
            }
            return attackers;
        }

        private static List<Field> PawnAttacking(Board board, Field f, bool opposingPieceWhite)
        {
            int coef = opposingPieceWhite ? -1 : 1;
            Type pawn = new Pawn(true).GetType();

            List<Field> attackers = new List<Field>();

            for (int i = -1; i <= 1; i = i + 2)
            {
                if (board.Fields[f.X + coef][f.Y + i].Piece != null &&
                    board.Fields[f.X + coef][f.Y + i].Piece.GetType().Equals(pawn))
                    attackers.Add(board.Fields[f.X + coef][f.Y + i]);
            }
            return attackers;
        }

        public static bool WithinBoundaries(int x, int y)
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }

        public static List<Field> FieldAttackers(Board board, Field f, bool opponentPieceWhite)
        {
            List<Field> attackers = new List<Field>();

            attackers.AddRange(CrossAttacking(board, f, opponentPieceWhite));

            attackers.AddRange(DiagonalAttacking(board, f, opponentPieceWhite));


            Field king = KingAttacking(board, f, opponentPieceWhite);
            if (king != null)
                attackers.Add(king);

            List<Field> knightAttackers = KnightAttacking(board, f, opponentPieceWhite);
            if (knightAttackers.Count != 0)
                attackers.AddRange(knightAttackers);

            List<Field> pawnAttackers = PawnAttacking(board, f, opponentPieceWhite);
            if (pawnAttackers.Count != 0)
                attackers.AddRange(pawnAttackers);

            return attackers;
        }

        public static bool IsPathClear(Field inTheWay, Field end)
        {
            if (inTheWay == null)
                return true;
            //figura koja smeta nije na ciljnom polju => nevalidan potez
            else if (inTheWay.X != end.X || inTheWay.Y != end.Y)
            {
                Console.WriteLine("Path blocked. ");
                return false;
            }

            else return true;
        }
    }
}
