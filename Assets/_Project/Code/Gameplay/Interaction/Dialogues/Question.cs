using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Gameplay.Interaction.Dialogues
{
    public class Question : IQuestion
    {
        private string _text;
        public string Text { get => _text; set => _text = value; }

        public Action OnYesAnswer { get; set; }
        public Action OnNoAnswer { get; set; }

    }
}
