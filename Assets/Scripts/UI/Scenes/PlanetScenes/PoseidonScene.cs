using System;
using System.Collections;
using DG.Tweening;
using Mission.Poseidon;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes
{
    public class PoseidonScene : PlanetScene
    {
        [SerializeField] private Button okButton;
        private Camera _mainCam;
        private Machine _machine;
        
        [Space(7)] [Header("CAMERA SHAKE PROPERTIES")]
        [SerializeField] private float strength;
        [SerializeField] private int vibrato;
        [SerializeField] private float randomness;
        [SerializeField] private bool fadeOut;
        [SerializeField] private ShakeRandomnessMode shakeRandomnessMode;
        
        [Space(7)] [Header("FEEDBACK PANEL PROPERTIES")]
        [SerializeField] private UIElement feedbackPanel;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private Sprite correctFeedback;
        [SerializeField] private Sprite wrongFeedback;
        
        private void Awake()
        {
            _mainCam = Camera.main;
            _machine = GetComponentInChildren<Machine>();
        }

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

                if (currentTutorial == Tutorials[1])
                {
                    _machine.EnterOrganism();
                }
                
                if (currentTutorial == Tutorials[0]) StartMission();
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
            
            SetText(currentStep.Instruction);
            
            if (currentTutorial == Tutorials[0] && currentStepIndex == 3)
            {
                NextMission();
            }
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }

        public void BreakMachine()
        {
            _mainCam.DOShakePosition(1, strength, vibrato, randomness, fadeOut, shakeRandomnessMode).OnComplete(() =>
            {
                okButton.interactable = false;
                StartTutorial(1);
            });
        }
        
        public void OpenPanel(bool state)
        {
            feedbackPanel.Open();
            if (state)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedback;
                feedbackText.text = "Harika. Bu canlının piramidin hangi basamağına ait olduğunu kolaylıkla buldun.";
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedback;
                feedbackText.text = "Bu canlıyı yanlış basamağa yerleştirdin. Tekrar dene!";
            }
        }
        
        public void ClosePanel()
        {
            feedbackPanel.Close();
        }
    }
}