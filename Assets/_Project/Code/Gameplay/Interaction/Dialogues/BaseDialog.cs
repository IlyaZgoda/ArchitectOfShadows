using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Interaction.Dialogues 
{
    public abstract class BaseDialog : MonoBehaviour, IChoiceInteractable
    {
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
                    Text = "Есть сиги?",
                    OnYesAnswer = () => Debug.Log("Yes"),
                    OnNoAnswer = () => Debug.Log("No")

                },

                new Question
                {
                    Text = "А если найду?",
                    OnYesAnswer = () => Debug.Log("Yes2"),
                    OnNoAnswer = () => Debug.Log("No2")
                },

                new Question
                {
                    Text = "Архитектора знаешь?",
                    OnYesAnswer = () => Debug.Log("Yes3"),
                    OnNoAnswer = () => Debug.Log("No3")
                },

                new Question
                {
                    Text = "Базаришь?",
                    OnYesAnswer = () => Debug.Log("Yes4"),
                    OnNoAnswer = () => Debug.Log("No4")
                }
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
            NextQuestion();
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

    }
}
