using Code.Gameplay.Interaction;
using Code.Gameplay.Interaction.Dialogues;
using Code.Services.Windows.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

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

        private IWindowFactory<NPCInteractor> _choiceWindowFactory;

        [Inject]
        public void Construct(ChoiceWindowFactory windowFactory) =>
            _choiceWindowFactory = windowFactory;

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
            {
                gameObject.SetActive(false);

                IChoiceInteractable choice;
                if (_interactor.TryGetComponent(out choice))
                {
                    _choiceWindowFactory.CreateWindow(transform.parent, _interactor);
                }
            }
                
        }

        public void SetInteractor(NPCInteractor interactor)
        {
            _interactor = interactor;
        }

        private void SetTitleText() =>
            _titleText.text = _interactor.Data.Name;

        private void SetLines() =>
            lines = _interactor.Data.Lines;

        public void Set(Transform position)
        {
            gameObject.transform.SetParent(position);
            gameObject.transform.position = transform.parent.position;

        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
