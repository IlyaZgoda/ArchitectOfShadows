using Code.Gameplay.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Services.Windows
{
    public class DialogueWindow : MonoBehaviour, IWindow
    {
        [SerializeField] TextMeshProUGUI text;
        private string[] lines;
        [SerializeField] private float speed;

        private TMP_Text _titleText;
        private int index;
        private NPCInteractor _interactor;

        private void Awake()
        {
            _titleText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            SetTitleText();

            text.text = string.Empty;

            SetLines();
            StartDialogue();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (text.text == lines[index])
                    NextLine();

                else
                {
                    StopAllCoroutines();
                    text.text = lines[index];
                }           
            }
        }

        private void StartDialogue()
        {
            index = 0;
            StartCoroutine(TypeLine());
        }

        private IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                text.text += c;
                yield return new WaitForSeconds(speed);
            }
        }

        private void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                text.text = string.Empty;

                StartCoroutine(TypeLine());
            }

            else
                gameObject.SetActive(false);
        }

        public void SetInteractor(NPCInteractor interactor)
        {
            _interactor = interactor;
        }

        private void SetTitleText() =>
            _titleText.text = _interactor.Data.Name;

        private void SetLines() =>
            lines = _interactor.Data.Lines;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
