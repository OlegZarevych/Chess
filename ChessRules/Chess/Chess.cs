using System.Collections.Generic;

namespace Chess
{
    public class Chess
    {
        private const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public string fen { get; private set; }
        Board board;
        Moves moves;
        List<FigureMoving> allMoves;


        public Chess(string fen = startFen)
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }

        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);

            if (!moves.CanMove(fm))
                    return this;
            if (!board.IsCheckAfterMove(fm))
                return this;

            Board nextBoard = board.Move(fm);

            return new Chess(nextBoard);
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);

            return f == Figure.none ? '.' : (char)f;
        }

        private void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach(FigureOnSquare fs in board.YieldFigures())
            {
                foreach(Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        if (!board.IsCheckAfterMove(fm))
                            allMoves.Add(fm);
                }
            }
        }

        public List<string> GetAllMoves()
        {
            List<string> list = new List<string>();
            foreach (FigureMoving fm in allMoves)
                list.Add(fm.ToString());

            return list;
        }

        public bool IsCheck()
        {
            return board.IsCheck();
        }
    }
}
