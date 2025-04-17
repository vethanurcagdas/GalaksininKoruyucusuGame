using System;
using System.Collections;
using Manager;
using NaughtyAttributes;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Scenes
{
    public abstract class UIScene : UIObject
    {
        [Space(7)] [Header("TUTORIAL PROPERTIES")]
        public TutorialData[] Tutorials;
        public UIElement[] TutorialPanels;
        [ReadOnly] public int CurrentTutorialIndex;
        [SerializeField] protected Image ekoBotImage;
        
        protected TextMeshProUGUI instructionText;
        protected UIElement continueButton;
        protected UIElement undoButton;

        public TutorialData currentTutorial;
        protected UIElement currentTutorialPanel;
        public TutorialStep currentStep;
        public int currentStepIndex;
        

        public void StartTutorial(int index)
        {
            if (Tutorials.Length <= 0) return;

            Debug.Log($"{name}: {CurrentTutorialIndex}.tutorial started!");
            
            currentTutorial = Tutorials[index];
            currentStepIndex = 0;
            PlayTutorial();
        }

        
        public void StopTutorial()
        {
            currentTutorialPanel?.Close();
            
            if (CurrentTutorialIndex >= Tutorials.Length)
            {
                CurrentTutorialIndex = 0;
            }
            else
            {
                CurrentTutorialIndex++;
            }
            
            ekoBotImage.gameObject.SetActive(false);
        }

        public virtual void SkipStep()
        {
            undoButton.Close();
            continueButton.Close();
        }

        public void UndoStep()
        {
            undoButton.Close();
            continueButton.Close();

            currentStepIndex--;
            PlayTutorial();
        }
        
        public abstract void PlayTutorial();
        
        protected void SetTutorialComponents()
        {
            currentTutorialPanel = currentStep.PanelState switch
            {
                PanelState.Middle => TutorialPanels[0],
                PanelState.Upper => TutorialPanels[1],
                _ => throw new ArgumentOutOfRangeException()
            };
            
            instructionText = currentTutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
            continueButton = currentTutorialPanel.transform.Find("ContinueButton").GetComponent<UIElement>();
            undoButton = currentTutorialPanel.transform.Find("UndoButton").GetComponent<UIElement>();
        }
        
        protected void SetText(string text)
        {
            string[] words = text.Split(' ');
            
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains("<playerName>"))
                {
                    words[i] = words[i].Replace("<playerName>", GameManager.Instance.PlayerName);
                }
            }
            
            string newText = string.Join(" ", words);
            
            // Set the size of the text
            instructionText.enableAutoSizing = true;
            instructionText.text = newText;
            instructionText.ForceMeshUpdate();
            instructionText.enableAutoSizing = false;
        }
    }
}