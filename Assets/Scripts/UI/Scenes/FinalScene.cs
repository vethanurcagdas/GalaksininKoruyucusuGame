using System.Collections;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scenes
{
    public class FinalScene : UIScene
    {
        [SerializeField] private UIElement certificate;
        
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

                certificate.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.PlayerName;
                certificate.Open();
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            ekoBotImage.gameObject.SetActive(true);
            ekoBotImage.sprite = currentStep.EkoBotSprite;
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            SetText(currentStep.Instruction);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }

        public void PlayAgainButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}