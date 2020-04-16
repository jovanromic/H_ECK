﻿using H_ECK.GameElements;
using H_ECK.Pieces;
using System;
using System.Collections.Generic;

namespace H_ECK.BoardElements
{
    class Board
    {
        public Field[][] Fields { get; set; }
        public bool EnPassantPossible { get; set; }

        public Board()
        {
            Fields = new Field[8][];
            for (int i = 0; i < 8; i++)
            {
                Fields[i] = new Field[8];

                for (int j = 0; j < 8; j++)
                    Fields[i][j] = new Field(i, j, null);
            }
            EnPassantPossible = false;
        }

        public void Initialize()
        {
            //poredjati figure
            for (int i = 0; i < 8; i++)
            {
                Fields[1][i].Piece = new Pawn(true);
                Fields[6][i].Piece = new Pawn(false);
            }

            //topovi
            Fields[0][0].Piece = new Rook(true);
            Fields[0][7].Piece = new Rook(true);
            Fields[7][0].Piece = new Rook(false);
            Fields[7][7].Piece = new Rook(false);

            //konji
            Fields[0][1].Piece = new Knight(true);
            Fields[0][6].Piece = new Knight(true);
            Fields[7][1].Piece = new Knight(false);
            Fields[7][6].Piece = new Knight(false);

            //lovci
            Fields[0][2].Piece = new Bishop(true);
            Fields[0][5].Piece = new Bishop(true);
            Fields[7][2].Piece = new Bishop(false);
            Fields[7][5].Piece = new Bishop(false);


            //kraljice
            Fields[0][3].Piece = new Queen(true);
            Fields[7][3].Piece = new Queen(false);

            //kraljevi
            Fields[0][4].Piece = new King(true);
            Fields[7][4].Piece = new King(false);

        }

        public void PerformMove(Player player)
        {
            Move m;
            do
            {
                m = player.MakeAMove();
            } while (!ValidMove(m, player.White));

            //Fields[m.Start.X][m.Start.Y].Piece.Move(this, m); umesto ovog ispod
            Fields[m.End.X][m.End.Y].Piece = Fields[m.Start.X][m.Start.Y].Piece;
            Fields[m.Start.X][m.Start.Y].Piece = null;
        }

        public bool ValidMove(Move m, bool white)
        {
            int startX = m.Start.X;
            int startY = m.Start.Y;
            int endX = m.End.X;
            int endY = m.End.Y;

            if (Fields[startX][startY].Piece == null)
            {
                Console.WriteLine("Invalid move! No piece selected.\n");
                return false;
            }

            else if (startX == endX && startY == endY)
            {
                Console.WriteLine("Invalid move! End field must differ from start field.");
                return false;
            }

            else if (Fields[startX][startY].Piece.White != white)
            {
                Console.WriteLine("Invalid piece selected!");
                return false;
            }

            else if (Fields[endX][endY].Piece != null &&
                Fields[endX][endY].Piece.White == white)
            {
                Console.WriteLine("Invalid move! Overlapping pieces.");
                return false;
            }

            else
            {
                bool valid = Fields[startX][startY].Piece.ValidMove(m, this);
                if (!valid)
                    Console.WriteLine("Invalid move!");
                return valid;
            }
        }

        public Field ExploreNorth(Field current, Field end)
        {
            for (int i = current.Y + 1; i <= end.Y; i++)
            {
                if (Fields[current.X][i].Piece != null)
                    return Fields[current.X][i];
            }
            return null;
        }

        public Field ExploreSouth(Field current, Field end)
        {
            for (int i = current.Y - 1; i >= end.Y; i--)
            {
                if (Fields[current.X][i].Piece != null)
                    return Fields[current.X][i];
            }
            return null;
        }

        public Field ExploreEast(Field current, Field end)
        {
            for (int i = current.X + 1; i <= end.X; i++)
            {
                if (Fields[i][current.Y].Piece != null)
                    return Fields[i][current.Y];
            }
            return null;
        }

        public Field ExploreWest(Field current, Field end)
        {
            for (int i = current.X - 1; i >= end.X; i--)
            {
                if (Fields[i][current.Y].Piece != null)
                    return Fields[i][current.Y];
            }
            return null;
        }

        public Field ExploreNorthEast(Field current, Field end)
        {
            int i = current.X + 1;
            int j = current.Y + 1;

            while (i <= end.X && j <= end.Y)
            {
                if (Fields[i][j].Piece != null)
                    return Fields[i][j];

                i++;
                j++;
            }
            return null;
        }

        public Field ExploreNorthWest(Field current, Field end)
        {
            int i = current.X - 1;
            int j = current.Y + 1;

            while (i >= end.X && j <= end.Y)
            {
                if (Fields[i][j].Piece != null)
                    return Fields[i][j];

                i--;
                j++;
            }
            return null;
        }

        public Field ExploreSouthEast(Field current, Field end)
        {
            int i = current.X + 1;
            int j = current.Y - 1;

            while (i <= end.X && j >= end.Y)
            {
                if (Fields[i][j].Piece != null)
                    return Fields[i][j];

                i++;
                j--;
            }
            return null;
        }

