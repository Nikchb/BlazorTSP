namespace BlazorTSP.Optimization
{
    public class Optimizer
    {
        public int N { get; }

        public double[,] Data { get; }

        public Optimizer(int n, double[,] data)
        {
            N = n;
            Data = data;          
        }

        public Solution Solve(double startTemperature, double c)
        {
            var rand = new Random();

            var currentSolution = new Solution(this);            
            for (int j = 0; j < N; j++)
            {
                currentSolution.Sequence[j] = j;
            }

            Array.Copy(currentSolution.Sequence.OrderBy(v => rand.NextDouble()).ToArray(), currentSolution.Sequence, N);
            currentSolution.Release();

            var bestSolution = currentSolution.Clone();
            double temperature = startTemperature;

            while(temperature > 1)
            {
                var newSollution = currentSolution.Clone();
                newSollution.Swap(rand.Next(0, N), rand.Next(0, N));

                if(newSollution.Result <= currentSolution.Result)
                {
                    currentSolution = newSollution;
                    if(currentSolution.Result < bestSolution.Result)
                    {
                        bestSolution = currentSolution;
                    }
                }
                else
                {
                    if (rand.NextDouble() < Math.Exp(-1 * Math.Abs(newSollution.Result - currentSolution.Result) / temperature))                       
                    {
                        currentSolution = newSollution;
                    }
                }
                temperature *= c;
            }
            return bestSolution;
        }
    }
}

