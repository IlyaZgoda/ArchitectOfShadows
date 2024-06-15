using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Interaction.Dialogues 
{
    public class Dialogue9 : MonoBehaviour, IChoiceInteractable
    {
        private string _question = "Чё как сам?";

        public event Action OnYesAnswer;
        public event Action OnNoAnswer;

        public string GetQuestion() =>
            _question;

        public void AnswerYes()
        {
            Debug.Log("Yes");
        }

        public void AnswerNo()
        {
            OnYesAnswer?.Invoke();
            Debug.Log("No");
        }

    }
}