        public Field ExploreSouthWest(Field current, Field end)
        {
            int i = current.X - 1;
            int j = current.Y - 1;

            while (i >= end.X && j >= end.Y)
            {
                if (Fields[i][j].Piece != null)
                    return Fields[i][j];

                i--;
                j--;
            }
            return null;
        }

        public bool IsPathClear(Field inTheWay, Field end)
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

        public List<Field> FieldAttackers(Field f, bool opponentPieceWhite)
        {
            List<Field> attackers = new List<Field>();

            attackers.AddRange(CrossAttacking(f, opponentPieceWhite));

            attackers.AddRange(DiagonalAttacking(f, opponentPieceWhite));


            Field king = KingAttacking(f, opponentPieceWhite);
            if (king != null)
                attackers.Add(king);

            List<Field> knightAttackers = KnightAttacking(f, opponentPieceWhite);
            if (knightAttackers.Count != 0)
                attackers.AddRange(knightAttackers);

            List<Field> pawnAttackers = PawnAttacking(f, opponentPieceWhite);
            if (pawnAttackers.Count != 0)
                attackers.AddRange(pawnAttackers);

            return attackers;
        }

        public bool IsAttackingPiece(Field inTheWay, bool opponentPieceWhite, Type piece)
        {
            //pomocna funkcija za ispitivanje figura koje se nadju na putu trazenja
            //ako se na putu nadju protivnicka kraljica ili "piece" onda te figure napadaju polje

            Type queen = new Queen(true).GetType();
            if (inTheWay != null && inTheWay.Piece.White == opponentPieceWhite &&
                (inTheWay.Piece.GetType().Equals(queen) || inTheWay.Piece.GetType().Equals(piece)))
                return true;
            return false;
        }

        public List<Field> CrossAttacking(Field f, bool opponentPieceWhite)
        {
            //ispituje da li postoji figura koja napada po horizontalnom ili vertikalnom pravacu
            //to mogu biti kraljica i top pa se top prosledjuje kao "piece" u pomocnu f-ju

            Field inTheWay;
            List<Field> attackers = new List<Field>();
            int x = f.X;
            int y = f.Y;
            Type rook = new Rook(true).GetType();

            inTheWay = ExploreNorth(f, Fields[x][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);


            inTheWay = ExploreEast(f, Fields[7][y]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouth(f, Fields[x][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            inTheWay = ExploreWest(f, Fields[0][y]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, rook))
                attackers.Add(inTheWay);

            return attackers;
        }

        public List<Field> DiagonalAttacking(Field f, bool opponentPieceWhite)
        {
            //ispituje napad po dijagonalama
            //u pomocnu funkciju se prosledjuje lovac

            Field inTheWay;
            List<Field> attackers = new List<Field>();
            int x = f.X;
            int y = f.Y;
            Type bishop = new Bishop(true).GetType();

            inTheWay = ExploreNorthEast(f, Fields[7][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouthEast(f, Fields[7][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreSouthWest(f, Fields[0][0]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            inTheWay = ExploreNorthWest(f, Fields[0][7]);
            if (IsAttackingPiece(inTheWay, opponentPieceWhite, bishop))
                attackers.Add(inTheWay);

            return attackers;
        }

        public Field KingAttacking(Field f, bool opponentPieceWhite)
        {
            Type king = new King(true).GetType();

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (!(dx == 0 && dy == 0))
                    {
                        if (WithinBoundaries(f.X + dx, f.Y + dy) &&
                            Fields[f.X + dx][f.Y + dy].Piece!=null &&
                            Fields[f.X+dx][f.Y+dy].Piece.White == opponentPieceWhite)
                        {
                            if (king.Equals(Fields[f.X + dx][f.Y + dy].Piece.GetType()))
                                return Fields[f.X + dx][f.Y + dy];
                        }

                    }
            return null;
        }

        public List<Field> KnightAttacking(Field f, bool opponentPieceWhite)
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
                    if (Fields[x][y].Piece != null &&
                        knight.Equals(Fields[x][y].Piece.GetType()) &&
                        Fields[x][y].Piece.White == opponentPieceWhite)
                        attackers.Add(Fields[x][y]);
                }
            }
            return attackers;
        }

        public List<Field> PawnAttacking(Field f, bool opposingPieceWhite)
        {
            int coef = opposingPieceWhite ? -1:1;
            Type pawn = new Pawn(true).GetType();

            List<Field> attackers = new List<Field>();

            for(int i = -1; i <= 1; i = i + 2)
            {
                if (Fields[f.X + coef][f.Y + i].Piece != null &&
                    Fields[f.X + coef][f.Y + i].Piece.GetType().Equals(pawn))
                    attackers.Add(Fields[f.X + coef][f.Y + i]);
            }
            return attackers;
        }

        public bool WithinBoundaries(int x, int y)
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }
    }
}