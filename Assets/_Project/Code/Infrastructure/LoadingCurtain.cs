using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] CanvasGroup Curtain;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1f;
        }
                        
        public void Hide() => 
            StartCoroutine(DoFadeIn());
        
        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0f)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }

    }
}
