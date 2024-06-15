using System;

namespace Code.Gameplay.Interaction.Dialogues
{
    public interface IQuestion
    {
        string Text { get; set; }
        public Action OnYesAnswer { get; set; }
        public Action OnNoAnswer { get; set; }
    }
}