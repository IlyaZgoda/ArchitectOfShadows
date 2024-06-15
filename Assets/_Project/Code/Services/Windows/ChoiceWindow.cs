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

        private void Awake()
        {
            _titleText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _interactor.TryGetComponent(out _choice);
            SetTitleText();
            SetQuestion();

            _yButton.ClickEvent += _choice.AnswerYes;
            _nButton.ClickEvent += _choice.AnswerNo;
            _yButton.ClickEvent += Destroy;
            _nButton.ClickEvent += Destroy;
        }

        public void Destroy()
        {
            _yButton.ClickEvent -= _choice.AnswerYes;
            _nButton.ClickEvent -= _choice.AnswerNo;
            _yButton.ClickEvent -= Destroy;
            _nButton.ClickEvent -= Destroy;

            Destroy(gameObject);
        }

        public void SetInteractor(NPCInteractor interactor) =>
            _interactor = interactor;

        private void SetTitleText() =>
            _titleText.text = _interactor.Data.Name;

        private void SetQuestion()
        {
            _question.text = _choice.GetQuestion();
        }
    }
}
