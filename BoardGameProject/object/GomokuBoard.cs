namespace BoardGameProject
{
    internal class GomokuBoard : IBoard
    {

        private int[,] cells;
        private int size;

        public GomokuBoard(int size)
        {
            this.size = size;
            cells = new int[size, size];
            InitialiseBoard();
            PrintBoard();
        }


        private void InitialiseBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = 0;
                }
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char symbol = cells[i, j] == 0 ? '.' : (cells[i, j] == 1 ? 'X' : 'O');
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
        }
    }
}
