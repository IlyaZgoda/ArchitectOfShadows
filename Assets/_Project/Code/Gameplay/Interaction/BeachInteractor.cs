using Code.Infrastructure.Factories;
using Code.Services.InteractionService;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Interaction
{
    public class BeachInteractor : BaseInteractor, ICustomInteractable
    {
        [SerializeField] Player player;
        [SerializeField] Animator noRodAnimator;
        [SerializeField] Animator noLuckAnimator;
        private string _actionName = "��������";
        private HealthFactory _healthFactory;

        [Inject]
        public void Construct(HealthFactory healthFactory) =>
            _healthFactory = healthFactory;
        

        
        public async void ExecuteCustom()
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
                await _healthFactory.CreateHealthPack(player.transform.position);
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
