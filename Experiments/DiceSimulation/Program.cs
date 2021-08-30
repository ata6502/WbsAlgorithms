using System;
using System.Diagnostics;

namespace DiceSimulation
{
    /// <summary>
    /// [Sedgewick] 1.1.35 p.61 - Run experiments to validate the probability distribution 
    /// for the sum of two dice. Keep track of the frequencies of occurrence of each value. 
    /// How many experiments do you have to perform before your empirical results match 
    /// the exact results to three decimal places? 
    /// 
    /// The exact probability distribution for the sum of two dice:
    /// distr[0]  = 0.0
    /// distr[1]  = 0.0
    /// distr[2]  = 0.027778
    /// distr[3]  = 0.055556
    /// distr[4]  = 0.083333
    /// distr[5]  = 0.111111
    /// distr[6]  = 0.138889
    /// distr[7]  = 0.166667
    /// distr[8]  = 0.138889
    /// distr[9]  = 0.111111
    /// distr[10] = 0.083333
    /// distr[11] = 0.055556
    /// distr[12] = 0.027778
    /// 
    /// An example of probability distribution calculated empirically:
    /// 
    /// The number of experiments performed: 4,336,063
    /// 
    /// Sum     Exact values    Epirical results
    /// ---     ------------    ----------------
    ///  0      0.000000        0.000000
    ///  1      0.000000        0.000000
    ///  2      0.027778        0.027776
    ///  3      0.055556        0.055557
    ///  4      0.083333        0.083291
    ///  5      0.111111        0.111147
    ///  6      0.138889        0.138986
    ///  7      0.166667        0.166575
    ///  8      0.138889        0.138941
    ///  9      0.111111        0.111211
    /// 10      0.083333        0.083241
    /// 11      0.055556        0.055480
    /// 12      0.027778        0.027796
    /// </summary>
    class Program
    {
        // We use two six-side dice in our experiments.
        private const int DieSideCount = 6;

        // We want the calculated distribution match the exact distribution
        // to three decimal places.
        private const double Tolerance = 0.0001;

        static void Main(string[] args)
        {
            // Calculate the exact probability distribution for the sum of two dice.
            var exact = CalculateExactDistribution();

            // Calculate the distribution by conducting a series of experiments.
            var (empirical, count) = CalculateEmpiricalDistribution(exact);

            ShowResults(exact, empirical, count);
        }

        private static double[] CalculateExactDistribution()
        {
            // dist[i] is the probability that the dice sum to k.
            var dist = new double[2 * DieSideCount + 1];

            for (int i = 1; i <= DieSideCount; ++i)
                for (int j = 1; j <= DieSideCount; ++j)
                    dist[i + j] += 1.0;

            for (int k = 2; k <= 2 * DieSideCount; ++k)
                dist[k] /= 36.0;

            return dist;
        }

        // Returns the empirically calculated probability distribution and the number 
        // of experiments performed.
        private static (double[], int count) CalculateEmpiricalDistribution(double[] exactValues)
        {
            // The number of occurrences of values between 2 and 12 i.e., the sum of two random integers between 1 and 6.
            var freq = new int[2 * DieSideCount + 1];

            // The empirically calculated probability distribution.
            var dist = new double[2 * DieSideCount + 1];

            var rnd = new Random(Guid.NewGuid().GetHashCode());

            var count = 1;
            var match = false;

            // Run the experiment 'count' times until the empirical results match the actual
            // distribution to three decimal places.
            while (!match)
            {
                // Obtain the sum of two random integers each between 1 and 6 i.e. an integer value between 2 and 12.
                var num = rnd.Next(1, 7) + rnd.Next(1, 7);

                // Count the number of occurrences.
                freq[num]++;

                // Compute the probability.
                dist[num] = (double)freq[num] / (double)count;

                // Check if the empirical results match the exact values to three decimal places.
                match = true;
                for (int i = 2; i < dist.Length; ++i) // we can start from index=2 because probability of 0 and 1 is always 0.0
                {
                    if (Math.Abs(dist[i] - exactValues[i]) > Tolerance)
                    {
                        match = false;
                        break;
                    }
                }

                ++count;
            }

            return (dist, count-1);
        }

        private static void ShowResults(double[] exact, double[] empirical, int count)
        {
            Debug.Assert(exact.Length == empirical.Length);

            Console.WriteLine("\nThe number of experiments performed: {0:N0}\n", count);
            Console.WriteLine("Sum\tExact values\tEpirical results");
            Console.WriteLine("---\t------------\t----------------");

            for (var i = 0; i < exact.Length; ++i)
                Console.Write($"{i,2}\t{exact[i]:F6}\t{empirical[i]:F6}\n");
        }
    }
}
