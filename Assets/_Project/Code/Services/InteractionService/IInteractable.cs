using Code.Services.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.InteractionService
{
    public interface IInteractable 
    {
        public IWindow Interact(Action callback = null);
    }
}
