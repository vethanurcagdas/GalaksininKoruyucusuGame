using System;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class AresScene : PlanetScene
    {
        public override void StartMission()
        {
            base.StartMission();
            CurrentMission.Open();
            Debug.Log($"{name}: {CurrentMission.name} mission started!");
        }

        public override void NextStep()
        {
            
            currentStepIndex++;
            Debug.Log("step increase!");
            PlayTutorial();
        }
        
        public override void PlayTutorial()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                StopTutorial();
                
                Debug.Log("Start mission after tutorial");

                if (currentTutorial == Tutorials[0] || currentTutorial == Tutorials[2] ||  currentTutorial == Tutorials[4])
                {
                    StartMission();
                }

                else if (currentTutorial == Tutorials[1])
                {
                    CurrentMission.Report.gameObject.SetActive(true);
                    Debug.Log("Report opened!");
                }
                else if (currentTutorial == Tutorials[3])
                {
                    CurrentMission.Report.Open();
                }
                else if (currentTutorial == Tutorials[5])
                {
                    TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
                }
            }

            Debug.Log($"{name}: {currentStepIndex}. step started!");
            currentStep = currentTutorial.Steps[currentStepIndex];
            TutorialStep previousStep = currentStep;
            
            if (currentStepIndex > 0) previousStep = currentTutorial.Steps[currentStepIndex - 1];
            
            if (currentStep.PanelState != previousStep.PanelState) currentTutorialPanel.Close();
            
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
            
            if (currentTutorial == Tutorials[0] && currentStepIndex == 1) NextMission();
            if (currentTutorial == Tutorials[2] && currentStepIndex == 1) NextMission();
            
            SetText(currentStep.Instruction);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}