using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Interaction
{
    public class CustomInteractor : BaseInteractor, ICustomInteractable
    {
        private string _actionName = "some action";

        public void ExecuteCustom()
        {
            Debug.Log("Execute");
        }

        public string GetActionName() =>
            _actionName;    
    }
}
