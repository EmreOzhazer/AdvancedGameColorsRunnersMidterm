using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "EmreOzhazerMidterm/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData Data;
    }
}