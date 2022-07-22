using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// A symbol graph with keys (vertex names) as strings.
    /// 
    /// [Sedgewick] p.552 Symbol graph data type
    /// </summary>
    public class SymbolGraph
    {
        private Dictionary<string, int> _keyToIndexMap;
        private string[] _indexToKeyMap;
        private Graph _graph;

        /// <summary>
        /// Builds a symbol graph.
        /// </summary>
        /// <param name="keyToIndexMap">Translates keys to indices of the underlying graph</param>
        /// <param name="indexToKeyMap">
        /// Translates indices of the underlying graph to keys. It serves as an inverted index which
        /// gives the key associated with a given index.</param>
        /// <param name="graph">The underlying graph</param>
        public SymbolGraph(
            Dictionary<string, int> keyToIndexMap,
            string[] indexToKeyMap,
            Graph graph)
        {
            _keyToIndexMap = keyToIndexMap;
            _indexToKeyMap = indexToKeyMap;
            _graph = graph;
        }

        /// <summary>
        /// Determines if a given key is a vertex.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True if the key is a vertex; false, otherwise</returns>
        public bool Contains(string key) => _keyToIndexMap.ContainsKey(key);

        /// <summary>
        /// Returns an index associated with a given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>An index associated with the key.</returns>
        public int Index(string key) => _keyToIndexMap[key];

        /// <summary>
        /// Returns a key associated with a given index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A key associated with the index.</returns>
        public string Key(int index) => _indexToKeyMap[index];

        /// <summary>
        /// The underlying graph.
        /// </summary>
        public Graph Graph => _graph;
    }
}
