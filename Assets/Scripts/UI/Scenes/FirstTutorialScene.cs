using System;
using Manager;
using TMPro;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class FirstTutorialScene : UIScene
    {
        [SerializeField] private UIElement playerNameInputField;
        
        public override void SkipStep()
        {
            if (currentStepIndex == 1)
            {
                TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
                GameManager.Instance.SetPlayerName(inputField.text);
                playerNameInputField.Deactivate();
            }
            
            currentStepIndex++;
            PlayTutorial();
        }
        
        public override void PlayTutorial()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                StopTutorial();
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.SecondTutorialScene);
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            switch (currentStep.PanelState)
            {
                case PanelState.Upper:
                    ekoBotImage.gameObject.SetActive(false);
                    break;
                case PanelState.Middle:
                    ekoBotImage.gameObject.SetActive(true);
                    ekoBotImage.sprite = currentStep.EkoBotSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            SetText(currentStep.Instruction);
            
            
            continueButton.Open();
            if (currentStepIndex == 1)
            {
                GetPlayerNameRoutine();
            }
            
            if (currentStepIndex > 1) undoButton.Open();
        }

        private void GetPlayerNameRoutine()
        {
            playerNameInputField.Activate();
            playerNameInputField.Open();
            continueButton.Close();
            undoButton.Close();
            
            TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
            inputField.onValueChanged.AddListener(
                delegate
                {
                    if (inputField.text.Length > 0)
                    {
                        continueButton.Open();
                    }
                    else
                    {
                        continueButton.Close();
                        undoButton.Close();
                    }
                });
        }
    }
}