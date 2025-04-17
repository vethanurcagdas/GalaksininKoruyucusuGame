using UI;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using BrunoMikoski.AnimationSequencer;
using Mission.Report;
using UI.Scenes;

public class AnimalReport : Report
{
    [SerializeField] private AnimalReportData[] datas = Array.Empty<AnimalReportData>();
    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private Button nextButton = null;

    [SerializeField] private GameObject tablet = null;

    [SerializeField] private Sprite correctFeedbackBackground = null, incorrectFeedbackBackground = null;

    [SerializeField] private UIElement infoPanel = null;
    [SerializeField] private UIElement infoCloseButton = null;
    [SerializeField] private AresScene aresScene = null;
    [SerializeField] private Mission.Mission currentMission = null;
    [SerializeField] private Mission.Mission nextMission = null;
    [SerializeField] private AnimationSequencerController nextMissionController = null;

    [SerializeField] private int currentIndex = 0;
    private AnimalReportData CurrentReportData => datas[currentIndex];

    private readonly List<DraggableSlot> _draggableSlots = new List<DraggableSlot>();

    private static readonly string[] CorrectFeedbackTexts = new string[5]
    {
        "Tebrikler! Üretici canlıların ortak özelliklerini de görmüş oldun.",
        "Tebrikler! Birincil tüketici canlıların ortak özelliklerini de görmüş oldun.",
        "Tebrikler! İkincil tüketici canlıların ortak özelliklerini de görmüş oldun.",
        "Tebrikler! Üçüncül tüketici canlıların ortak özelliklerini de görmüş oldun.",
        "Tebrikler! Ayrıştırıcı canlıların ortak özelliklerini de görmüş oldun.",
    };

    private const string WrongFeedbackText = "Bazı bilgileri yanlış doldurdun. Lütfen tabloyu tekrar gözden geçir.";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // CheckAnswers(true); 
            OnClickNextButton(true);
        }
    }

    protected override void Start()
    {
        infoPanel.Open();
        infoCloseButton.gameObject.SetActive(true);
        infoCloseButton.Open();
        infoCloseButton.GetComponent<Button>().onClick.AddListener(delegate { infoPanel.Close(); });

        nextButton.onClick.AddListener(OnClickNextButton);
        currentIndex = 0;
        Setup();
    }

    private void Setup()
    {
        titleText.SetText(CurrentReportData.Title);

        if (currentIndex - 1 >= 0)
            datas[currentIndex - 1].Table.SetActive(false);

        CurrentReportData.Table.SetActive(true);
        _draggableSlots.Clear();
        _draggableSlots.AddRange(CurrentReportData.Table.GetComponentsInChildren<DraggableSlot>());
    }

    private void OnClickNextButton() => OnClickNextButton(false);

    private void OnClickNextButton(bool autoValue = false)
    {
        bool checkValue = Check();
        if (autoValue)
            checkValue = true;

        feedbackText.SetText(checkValue ? CorrectFeedbackTexts[currentIndex] : WrongFeedbackText);
        feedbackPanel.GetComponent<Image>().sprite = checkValue ? correctFeedbackBackground : incorrectFeedbackBackground;
        feedbackPanel.Open();
        if (checkValue)
        {
            feedbackPanel.OpenAnimation.OnFinishedEvent.AddListener(delegate
            {
                currentIndex++;
                if (currentIndex >= datas.Length)
                {
                    currentMission.gameObject.SetActive(false);
                    aresScene.StartTutorial(2);
                }

                Setup();
            });
        }
    }

    private bool Check()
    {
        bool allCorrect = true;

        foreach (DraggableSlot slot in _draggableSlots)
        {
            bool isCorrect = slot.IsCorrect(currentIndex);

            if (!isCorrect)
            {
                allCorrect = false;
            }
        }

        return allCorrect;
    }

    public void OnCloseFeedback()
    {
    }

    public void ContinueAfterFeedback()
    {
        feedbackPanel.Close();
    }
}

[System.Serializable]
public class AnimalReportData
{
    [SerializeField] private string title = null;
    [SerializeField] private GameObject table = null;

    public string Title => title;
    public GameObject Table => table;
}