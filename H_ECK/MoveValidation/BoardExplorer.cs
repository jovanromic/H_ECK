using H_ECK.BoardElements;
using H_ECK.GameElements;
using H_ECK.GameUI;
using H_ECK.Pieces;
using System;
using System.Collections.Generic;

namespace H_ECK.MoveValidation
{
    class BoardExplorer
    {
        //pomocne funkcije za odredjivanje napadnutog polja
        public static List<Field> ExploreEast(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            for (int i = current.Y + 1; i <= end.Y; i++)
            {
                path.Add(board.Fields[current.X][i]);
                if (board.Fields[current.X][i].Piece != null)
                    return path;
            }
            return path;
        }

        public static List<Field> ExploreWest(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            for (int i = current.Y - 1; i >= end.Y; i--)
            {
                path.Add(board.Fields[current.X][i]);
                if (board.Fields[current.X][i].Piece != null)
                    return path;
            }
            return path;
        }

        public static List<Field> ExploreNorth(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            for (int i = current.X + 1; i <= end.X; i++)
            {
                path.Add(board.Fields[i][current.Y]);
                if (board.Fields[i][current.Y].Piece != null)
                    return path;
            }
            return path;
        }

        public static List<Field> ExploreSouth(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            for (int i = current.X - 1; i >= end.X; i--)
            {
                path.Add(board.Fields[i][current.Y]);
                if (board.Fields[i][current.Y].Piece != null)
                    return path;
            }
            return path;
        }

        public static List<Field> ExploreNorthEast(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            int i = current.X + 1;
            int j = current.Y + 1;

            while (i <= end.X && j <= end.Y)
            {
                path.Add(board.Fields[i][j]);
                if (board.Fields[i][j].Piece != null)
                    return path;

                i++;
                j++;
            }
            return path;
        }

        public static List<Field> ExploreNorthWest(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            int i = current.X - 1;
            int j = current.Y + 1;

            while (i >= end.X && j <= end.Y)
            {
                path.Add(board.Fields[i][j]);
                if (board.Fields[i][j].Piece != null)
                    return path;

                i--;
                j++;
            }
            return path;
        }

        public static List<Field> ExploreSouthEast(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            int i = current.X + 1;
            int j = current.Y - 1;

            while (i <= end.X && j >= end.Y)
            {
                path.Add(board.Fields[i][j]);
                if (board.Fields[i][j].Piece != null)
                    return path;

                i++;
                j--;
            }
            return path;
        }

        public static List<Field> ExploreSouthWest(Board board, Field current, Field end)
        {
            List<Field> path = new List<Field>();
            int i = current.X - 1;
            int j = current.Y - 1;

            while (i >= end.X && j >= end.Y)
            {
                path.Add(board.Fields[i][j]);
                if (board.Fields[i][j].Piece != null)
                    return path;

                i--;
                j--;
            }
            return path;
        }

