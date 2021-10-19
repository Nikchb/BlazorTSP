using BlazorTSP.Optimization;
using Microsoft.AspNetCore.Components;
using Tewr.Blazor.FileReader;

namespace BlazorTSP.Pages
{
    public partial class Index
    {
        int N
        {
            get => n;
            set {
                n = value;
                Create();
            }
        }

        int n;

        double[,] data;

        double startTemperature;

        double c;

        void setData(int i, int j, double value)
        {
            data[i, j] = value;
            data[j, i] = value;
        }        

        Solution? solution;

        TimeSpan calculationTime;

        void Create()
        {
            data = new double[n, n];            
            solution = null;
            startTemperature = 1000;
            c = 0.9;
        }
        
        void Solve()
        {
            var time = DateTime.Now;
            var optimizer = new Optimizer(n, data);
            solution = optimizer.Solve(startTemperature, c);
            calculationTime = DateTime.Now - time;
        }
    }
}
