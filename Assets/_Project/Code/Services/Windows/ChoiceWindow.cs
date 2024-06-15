using Code.Gameplay.Interaction;
using Code.Gameplay.Interaction.Dialogues;
using Code.Services.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Services.Windows
{
    public class ChoiceWindow : MonoBehaviour, IWindow
    {
        private TMP_Text _titleText;
        private NPCInteractor _interactor;
        [SerializeField] private TMP_Text _question;
        [SerializeField] private WindowButton _yButton;
        [SerializeField] private WindowButton _nButton;

        IChoiceInteractable _choice;
        private Action _callback;

        private void Awake()
        {
            _titleText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _interactor.TryGetComponent(out _choice);

            _choice.Init();
            SetTitleText();
            SetQuestion();

            _yButton.ClickEvent += _choice.AnswerYes;
            _nButton.ClickEvent += _choice.AnswerNo;
            _choice.OnChange += SetQuestion;
            _choice.OnEnd += Destroy;

            Debug.Log(_titleText.text);
        }

        public void Destroy()
        {
            _yButton.ClickEvent -= _choice.AnswerYes;
            _nButton.ClickEvent -= _choice.AnswerNo;
            _choice.OnChange -= SetQuestion;
            _choice.OnEnd -= Destroy;

            Destroy(gameObject);

            _callback?.Invoke();
        }

        public void SetInteractor(NPCInteractor interactor) =>
            _interactor = interactor;

        public void SetCallback(Action callback = null) =>
            _callback = callback;

        private void SetTitleText() =>
            _titleText.text = _interactor.Data.Name;

        private void SetQuestion()
        {
            _question.text = _choice.GetQuestion();
        }
    }
}
