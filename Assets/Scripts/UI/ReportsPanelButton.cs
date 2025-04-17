using Mission.Report;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReportsPanelButton : MonoBehaviour
    {
        public Button Button;
        [SerializeField] private Report report;

        private ReportsPanelWindow _reportsPanelWindow;

        private void Awake()
        {
            Button = GetComponentInChildren<Button>();
            _reportsPanelWindow = GetComponentInParent<ReportsPanelWindow>();
        }
        
        public void OpenReport()
        {
            _reportsPanelWindow.CloseCurrentReport();
            
            
            _reportsPanelWindow.SetCurrentReport(report);
            _reportsPanelWindow.OpenCurrentReport();
        }
    }
}