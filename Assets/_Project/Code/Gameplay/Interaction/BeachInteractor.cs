using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Interaction
{
    public class BeachInteractor : BaseInteractor, ICustomInteractable
    {
        [SerializeField] Player player;
        [SerializeField] Animator noRodAnimator;
        [SerializeField] Animator noLuckAnimator;
        private string _actionName = "��������";

        public void ExecuteCustom()
        {
            Debug.Assert(player != null);

            if(player.fishingEnabled == false)
            {
                Debug.Log("��� ������");
                noRodAnimator.SetTrigger("Appear");
                return;
            }

            if (Random.Range(0.0f, 1.0f) <= 0.35f)
            {
                Debug.Log("���� �������");
            }
            else
            {
                noLuckAnimator.SetTrigger("Appear");
                Debug.Log("���...");
            }
        }

        public string GetActionName() =>
            _actionName;    
    }
}
