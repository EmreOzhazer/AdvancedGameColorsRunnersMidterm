using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnChangeAnimationState;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnChangeAnimationState;
        }

        private void OnChangeAnimationState(PlayerAnimationStates animationState)
        {
            SetAnimationState(animationState);
        }

        internal void OnReset()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }

        private void SetAnimationState(PlayerAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }
    }
}