using System;
using System.Collections;
using Mission.Earth;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes
{
    public class EarthScene : PlanetScene
    {
        public override void StartMission()
        {
            base.StartMission();
            EarthMission mission = CurrentMission as EarthMission;
            
            float value = 0;
            
            if (currentTutorial == Tutorials[0])
            {
                mission.SetMissionPhase(MissionPhase.Tutorial);
                value = 5;
            }
            else if (currentTutorial == Tutorials[1])
            {
                mission.SetMissionPhase(MissionPhase.Grass);
                value = 0;
            }
            else if (currentTutorial == Tutorials[2])
            {
                mission.SetMissionPhase(MissionPhase.Rabbit);
                value = 0;
            }
            else if (currentTutorial == Tutorials[3])
            {
                mission.SetMissionPhase(MissionPhase.Owl);
                value = 0;
            }

            mission.Initialize(value);
        }

        public override void NextStep()
        {
            if (currentTutorial == Tutorials[0] && currentStepIndex == 0) NextMission();
            
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

            if (currentStep.PanelState != previousStep.PanelState)
            {
                currentTutorialPanel.Close();
            }
            
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
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}