using System.Collections;
using Manager;
using UnityEngine;

namespace UI.Scenes
{
    public class SecondTutorialScene : UIScene
    {
        [SerializeField] private UIElement reportNotes;
        [SerializeField] private UIElement mainMenuButton;
        [SerializeField] private UIElement openReportButton;
        [SerializeField] private UIElement infoButton;
        [SerializeField] private UIElement quitButton;
        [SerializeField] private UIElement diamondImage;
        
        public override void SkipStep()
        {
            currentStepIndex++;
            PlayTutorial();
        }

        public override void PlayTutorial()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                StopTutorial();
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            ekoBotImage.gameObject.SetActive(true);
            ekoBotImage.sprite = currentStep.EkoBotSprite;

            SetTutorialComponents();

            instructionText.text = string.Empty;

            currentTutorialPanel.Open();

            
            switch (currentStepIndex)
            {
                case 0:
                    OpenStepRoutine(reportNotes);
                    break;
                case 1:
                    OpenStepRoutine(mainMenuButton);
                    break;
                case 2:
                    OpenStepRoutine(infoButton);
                    break;
                case 3:
                    OpenStepRoutine(quitButton);
                    break;
                case 4:
                    OpenStepRoutine(openReportButton);
                    break;
                case 5:
                    OpenStepRoutine(diamondImage);
                    break;
            }
            
            SetText(currentStep.Instruction);
            

            switch (currentStepIndex)
            {
                case 0:
                    CloseStepRoutine(reportNotes);
                    break;
                case 1:
                    CloseStepRoutine(mainMenuButton);
                    break;
                case 2:
                    CloseStepRoutine(infoButton);
                    break;
                case 3:
                    CloseStepRoutine(quitButton);
                    break;
                case 4:
                    CloseStepRoutine(openReportButton);
                    break;
            }
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
            
        }

        private IEnumerator OpenStepRoutine(UIElement uiElement)
        {
            uiElement.Open();
            yield return null;
        }
        
        private IEnumerator CloseStepRoutine(UIElement uiElement)
        {
            uiElement.Close();
            yield return null;
        }
    }
}