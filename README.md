# WbsAlgorithms

This repository contains C# implementations of selected algorithms and data structures each with a set of unit tests. Majority of code
contains solutions to exercises from the book "Algorithms" by Robert Sedgewick (chapters 1.1, 1.2, 1.3, and partially 1.4) as well as
from the series "Algorithms Illuminated" by Tim Roughgarden. 

Below, there are a few examples of algorithms implemented in this repository:

* [Karatsuba Integer Multiplication](./WbsAlgorithms/Arithmetic/IntegerMultiplication.cs)
* [Strassen's Matrix Multiplication](./WbsAlgorithms/Arithmetic/MatrixMultiplication.cs)
* Sorting algoritms: [MergeSort](./WbsAlgorithms/Sorting/MergeSort.cs), [QuickSort](./WbsAlgorithms/Sorting/QuickSort.cs), [InsertionSort](./WbsAlgorithms/Sorting/InsertionSort.cs)
* [Finding the closest pair of points in O(n&#183;log(n))](./WbsAlgorithms/PairPointMinMax/ClosestPair2D.cs)
* [RSelect](./WbsAlgorithms/Searching/RSelect.cs) (Randomized Selection) and [DSelect](./WbsAlgorithms/Searching/DSelect.cs) (Deterministic Selection)
* [BFS](./WbsAlgorithms/Graphs/BreathFirstSearch.cs) (Breath First Search) and [DFS](./WbsAlgorithms/Graphs/DepthFirstSearch.cs) (Deph First Search)
* [MinCut](./WbsAlgorithms/Graphs/MinCut.cs) (Karger's Random Contraction)
* [Topological Sorting](./WbsAlgorithms/Graphs/TopologicalSorting.cs)
* [Strongly Connected Components](./WbsAlgorithms/Graphs/StronglyConnectedComponents.cs) (Kosaraju's Two-pass Algorithm) 

# References

Below, there is a list of sources used to create the C# code. The names in brackets indicate a comment in the code that references 
a particular source. For example, for exercises from the Robert Sedgewick's book "Algorithms" search for the comment ``[Sedgewick]``.

- ``[Sedgewick]`` Robert Sedgewick, Kevin Wayne (2011) "Algorithms" 4th edition; Addison-Wesley Professional
- ``[AlgoIlluminated-1]`` Tim Roughgarden (2017) "Algorithms Illuminated: Part 1: The Basics: Asymptotic Notation, Master Method, Sorting, Selection"; Soundlikeyourself Publishing
- ``[AlgoIlluminated-2]`` Tim Roughgarden (2018) "Algorithms Illuminated: Part 2: Graph Algorithms and Data Structures"; Soundlikeyourself Publishing
- ``[Leetcode]`` https://leetcode.com/problemset/all/




