using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.Windows
{
    [CreateAssetMenu(fileName = "InteractionData", menuName = "Static Data/InteractionConfig")]
    public class InteractionConfig : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField] string _description;
        public string Name => _name;
        public string Description => _description;
    }
}
