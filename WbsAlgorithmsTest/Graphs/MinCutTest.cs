using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WbsAlgorithms.Common;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    class MinCutTest
    {
        private const string JsonDataFilename = @"Data\MinCutTestCases.json";
        private const int IterationCount = 100; // the number of iterations chosen empirically

        [TestCaseSource(nameof(TestCases))]
        public void GetCutTest(Graph inputGraph, int expectedMinCut, string[] expectedCuts)
        {
            var minCut = int.MaxValue;

            for (var i = 1; i <= IterationCount; ++i)
            {
                var (setA, setB, crossingEdgeCount) = MinCut.GetCut(inputGraph);

                minCut = Math.Min(minCut, crossingEdgeCount);

                if (expectedCuts != null)
                {
                    // Format the results to compare with the expected cuts.
                    var formattedCut = $"{string.Join(' ', setA)},{string.Join(' ', setB)}";
                    var cutExists = expectedCuts.Any(c => c.Equals(formattedCut));
                    Assert.That(cutExists, Is.True, $"The cut {formattedCut} is not correct.");
                }
            }

            Assert.That(minCut, Is.EqualTo(expectedMinCut));
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            var data = DataReader.ReadJsonArray<MinCutTestData>(JsonDataFilename);

            foreach (var d in data)
            {
                var graph = DataReader.ReadGraph(d.InputFile);

                yield return new TestCaseData(graph, d.MinCut, d.Cuts).SetName(d.TestName);
            }
        }

        private class MinCutTestData
        {
            public string TestName { get; set; }
            public string Description { get; set; }
            public string InputFile { get; set; }
            public int MinCut { get; set; }
            public string[] Cuts { get; set; }

            public override string ToString() => $"{TestName}: {Description}";
        }
    }
}
