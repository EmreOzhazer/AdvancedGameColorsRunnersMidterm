using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] internal Animator animator;

        private void OnEnable()
        {
            SubscribeEvents();
            SetInitialAnimationState();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState += OnChangeAnimationState;
        }

        private void SetInitialAnimationState()
        {
            SetAnimationState(CollectableAnimationStates.Idle);
        }

        internal void SetAnimationState(CollectableAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }

        private void OnChangeAnimationState(CollectableAnimationStates animationState)
        {
            SetAnimationState(animationState);
           
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState -= OnChangeAnimationState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        internal void OnReset()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState?.Invoke(CollectableAnimationStates.Idle);
        }
    }
}