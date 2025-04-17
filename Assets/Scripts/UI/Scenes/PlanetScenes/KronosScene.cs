using System;
using System.Collections;
using Mission.Kronos;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class KronosScene : PlanetScene
    {
        public override void StartMission()
        {
            base.StartMission();
            KronosMission kronosMission = CurrentMission as KronosMission;
            if (kronosMission != null) kronosMission.SetDraggableItems(true);
        }

        public override void NextStep()
        {
            currentStepIndex++;
            PlayTutorial();
        }
        
        public override void PlayTutorial()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                StopTutorial();
                Debug.Log("Start mission after tutorial");
                StartMission();
            }

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
            
            if(currentStepIndex == 3) NextMission();
            
            SetText(currentStep.Instruction);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}