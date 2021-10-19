namespace BlazorTSP.Optimization
{
    public class Solution
    {
        public double Result { get; private set; }
        public int[] Sequence { get; private set; }
        private double[,] Data => optimizer.Data;
        private int N => optimizer.N;

        private readonly Optimizer optimizer;

        public Solution(Optimizer optimizer)
        {
            this.optimizer = optimizer;
            Sequence = new int[N];
            Result = 0;
        }

        public void Release()
        {
            Result = 0;
            for (int i = 1; i < N; i++)
            {
                Result += Data[Sequence[i - 1], Sequence[i]];
            }
            Result += Data[Sequence[N - 1], Sequence[0]];
        }
        public void Swap(int p1, int p2)
        {
            var sw = Sequence[p1];
            Sequence[p1] = Sequence[p2];
            Sequence[p2] = sw;
            Release();
        }

        public Solution Clone()
        {
            var solution = new Solution(optimizer);
            Array.Copy(Sequence, solution.Sequence, N);            
            solution.Result = Result;
            return solution;
        }

        public int CompareTo(Solution other)
        {
            return Result.CompareTo(other.Result);
        }
    }
}
