using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.Windows.NPCDialogueConfigs
{
    [CreateAssetMenu(fileName = "InteractionData", menuName = "Static Data/NPCDialogData")]
    public class DialogueConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string[] _lines;

        public string Name => _name;
        public string[] Lines => _lines;
    }
}
