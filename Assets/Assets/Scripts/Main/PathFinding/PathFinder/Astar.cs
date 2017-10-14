using System.Collections.Generic;
using UnityEngine;
using Extension.ExtraTypes.Heap;

namespace PathFinding.PathFinder
{
    public class Astar : GridGraphPathFinder
    {
        public Astar (GridGraph gridGraph) : base (gridGraph) { }

        protected override void UpdateGraph(Node startNode, Node targetNode)
        {
            Heap<Node> openNodes = new Heap<Node>(gridGraph.Area);
            HashSet<Node> closedSet = new HashSet<Node>();
            openNodes.Add(startNode);

            int stepCount = 0;

            while (openNodes.Count > 0)
            {
                Node currentNode = openNodes.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                    break;

                foreach (Node neighbour in currentNode.NeighbourNodes)
                {
                    stepCount++;

                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                        continue;

                    int newMovementCostToNeighbour = currentNode.GCost + gridGraph.GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openNodes.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = gridGraph.GetDistance(neighbour, targetNode);
                        neighbour.PreviousNode = currentNode;

                        if (!openNodes.Contains(neighbour))
                        {
                            openNodes.Add(neighbour);
                        }
                        else
                        {
                            openNodes.UpdateItem(neighbour);
                        }
                    }
                    
                }           
            }
            Debug.Log("Astar Updated Graph in " + stepCount + " step.");
        }
    }
}
