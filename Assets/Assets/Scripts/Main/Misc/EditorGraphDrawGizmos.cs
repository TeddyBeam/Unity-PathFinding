using UnityEngine;
using BaseSystems.DesignPatterns.DependencyInjection;
using PathFinding;

namespace Main.Misc
{
    public class EditorGraphDrawGizmos : MonoBehaviour
    {
        private GridGraph gridGraph;

        [Inject]
        public void Construct (GridGraph gridGraph)
        {
            this.gridGraph = gridGraph;
        }

        protected virtual void OnDrawGizmos()
        {
            if (gridGraph == null || gridGraph.Grid == null)
                return;

            foreach (Node node in gridGraph.Grid)
            {
                Gizmos.color = (node.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.Position, Vector3.one * (gridGraph.NodeDiameter - 0.1f));
            }
        }
    }
}
