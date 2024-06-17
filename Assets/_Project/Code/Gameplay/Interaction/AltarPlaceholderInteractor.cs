using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Interaction
{
    public class AltarPlaceholderInteractor : BaseInteractor, ICustomInteractable
    {
        [SerializeField] Portal portal;
        [SerializeField] DogFollowing dog;

        private string _actionName = "Спасти собаку";

        public void ExecuteCustom()
        {
            Debug.Assert(portal != null);
            Debug.Assert(dog != null);

            portal.gameObject.SetActive(true);
            dog.SetAttractor(portal.transform);
        }

        public string GetActionName() =>
            _actionName;    
    }
}
