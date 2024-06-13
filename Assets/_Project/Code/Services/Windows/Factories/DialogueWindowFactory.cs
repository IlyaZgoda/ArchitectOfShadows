using Code.Gameplay.Interaction;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public class DialogueWindowFactory : IWindowFactory<NPCInteractor>
    {
        public IWindow CreateWindow(Transform position, NPCInteractor interactor)
        {
            var prefab = Resources.Load<GameObject>("HUD/Windows/DialogueWindow");
            GameObject dialogueWindow = Object.Instantiate(prefab, position);
            var window = dialogueWindow.GetComponent<DialogueWindow>();
            window.SetInteractor(interactor);

            return window;
        }
    }
}
