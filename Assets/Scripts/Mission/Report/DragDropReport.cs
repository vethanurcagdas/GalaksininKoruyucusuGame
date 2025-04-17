using System;
using DragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Report
{
    public class DragDropReport : Report
    {
        [SerializeField] private DraggableItem[] draggableItemAnswers;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CheckAnswers(true);
                OpenPanel();
            }
        }

        public override void CheckAnswers(bool isAuto = false)
        {
            if (isAuto)
            {
                isCompleted = true;
                return;
            }
            isCompleted = true;
            
            foreach (DraggableItem draggableItem in draggableItemAnswers)
            {
                Debug.Log(draggableItem.gameObject.name + " : " + draggableItem.IsContained);
                if (draggableItem.IsContained) continue;
                
                isCompleted = false;
            }
        }

        public override void OpenPanel()
        {
            base.OpenPanel();
            if (isCompleted)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedbackPanelSprite;
                feedbackText.text = correctFeedbackText;
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedbackPanelSprite;
                feedbackText.text = wrongFeedbackText;
            }
        }
    }
}