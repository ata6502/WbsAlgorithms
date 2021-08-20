namespace WbsAlgorithms.Miscellaneous
{
    /// <summary>
    /// The BinomialDistribution class implements two methods that calculate a single binomial distribution probability:
    /// - The Compute method does not save any intermediate results i.e., it calculates binomial distribution without utilizing already computed values. 
    /// - The ComputeWithMemory method saves computed values in an array.
    /// Binomial distribution is a discrete version of normal distribution (a bell curve).
    ///
    /// Examples of binominal distribution:
    /// 
    /// Example #1: Let's consider an experiment in which we flip a coin five times. How many heads could we get? We can have 0, 1, 2, 3, 4, or 5 heads. 
    /// What is the probability of having 0, 1, 2, 3, 4, or 5 heads? To answer this question we need to know the number of possible outcomes. Let's write 
    /// a few of them:
    /// 
    /// T T T T T (all tails)
    /// T T T T H (4 tails and 1 head)
    /// T T T H T (4 tails and 1 head)
    /// T T T H H (3 tails and two heads) etc.
    /// 
    /// We deduce that the number of outcomes is 2^n where n is the number of flips. Here, n = 5 hence the number of all possible outcomes is 2^5= 32. 
    /// Also, from combinatorics, we know that the number of ways to choose a k-element subset from an n-element set is given by the formula for binomial 
    /// coefficients:
    ///
    /// C(n, k) = (n!) / ((n - k)! k!)
    ///
    /// where, in our example, n=5 is the number of flips and k is the number of heads: 0, 1, 2, 3, 4, or 5.
    ///
    /// We are ready to calculate probabilities for each outcome.
    /// P(0 heads) = C(5, 0) / 32 = 1/32
    /// P(1 head)  = C(5, 1) / 32 = 5/32
    /// P(2 heads) = C(5, 2) / 32 = 10/32
    /// P(3 heads) = C(5, 3) / 32 = 10/32
    /// P(4 heads) = C(5, 4) / 32 = 5/32
    /// P(5 heads) = C(5, 5) / 32 = 1/32
    /// 
    /// The above list represents a binomial distribution of probability for flipping a coin five times. Note that the distribution is symmetrical as 
    /// the probability of getting a head and a tail is the same, 0.5. What would happen if probabilities for the two possible outcomes were different? 
    /// Let's see it in another example.
    ///
    /// Example #2: Let's assume that the probability of scoring in a shoot 'em up space game is p = 0.7. It means that the probability of missing 
    /// is p - 1 = 0.3. Let's say we have six attempts to destroy an alien spaceship. Here are a few possible outcomes:
    /// 
    /// M M M M M M (all missed)
    /// M M M M M S (one scored)
    /// M M M M S M (one scored)
    /// M M M M S S (two scored)
    /// M M M S M M (one scored) etc.
    ///
    /// In order to calculate probabilities for each outcome we will use the following formula:
    ///
    /// P(n, k) = C(n, k) * p^k(1-p)^(n-k)
    ///
    /// where, in our example, n=6 is the number of attempts, k is the number of successful shots (0, 1, 2, 3, 4, 5, or 6), and P(n, k) represents 
    /// the probability of exactly k successful shots in n attempts.
    ///
    /// Let's calculate probabilities:
    /// P(6,0) = C(6,0) 0.70 0.36 = 0.001
    /// P(6,1) = C(6,1) 0.71 0.35 = 0.01
    /// P(6,2) = C(6,2) 0.72 0.34 = 0.06
    /// P(6,3) = C(6,3) 0.73 0.33 = 0.185
    /// P(6,4) = C(6,4) 0.74 0.32 = 0.324
    /// P(6,5) = C(6,5) 0.75 0.31 = 0.303
    /// P(6,6) = C(6,6) 0.76 0.30 = 0.118
    /// 
    /// This is the binomial distribution of scoring in six attempts with the probability of scoring 0.7. Note that this distribution is not symmetrical 
    /// as the probability of scoring is different than the probability of missing.
    ///
    /// The formula P(n, k) = C(n, k) * p ^ k(1 - p) ^ (n - k) reduces to P(n, k) = (C(n, k)) / (2 ^ n) if the probabilities of both outcomes are equal 
    /// i.e., if they are 0.5 (just like in the first example).
    ///
    /// [Sedgewick] 1.1.27 p.59 - Estimate the number of recursive calls that would be used by
    /// the BinomialDistribution code. Develop a better implementation that is based on saving
    /// computed values in an array.
    /// </summary>
    public class BinomialDistribution
    {
        /// <summary>
        /// The number of recursive calls used by the Compute method.
        /// </summary>
        private int _counter1 = 0;

        /// <summary>
        /// The number of recursive calls used by the ComputeWithMemory method.
        /// </summary>
        private int _counter2 = 0;

        /// <summary>
        /// Intermediate probabilities computed by the ComputeWithMemory method.
        /// </summary>
        private double?[,] _values = null;

        /// <summary>
        /// Calculates probability recursively without utilizing already computed values.
        /// </summary>
        /// <param name="n">The number of all possible outcomes e.g., the number of flipping of a coin</param>
        /// <param name="k">The number of a particular outcome e.g., three heads in five coin flips</param>
        /// <param name="p">The probability of the particular outcome e.g., the probability of getting a head in a coin flip</param>
        /// <returns>Calculated probability and the number of recursive calls</returns>
        public (double Probability, int Counter) Compute(int n, int k, double p)
        {
            ++_counter1;

            if ((n == 0) && (k == 0))
                return (1.0, _counter1);

            if ((n < 0) || (k < 0))
                return (0.0, _counter1);

            var b1 = Compute(n - 1, k, p);
            var b2 = Compute(n - 1, k - 1, p);
            var bp = (1 - p) * b1.Probability + p * b2.Probability;

            return (bp, _counter1);
        }

        /// <summary>
        /// Calculates probability recursively utilizing already computed values.
        /// </summary>
        /// <param name="n">The number of all possible outcomes</param>
        /// <param name="k">The number of a particular outcome</param>
        /// <param name="p">The probability of the particular outcome</param>
        /// <returns>Calculated probability and the number of recursive calls</returns>
        public (double Probability, int Counter) ComputeWithMemory(int n, int k, double p)
        {
            if (_values == null)
                _values = new double?[n + 1, k + 1];

            ++_counter2;

            if ((n == 0) && (k == 0))
                return (1.0, _counter2);

            if ((n < 0) || (k < 0))
                return (0.0, _counter2);

            var b1 = n - 1 >= 0 ? _values[n - 1, k] : null;
            if (!b1.HasValue)
            {
                var result = ComputeWithMemory(n - 1, k, p);
                b1 = result.Probability;
                if (n - 1 >= 0)
                    _values[n - 1, k] = b1;
            }

            var b2 = n - 1 >= 0 && k - 1 >= 0 ? _values[n - 1, k - 1] : null;
            if (!b2.HasValue)
            {
                var result = ComputeWithMemory(n - 1, k - 1, p);
                b2 = result.Probability;
                if (n - 1 >= 0 && k - 1 >= 0)
                    _values[n - 1, k - 1] = b2;
            }

            var bp = (1 - p) * b1 + p * b2;

            return (bp.Value, _counter2);
        }
    }
}
