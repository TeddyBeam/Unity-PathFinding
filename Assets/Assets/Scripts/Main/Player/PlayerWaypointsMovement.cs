using System;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using PathFinding.PathFinder;
using BaseSystems.DesignPatterns.Observer;
using BaseSystems.TimingCoroutine;

namespace Main.Player
{
    public class PlayerWaypointsMovement
    {
        [Serializable]
        public class Setting
        {
            public Rigidbody playerTransfrom;
            public float speed;
        }

        private float speed;
        private Rigidbody playerRigidbody;
        private IPathFinder pathFinder;

        private bool isMoving;

        public PlayerWaypointsMovement(IPathFinder pathFinder, Setting setting)
        {
            playerRigidbody = setting.playerTransfrom;
            speed = setting.speed;
            this.pathFinder = pathFinder;

            this.RegisterListener(ObserverEventID.OnPlayerClickDetected, (param) => StartMoving((Vector3)param));
        }

        public void StartMoving (Vector3 target)
        {
            if (isMoving)
                Timing.KillCoroutines("Move");

            Timing.RunCoroutine(Move(target), "Move");
        }

        public IEnumerator<float> Move (Vector3 target)
        {
            Stack<Node> path = pathFinder.FindPath(playerRigidbody.position, target);
            
            isMoving = true;

            if (path != null)
            {
                Vector3 currentTarget = path.Pop().Position;
                while (path.Count > 0)
                {
                    if (Vector3.Distance(currentTarget, playerRigidbody.position) <= 0.01f)
                        currentTarget = path.Pop().Position;

                    playerRigidbody.position = Vector3.Slerp(playerRigidbody.position, currentTarget, speed);

                    yield return Timing.WaitForOneFrame;
                }
            }

            isMoving = false;
        }
    }
}
