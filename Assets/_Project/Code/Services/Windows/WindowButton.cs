using Code.StaticData.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Services.Windows
{
    public class WindowButton : MonoBehaviour
    {
        private Button _button;

        public event Action ClickEvent;

        private void Awake()
        {
            if (TryGetComponent(out _button))
                _button.onClick.AddListener(()
                    => ClickEvent?.Invoke());
        }
    }
}
