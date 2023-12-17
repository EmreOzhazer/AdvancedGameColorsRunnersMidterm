using Runtime.Enums;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableManager manager;

        #endregion

        #region Private Variables

        private readonly string _player = "Player";

        private readonly string _collectable = "Collectable";
        private readonly string _collected = "Collected";
        private readonly string _gate = "Gate";

        private readonly string _redWall = "Red Wall";
        private readonly string _blueWall = "Blue Wall";
        private readonly string _greenWall = "Green Wall";

        private readonly string _atm = "ATM";
        private readonly string _obstacle = "Obstacle";
        private readonly string _conveyor = "Conveyor";

        private GateTypes _gateTypes;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(_gate) && CompareTag(_collected))
            {
                manager.CollectableUpgrade(manager.GetCurrentValue());
            }

            if (other.CompareTag(_atm) && CompareTag(_collected))
            {
                manager.InteractionWithAtm(transform.parent.gameObject);
            }

            if (other.CompareTag(_obstacle) && CompareTag(_collected))
            {
                manager.InteractionWithObstacle(transform.parent.gameObject);
            }

            if (other.CompareTag(_conveyor) && CompareTag(_collected))
            {
                manager.InteractionWithConveyor();
            }

            if (other.CompareTag(_blueWall) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateBlue);
                Debug.LogWarning("blue wall");
            }

            if (other.CompareTag(_greenWall) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateGreen);
                Debug.LogWarning("green wall");
            }

            if (other.CompareTag(_redWall) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateRed);
                Debug.LogWarning("red wall");
            }
        }
        
    }
}