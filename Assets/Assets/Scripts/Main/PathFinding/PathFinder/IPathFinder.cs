using System.Collections.Generic;
using UnityEngine;

namespace PathFinding.PathFinder
{
    public interface IPathFinder
    {
        Stack<Node> FindPath(Vector3 startPosition, Vector3 targetPosition);
    }
}
