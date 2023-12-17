using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ObstacleSignals : MonoSingleton<ObstacleSignals>
    {
        public UnityAction onObstacleDroneAttack = delegate { };
        public UnityAction onObstacleAttack = delegate { };
        public UnityAction<bool> onObstacleColorMatch = delegate { };
        public UnityAction<GroundObstacle> onSendObstacleGroundType = delegate { };
    }
}