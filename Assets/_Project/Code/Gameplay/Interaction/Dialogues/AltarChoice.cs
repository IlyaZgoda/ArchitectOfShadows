using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Interaction.Dialogues
{
    public class AltarChoice : BaseDialog
    {
        [SerializeField] Portal portal;
        [SerializeField] DogFollowing dog;

        public override void Init()
        {
            _questionList = new List<IQuestion>()
            {
                new Question
                {
                    Text = "Отдать собаку архитектору?",
                    OnYesAnswer = SpawnImmortalFishman,
                    OnNoAnswer = SpawnFishmanAndPortal

                }
            };
        }

        private void SpawnFishmanAndPortal()
        {
            Debug.Assert(portal != null);
            Debug.Assert(dog != null);

            portal.gameObject.SetActive(true);
            dog.SetAttractor(portal.transform);
        }

        private void SpawnImmortalFishman()
        {
            Debug.Assert(dog != null);

            dog.gameObject.SetActive(false);
        }
    }
}
