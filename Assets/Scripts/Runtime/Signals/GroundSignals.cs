using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class GroundSignals : MonoSingleton<GroundSignals>
    {
        public UnityAction onObstacleAttack = delegate { };
    }
}