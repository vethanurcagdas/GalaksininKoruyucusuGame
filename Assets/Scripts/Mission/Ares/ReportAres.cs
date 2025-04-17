using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class ReportAres : Report.Report
    {
        [Space(10)]
        [SerializeField] private ReportAnswerInputField[] reportAnswers;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CheckAnswers(true);
                OpenPanel();
            }
        }
        protected override void Awake()
        {
            base.Awake();
            reportAnswers = GetComponentsInChildren<ReportAnswerInputField>();
        }

        public override void CheckAnswers(bool isAuto = false)
        {
            if (isAuto)
            {
                isCompleted = true;
                return;
            }
            isCompleted = true;
            foreach (ReportAnswerInputField reportAnswer in reportAnswers)
            {
                if (reportAnswer.CheckAnswer())
                {
                    reportAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    reportAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    isCompleted = false;
                }
            }
        }

        public override void OpenPanel()
        {
            base.OpenPanel();
            if (isCompleted)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedbackPanelSprite;
                feedbackText.text = "Tebrikler! Bütün boşlukları doğru tamamladın.";
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedbackPanelSprite;
                feedbackText.text = "Boşlukları doğru tamamlayamadın, tekrar dene!";
            }
        }
    }
}