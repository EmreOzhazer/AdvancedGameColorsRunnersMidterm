using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackLastItemRemoverCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private Transform _levelHolder;

        public StackLastItemRemoverCommand(StackManager stackManager, ref List<GameObject> collectableStack)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _levelHolder = GameObject.Find("LevelHolder").transform;
        }


        public void Execute()
        {
            if (_collectableStack.Count > 0)
            {
                RemoveLastItemFromStack();

                _stackManager.StackJumperCommand.Execute(_collectableStack.Count - 1,
                    _collectableStack.Count);
                _stackManager.StackTypeUpdaterCommand.Execute();

                _stackManager.OnSetStackAmount();
            }
        }

        private void RemoveLastItemFromStack()
        {
            int lastIndex = _collectableStack.Count - 1;
            GameObject lastItem = _collectableStack[lastIndex];
            _collectableStack.RemoveAt(lastIndex);
            _collectableStack.TrimExcess();

            MoveLastItemToLevelHolder(lastItem);
        }

        private void MoveLastItemToLevelHolder(GameObject lastItem)
        {
            lastItem.transform.SetParent(_levelHolder.transform.GetChild(0));
            lastItem.SetActive(false);
        }

    }
}