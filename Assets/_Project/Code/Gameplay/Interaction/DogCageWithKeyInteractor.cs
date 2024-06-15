using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Interaction
{
    public class DogCageWithKeyInteractor : BaseInteractor, ICustomInteractable
    {
        [SerializeField] List<GameObject> toDeactivate;   // deactivate these objects after interact
        [SerializeField] List<GameObject> toActivate;     // activate these objects after interact
        [SerializeField] GameObject dog;
        [SerializeField] Animator animator;

        private string _actionName = "Открыть";

        private IEnumerator ReleaseDog()
        {
            yield return new WaitForSeconds(1.0f / 60.0f * 50.0f); // wait 50 frames
            animator.SetTrigger("EmptyCage");
            dog.SetActive(true);
            gameObject.SetActive(false);
        }

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

            animator.SetTrigger("OpenCage");
            StartCoroutine(ReleaseDog());
        }

        public string GetActionName() =>
            _actionName;    
    }
}
