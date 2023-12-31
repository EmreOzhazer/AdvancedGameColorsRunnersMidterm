﻿using JetBrains.Annotations;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Controllers.Obstacles
{
    public class ObstacleDroneController : MonoBehaviour
    {
        internal void DroneAttackFunction()
        {
            StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
            
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        }
        
    }
    
}