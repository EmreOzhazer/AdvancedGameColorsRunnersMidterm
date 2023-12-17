using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Obstacle
{
    public class ObstacleNormalAttackCommand
    {
        private ColorfulObstacleManager _colorfulObstacleManager;

        public ObstacleNormalAttackCommand(ColorfulObstacleManager obstacleManager)
        {
            _colorfulObstacleManager = obstacleManager;
        }

        public void Execute()
        {
            PerformNormalAttack();
        }

        private void PerformNormalAttack()
        {
            if (!_colorfulObstacleManager.IsColorMatched)
            {
                HandleObstacleInteraction();
                Debug.LogWarning("TURRET SHOOT !");
            }
        }

        private void HandleObstacleInteraction()
        {
            StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
        }
    }
}
