using System;
using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// Finds sortest paths in a symbol graph using the BreathFirstPaths algorithm.
    /// 
    /// [Sedgewick] p.555
    /// </summary>
    public class DegreesOfSeparation
    {
        private SymbolGraph _symbolGraph;
        private Graph _graph;

        /// <summary>
        /// Finds sortest paths in a symbol graph.
        /// </summary>
        /// <param name="symbolGraph">The symbol graph</param>
        public DegreesOfSeparation(SymbolGraph symbolGraph)
        {
            _symbolGraph = symbolGraph;
            _graph = symbolGraph.Graph;
        }

        /// <summary>
        /// Finds the sortest path between two vertices in a symbol graph.
        /// </summary>
        /// <param name="sourceVertex">The starting vertex</param>
        /// <param name="destVertex">The sink vertex</param>
        /// <returns></returns>
        public List<string> GetShortestPath(string sourceVertex, string destVertex)
        {
            if (!_symbolGraph.Contains(sourceVertex))
                throw new ArgumentException($"The source vertex {sourceVertex} not found in the symbol graph.");

            if (!_symbolGraph.Contains(destVertex))
                throw new ArgumentException($"The destination vertex {destVertex} not found in the symbol graph.");

            var path = new List<string>();

            var sourceVertexIndex = _symbolGraph.Index(sourceVertex);
            var destVertexIndex = _symbolGraph.Index(destVertex);

            var bfs = new BreathFirstPaths(_graph, sourceVertexIndex);
            if (bfs.HasPathTo(destVertexIndex))
            {
                foreach (var v in bfs.GetPathTo(destVertexIndex))
                    path.Add(_symbolGraph.Key(v));
            }

            return path;
        }
    }
}
