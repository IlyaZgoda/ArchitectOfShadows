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
        [SerializeField] GameObject fisherMan;
        [SerializeField] GameObject dialogAccept;
        [SerializeField] GameObject dialogRefuse;

        public override void Init()
        {
            _questionList = new List<IQuestion>()
            {
                new Question
                {
                    Text = "Отдать собаку архитектору?",
                    OnYesAnswer = Accept,
                    OnNoAnswer = Refuse

                }
            };
        }

        private void Accept()
        {
            dialogAccept.SetActive(true);

            //Debug.Assert(portal != null);
            //Debug.Assert(dog != null);

            //portal.gameObject.SetActive(true);
            //dog.SetAttractor(portal.transform);

            //fisherMan.SetActive(true);
        }

        private void Refuse()
        {
            dialogRefuse.SetActive(true);
            //Debug.Assert(dog != null);

            //dog.gameObject.SetActive(false);

            //fisherMan.SetActive(true);
            //fisherMan.GetComponent<FisherMan>().HealthPoint = 1000;
        }
    }
}
