using System;
using Runtime.Controllers.Obstacles;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class GroundObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ObstacleDroneController _droneObstacleController;

        [SerializeField] private ObstacleTurretController _turretObstacleController;

        #endregion

        #region Private Variables
        

        [ShowInInspector] GroundObstacle _obstacleType;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
            GroundSignals.Instance.onObstacleAttack += OnObstacleAttack;
            ObstacleSignals.Instance.onSendObstacleGroundType += OnGetObstacleGroundType;
        }

        private void OnGetObstacleGroundType(GroundObstacle groundType)
        {
            _obstacleType = groundType;
            Debug.LogWarning("grand Type warning " + _obstacleType );
        }

        private void OnObstacleAttack()
        {
            switch (_obstacleType)
            {
                case GroundObstacle.Drone:
                    _droneObstacleController.DroneAttackFunction();
                    break;
                case GroundObstacle.Turret:
                    _turretObstacleController.TurretAttackFunction();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            GroundSignals.Instance.onObstacleAttack -= OnObstacleAttack;
            ObstacleSignals.Instance.onSendObstacleGroundType -= OnGetObstacleGroundType;
        }
    }
}