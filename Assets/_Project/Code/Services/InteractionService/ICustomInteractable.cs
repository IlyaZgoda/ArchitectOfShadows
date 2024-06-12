using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.InteractionService
{
    public interface ICustomInteractable : IInteractable
    {
        public string GetActionName();
        public void ExecuteCustom();
    }
}
