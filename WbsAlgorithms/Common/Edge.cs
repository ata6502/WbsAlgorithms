namespace WbsAlgorithms.Common
{
    // TODO: Do we need this structure for graphs?
    public class Edge
    {
        public int U { get; set; } // the index of the tail vertex
        public int V { get; set; } // the index of the head vertex

        public override string ToString()
            => $"{U},{V}";
    }
}
