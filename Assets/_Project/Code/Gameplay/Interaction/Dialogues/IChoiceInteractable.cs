using System;

namespace Code.Gameplay.Interaction.Dialogues
{
    public interface IChoiceInteractable
    {
        public void Init();
        string GetQuestion();

        public event Action OnChange;
        public event Action OnEnd;
        void AnswerYes();
        void AnswerNo();    

        
        
    }
}