using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension.ExtraTypes.Heap;
using Extension.ExtraTypes.IntVector2;

namespace PathFinding
{
    public class Node : IHeapItem<Node>
    {
        public bool Walkable { get; set; }

        public Vector3 Position { get; set; }

        public IntVector2 Coordinate { get; set; }

        /// <summary>
        /// Movement penalty value. 
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The node we have to go through to get to this node.
        /// </summary>
        public Node PreviousNode { get; set; }

        public List<Node> NeighbourNodes { get; set; }

        /// <summary>
        /// Distance from starting node.
        /// </summary>
        public int GCost { get; set; }

        /// <summary>
        /// Distance from end node.
        /// </summary>
        public int HCost { get; set; }

        /// <summary>
        /// Gcost + Hcost.
        /// </summary>
        public int FCost { get { return GCost + HCost; } }

        public int HeapIndex { get; set; }

        public Node () : this (false, Vector3.zero, new IntVector2(0, 0), 0) { }

        public Node(bool walkable, Vector3 position, IntVector2 gridCoordinate, int weight)
        {
            Walkable = walkable;
            Position = position;
            Coordinate = gridCoordinate;
            Weight = weight;
            NeighbourNodes = new List<Node>();
        }

        public override string ToString()
        {
            return string.Format("[Walkable: {0}, Position: {1}, [G : {2}, H : {3}, F: {4}], GridCoordinate: {5}], ",
                                  Walkable, Position, GCost, HCost, FCost, Coordinate);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Node))
                return false;

            Node other = obj as Node;
            return Position.Equals(other.Position) && Coordinate.Equals(other.Coordinate) &&
                   GCost == other.GCost && HCost == other.HCost && FCost == other.FCost;
        }

        public override int GetHashCode()
        {
            return (GCost.GetHashCode() + HCost.GetHashCode() + FCost.GetHashCode() + Position.GetHashCode()) * 26;
        }

        public int CompareTo(Node other)
        {
            int compare = FCost.CompareTo(other.FCost);

            if (compare == 0)
                compare = HCost.CompareTo(other.HCost);

            if (compare == 0)
                compare = GCost.CompareTo(other.GCost);

            return -compare;
        }
    }
}
