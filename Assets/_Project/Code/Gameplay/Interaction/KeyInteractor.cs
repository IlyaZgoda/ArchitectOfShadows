using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Interaction
{
    public class KeyInteractor : BaseInteractor, ICustomInteractable
    {
        [SerializeField] List<GameObject> toDeactivate;   // deactivate these objects after pickup
        [SerializeField] List<GameObject> toActivate;     // activate these objects after pickup

        private string _actionName = "Подобрать";

        public void ExecuteCustom()
        {
            foreach(var obj in toDeactivate)
            {
                obj.SetActive(false);
            }
            foreach (var obj in toActivate)
            {
                obj.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        public string GetActionName() =>
            _actionName;    
    }
}
