using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Interaction.Dialogues
{
    public class CorruptedArchitectFirstChoice : MonoBehaviour, IChoiceInteractable
    {
        [SerializeField] CorruptedArchitect boss;

        protected List<IQuestion> _questionList;
        private int _current;

        public event Action OnChange;
        public event Action OnEnd;

        public virtual void Init()
        {
            _questionList = new List<IQuestion>()
            {
                new Question
                {
                    Text = "Принять предложение?",
                    OnYesAnswer = () => Debug.Log("accepted"),
                    OnNoAnswer = StartFight

                },

                new Question
                {
                    Text = "\"Передай мне остатки силы архитектора и ты станешь правителем этого мира!\"",
                    OnYesAnswer = KillPlayer,
                    OnNoAnswer = StartFight
                },
            };
        }

        public string GetQuestion() =>
            _questionList[_current].Text;

        public void AnswerYes()
        {
            _questionList[_current].OnYesAnswer?.Invoke();
            NextQuestion();
        }

        public void AnswerNo()
        {
            _questionList[_current].OnNoAnswer?.Invoke();
            //NextQuestion();
            Destroy(gameObject);
            OnEnd?.Invoke();
        }

        private void NextQuestion()
        {
            _current++;

            if (_current >= _questionList.Count)
            {
                Destroy(gameObject);
                OnEnd?.Invoke();
            }

            else
                OnChange?.Invoke();

        }

        private void StartFight()
        {
            boss.StartFight();
        }

        private void KillPlayer()
        {
            GameObject.Find("Player").GetComponent<Player>().deathIsGameOver = true;
            boss.AnnihilatePlayer();
        }
    }
}
