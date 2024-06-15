using Code.Gameplay.Interaction;
using Code.Services.InteractionService;
using Code.StaticData.Windows;
using Code.UI.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Services.Windows
{
    public class InteractionWindow : MonoBehaviour, IWindow
    {
        private TMP_Text _titleText;
        private TMP_Text _descriptionText;
        private BaseInteractor _interactor;

        [SerializeField] private WindowButton _researchButton;
        [SerializeField] private WindowButton _actionButton;
        [SerializeField] private WindowButton _closeButton;
        [SerializeField] private WindowButton _backButton;
        [SerializeField] private GameObject _tittlePanel;
        [SerializeField] private GameObject _researchPanel;

        private event Action _onResearchButtonClick;
        private event Action _onActionButtonClick;
        private event Action _onCloseButtonClick;
        private event Action _onBackButtonClick;
        
        private void Awake()
        {
            _titleText = GetComponentInChildren<TMP_Text>();
            _descriptionText = _researchPanel.GetComponentInChildren<TMP_Text>();

            InitializeEventHandlers();
            SubscribeOnResearchButton();
            SubscribeOnCloseButton();
            SubscribeOnBackButton();
        }

        private void InitializeEventHandlers()
        {
            _onResearchButtonClick = () =>
            {
                DeactivateTittlePanel();
                ActivateResearchPanel();
                SetDescription();
                ActivateBackButton();
            };

            _onCloseButtonClick = () =>
            Destroy(gameObject);

            _onBackButtonClick = () =>
            {
                DeactivateResearchPanel();
                ActivateTittlePanel();
                DeactivateBackButton();
            };
        }

        private void Start()
        {     
            SetTitleText();
            SetCustom();
        }          

        public void SetInteractor(BaseInteractor interactor) =>      
            _interactor = interactor;   
        
        private ICustomInteractable GetCustom()
        {
            ICustomInteractable custom;

            return (_interactor.TryGetComponent(out custom)) ? custom : null;

        }

        private void SetCustom()
        {
            var custom = GetCustom();

            if (custom != null)
            {
                var button =_actionButton.GetComponent<Button>();
                button.gameObject.SetActive(true);
                var text = button.GetComponentInChildren<TMP_Text>();
                text.text = custom.GetActionName();

                _onActionButtonClick = () =>
                {
                    custom.ExecuteCustom();
                    Destroy(gameObject);
                };
                
                SubscribeOnActionButton();
            }
        }

        private void SetTitleText() =>        
            _titleText.text = _interactor.Data.Name;     

        private void SetDescription() =>
            _descriptionText.text = _interactor.Data.Description;

        private void ActivateResearchPanel() =>
            _researchPanel.SetActive(true);
              
        private void DeactivateResearchPanel() =>
            _researchPanel.SetActive(false);

        private void ActivateTittlePanel() =>
            _tittlePanel.SetActive(true);

        private void DeactivateTittlePanel() =>
            _tittlePanel.SetActive(false);

        private void ActivateBackButton()
        {
            var button = _backButton.GetComponent<Button>();
            button.gameObject.SetActive(true);
        }

        private void DeactivateBackButton()
        {
            var button = _backButton.GetComponent<Button>();
            button.gameObject.SetActive(false);
        }

        private void SubscribeOnActionButton() =>
            _actionButton.ClickEvent += _onActionButtonClick;

        private void SubscribeOnResearchButton() =>      
            _researchButton.ClickEvent += _onResearchButtonClick;

        private void SubscribeOnCloseButton() =>
            _closeButton.ClickEvent += _onCloseButtonClick;

        private void SubscribeOnBackButton() =>   
            _backButton.ClickEvent += _onBackButtonClick;

        private void UnsubscribeOnActionButton() =>
            _actionButton.ClickEvent -= _onActionButtonClick;

        private void UnsubscribeOnResearchButton() =>
            _researchButton.ClickEvent -= _onResearchButtonClick;

        private void UnsubscribeOnCloseButton() =>
            _closeButton.ClickEvent -= _onCloseButtonClick;

        private void UnsubscribeOnBackButton() =>
            _backButton.ClickEvent -= _onBackButtonClick;

        private void OnDestroy()
        {
            UnsubscribeOnActionButton();
            UnsubscribeOnResearchButton();
            UnsubscribeOnCloseButton();
            UnsubscribeOnBackButton();
        }
    }
}
