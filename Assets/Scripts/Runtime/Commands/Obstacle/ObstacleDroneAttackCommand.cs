using DG.Tweening;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Obstacle
{
    public class ObstacleDroneAttackCommand
    {
        private ColorfulObstacleManager _colorfulObstacleManager;
        private GameObject _droneGameObject;

        public ObstacleDroneAttackCommand(ColorfulObstacleManager obstacleManager, GameObject droneGameObject)
        {
            _colorfulObstacleManager = obstacleManager;
            _droneGameObject = droneGameObject;
        }

        public void Execute()
        {
            MoveDroneToTarget(_colorfulObstacleManager.startPoint.position, _colorfulObstacleManager.midPoint.position, 2.0f, () =>
            {
                MoveDroneToTarget(_colorfulObstacleManager.midPoint.position, _colorfulObstacleManager.endPoint.position, 2.0f, () =>
                {
                    if (!_colorfulObstacleManager.IsColorMatched)
                    {
                        HandleObstacleInteraction();
                        Debug.LogWarning("DRONE SHOOT !");
                    }

                    HandlePlayerSignals();

                    Debug.Log("DRONE ACTION COMPLETED!");
                });
            });
        }

        private void MoveDroneToTarget(Vector3 from, Vector3 to, float duration, TweenCallback onComplete = null)
        {
            _droneGameObject.SetActive(true);
            _droneGameObject.transform.DOMove(to, duration).OnComplete(onComplete);
        }

        private void HandleObstacleInteraction()
        {
            StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
        }

        private void HandlePlayerSignals()
        {
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Run);
        }
    }
}