using System;
using System.Linq;
using UnityEngine;
using BaseSystems.DesignPatterns.DependencyInjection;

namespace Main.Player
{
    public class PlayerInputHandler: ITickable
    {
        [Serializable]
        public class Setting
        {
            public Camera mainCamera;
            public float rayCastMaxDistance;
            public LayerMask inputLayers;
        }

        private Camera mainCamera;
        private float rayCastMaxDistance;
        private LayerMask inputLayers;

        private PlayerWaypointsMovement playerMovement;

        public PlayerInputHandler (Setting setting, PlayerWaypointsMovement playerMovement)
        {
            mainCamera = setting.mainCamera;
            rayCastMaxDistance = setting.rayCastMaxDistance;
            inputLayers = setting.inputLayers;
            this.playerMovement = playerMovement;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, rayCastMaxDistance, inputLayers))
                {
                    playerMovement.StartMoving(hit.point);
                }
            }
        }
    }
}
