using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Interaction.Dialogues
{
    public class Dialogue9 : BaseDialog
    {
        public override void Init()
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
    }
}
