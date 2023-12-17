using JetBrains.Annotations;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Controllers.Obstacles
{
    public class ObstacleTurretController : MonoBehaviour
    {
        internal void TurretAttackFunction()
        {
            StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
        }
        
    }
    
}