        private static bool IsAttackingPiece(Field inTheWay, bool opponentPieceWhite, Type piece)
        {
            //pomocna funkcija za ispitivanje figura koje se nadju na putu trazenja
            //ako se na putu nadju protivnicka kraljica ili "piece" onda te figure napadaju polje

            Type queen = new Queen(true).GetType();
            if (inTheWay.Piece != null && inTheWay.Piece.White == opponentPieceWhite &&
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
            List<Field> path;
            int x = f.X;
            int y = f.Y;
            Type rook = new Rook(true).GetType();

            path = ExploreEast(board, f, board.Fields[x][7]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                    attackers.Add(inTheWay);
            }


            path = ExploreNorth(board, f, board.Fields[7][y]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                    attackers.Add(inTheWay);
            }


            path = ExploreWest(board, f, board.Fields[x][0]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                    attackers.Add(inTheWay);
            }

            path = ExploreSouth(board, f, board.Fields[0][y]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                    attackers.Add(inTheWay);
            }

            return attackers;
        }

        private static List<Field> DiagonalAttacking(Board board, Field f, bool opponentPieceWhite)
        {
            //ispituje napad po dijagonalama
            //u pomocnu funkciju se prosledjuje lovac

            Field inTheWay;
            List<Field> attackers = new List<Field>();
            List<Field> path;
            int x = f.X;
            int y = f.Y;
            Type bishop = new Bishop(true).GetType();

            path = ExploreNorthEast(board, f, board.Fields[7][7]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                    attackers.Add(inTheWay);
            }

            path = ExploreSouthEast(board, f, board.Fields[7][0]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                    attackers.Add(inTheWay);
            }

            path = ExploreSouthWest(board, f, board.Fields[0][0]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                    attackers.Add(inTheWay);
            }

            path = ExploreNorthWest(board, f, board.Fields[0][7]);
            if (path.Count > 0)
            {
                inTheWay = path[path.Count - 1];
                if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                    attackers.Add(inTheWay);
            }

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
                if (WithinBoundaries(f.X + coef,f.Y+i) &&
                    board.Fields[f.X + coef][f.Y + i].Piece != null &&
                    board.Fields[f.X + coef][f.Y + i].Piece.GetType().Equals(pawn) &&
                    board.Fields[f.X + coef][f.Y + i].Piece.White == opposingPieceWhite)
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

        public static bool CanEscape(Board board, bool white)
        {
            int index = white ? 0 : 1;
            Field f = board.CurrentKingFields[index];
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (!(dx == 0 && dy == 0))
                    {
                        if (WithinBoundaries(f.X + dx, f.Y + dy) &&
                            board.Fields[f.X + dx][f.Y + dy].Piece == null)
                        {
                            List<Field> attackers = FieldAttackers(board,
                                board.Fields[f.X + dx][f.Y + dy], !white);
                            if (attackers.Count == 0)
                                return true;
                        }

                    }
            return false;
        }

        //public static bool CanEatAttacker(Board board, Field attackerField, bool white)
        //{
        //    List<Field> defenders
        //    //todo
        //    //return false;
        //}

        public static bool CanBlockAttacker(Board board, Field attackerField, bool white)
        {
            //ispitati da li neka moja figura napada polje na putu od kralja do napadaca
            //legalan potez
            List<Field> allDefenders = new List<Field>();
            if (new Knight(true).GetType().Equals(attackerField.Piece.GetType()))
                return false;
            else
            {
                int coef = white ? 0 : 1;
                int attackerX = attackerField.X;
                int attackerY = attackerField.Y;
                int kingX = board.CurrentKingFields[coef].X;
                int kingY = board.CurrentKingFields[coef].Y;
                //lista svih polja od kralja do napadacke figure ukljucujuci i napadaca
                //ako napadamo neko od tih polja, sah je blokiran ili napadac pojeden
                List<Field> path = new List<Field>();

                if (attackerY == kingY && attackerX > kingX)
                    path = ExploreNorth(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY == kingY && attackerX < kingX)
                    path = ExploreSouth(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY > kingY && attackerX == kingX)
                    path = ExploreEast(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY < kingY && attackerX == kingX)
                    path = ExploreWest(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY > kingY && attackerX > kingX)
                    path = ExploreNorthEast(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY < kingY && attackerX > kingX)
                    path = ExploreNorthWest(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY > kingY && attackerX < kingX)
                    path = ExploreSouthEast(board, board.CurrentKingFields[coef], attackerField);

                if (attackerY < kingY && attackerX < kingX)
                    path = ExploreSouthWest(board, board.CurrentKingFields[coef], attackerField);



                for (int i = 0; i < path.Count; i++)
                {
                    List<Field> fieldDefenders = FieldAttackers(board, path[i], white);
                    if (fieldDefenders.Count > 0)
                    {
                        for (int j = 0; j < fieldDefenders.Count; j++)
                        {
                            //ispituje se validnost poteza od branioca do polja na putu path
                            Move defendingMove = new Move(fieldDefenders[j], path[i]);
                            if (Validator.TryMove(board, defendingMove, white, new ConsoleDisplay()))
                                return true;
                        }
                    }
                }
                return false;
            }
        }

    }
}
