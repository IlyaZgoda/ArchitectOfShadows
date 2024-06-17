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
        private string _actionName = "Рыбачить";
        private HealthFactory _healthFactory;

        [Inject]
        public void Construct(HealthFactory healthFactory) =>
            _healthFactory = healthFactory;
        

        
        public async void ExecuteCustom()
        {
            Debug.Assert(player != null);

            if(player.fishingEnabled == false)
            {
                Debug.Log("НЕТ УДОЧКИ");
                noRodAnimator.SetTrigger("Appear");
                return;
            }

            if (Random.Range(0.0f, 1.0f) <= 0.35f)
            {
                var rodSound = Resources.Load<GameObject>("Prefabs/Sounds/Sound_Rod");
                GameObject sound = Instantiate(rodSound, transform.position, Quaternion.identity);

                Debug.Log("лови аптечку");
                await _healthFactory.CreateHealthPack(player.transform.position);
            }
            else
            {
                var rodSound = Resources.Load<GameObject>("Prefabs/Sounds/Sound_Rod");
                GameObject sound = Instantiate(rodSound, transform.position, Quaternion.identity);

                noLuckAnimator.SetTrigger("Appear");
                Debug.Log("лох...");
            }
        }

        public string GetActionName() =>
            _actionName;    
    }
}
