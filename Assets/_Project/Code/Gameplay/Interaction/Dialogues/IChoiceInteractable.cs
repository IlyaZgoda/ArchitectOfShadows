using System;

namespace Code.Gameplay.Interaction.Dialogues
{
    public interface IChoiceInteractable
    {
        string GetQuestion();
        public event Action OnYesAnswer;
        public event Action OnNoAnswer;

        void AnswerYes();
        void AnswerNo();    

        
        
    }
}