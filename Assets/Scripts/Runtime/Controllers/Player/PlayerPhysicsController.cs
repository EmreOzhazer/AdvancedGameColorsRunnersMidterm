using DG.Tweening;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody managerRigidbody;

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private bool _isColorMatchFailed;

        private readonly string _obstacle = "Obstacle";
        private readonly string _atm = "ATM";
        private readonly string _collectable = "Collectable";
        private readonly string _conveyor = "Conveyor";
        
        

        #region Color Changer Gates Tags

        private readonly string _redWall = "Red Wall";
        private readonly string _blueWall = "Blue Wall";
        private readonly string _greenWall = "Green Wall";

        #endregion

        #region Colorful Ground Obstacle Types Tags

        private readonly string _colorfulObstacle = "Colorful Obstacle";
        private readonly string _colorfulDynamicObstacle = "Colorful Dynamic";

        #endregion

        #region Colorful Ground Obstacle Tag

        private readonly string _groundObstacle = "Colorful Ground";

        #endregion

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_obstacle))
            {
                managerRigidbody.transform.DOMoveZ(managerRigidbody.transform.position.z - 10f, 1f)
                    .SetEase(Ease.OutBack);

                StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();

                other.gameObject.SetActive(false);

                return;
            }

            if (other.CompareTag(_atm))
            {
                CoreGameSignals.Instance.onAtmTouched?.Invoke(other.gameObject);
                return;
            }

            if (other.CompareTag(_collectable))
            {
                CollectableManager collectableManager = other.transform.parent.GetComponent<CollectableManager>();

                if (collectableManager != null)
                {
                    CollectableColorTypes collectableColorType = collectableManager.collectableColorType;
                    CollectableColorTypes playerColorType = playerManager.playerColorType;
                    
                    if (collectableColorType == playerColorType)
                    {
                        Debug.Log("Color match: " + collectableColorType);
                        
                        other.tag = "Collected";
                        StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
                        StackSignals.Instance.onUpdateAnimation?.Invoke();
                    }
                    else
                    {
                        Debug.Log("Color not match: " + collectableColorType);
                        StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
                        other.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogError("qqq");
                }

                return;
            }

            if (other.CompareTag(_conveyor))
            {
                CoreGameSignals.Instance.onMiniGameEntered?.Invoke();
            }

            if (other.CompareTag(_blueWall))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Blue);
            }

            if (other.CompareTag(_greenWall))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Green);
            }

            if (other.CompareTag(_redWall))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Red);
            }


            if (other.CompareTag(_colorfulObstacle))
            {
                playerManager.StaticGroundObstacleState();
                
                ObstacleSignals.Instance.onSendObstacleGroundType.Invoke(GroundObstacle.Turret);
                Debug.Log("111");
                //delay
                GroundSignals.Instance.onObstacleAttack.Invoke();
                
            }

            if (other.CompareTag(_colorfulDynamicObstacle))
            {
               // playerManager.DynamicGroundObstacleState();
                playerManager.StaticGroundObstacleState();
                ObstacleSignals.Instance.onSendObstacleGroundType.Invoke(GroundObstacle.Drone);
               
                
                GroundSignals.Instance.onObstacleAttack.Invoke();
                
                //PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);

            }

            if (other.CompareTag(_groundObstacle))
            {
                var otherMaterial = other.gameObject.GetComponent<MeshRenderer>().materials[0];
                var otherColor = CleanUpMaterialName(otherMaterial.name);

                var playerColor = playerManager.playerColorType.ToString();

                Debug.LogWarning("Other Color :" + otherColor);

                if (otherColor == playerManager.playerColorType.ToString())
                {
                    _isColorMatchFailed = false;
                    
                    Debug.Log("same ground color");
                    
                    ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
                }
                else if (otherColor != playerManager.playerColorType.ToString())
                {
                    _isColorMatchFailed = true;
                    Debug.Log("players color" + playerColor);
                    ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
                }
            }
        }
        private string CleanUpMaterialName(string fullName)
        {
            int indexOfParenthesis = fullName.IndexOf(" (");
            return indexOfParenthesis >= 0 ? fullName.Substring(0, indexOfParenthesis) : fullName;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_colorfulObstacle))
            {
                playerManager.SetNormalSpeed();
                ResetColorMatchState();
            }
        }

        private void ResetColorMatchState()
        {
            _isColorMatchFailed = false;
            
            ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
        }
    }
}