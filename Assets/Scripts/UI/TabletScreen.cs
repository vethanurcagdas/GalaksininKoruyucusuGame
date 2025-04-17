using System;
using BrunoMikoski.AnimationSequencer;
using TMPro;
using UI;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

public class TabletScreen : MonoBehaviour
{
    [SerializeField] private AnimalData[] animalDatas = Array.Empty<AnimalData>();

    [SerializeField] private Image animalImage = null;
    [SerializeField] private TextMeshProUGUI animalNameText = null;
    [SerializeField] private TextMeshProUGUI animalFoodChainText = null;
    [SerializeField] private TextMeshProUGUI animalNutritionText = null;
    [SerializeField] private TextMeshProUGUI animalEnergySourceText = null;
    [SerializeField] private Button previousButton = null;
    
    [SerializeField] private UIElement middleTutorialPanel = null;
    [SerializeField] private UIElement middleTutorialNextButton = null;
    [SerializeField] private UIElement middleTutorialBackButton = null;
    [SerializeField] private TextMeshProUGUI  middleTutorialText = null;
    [SerializeField] private Image ekoImage = null;
    [SerializeField] private GameObject firstReport = null;
    
    public AresScene aresScene;

    [SerializeField] private int currentAnimalDataIndex = 0;

    private AnimalData CurrentAnimalData => animalDatas[currentAnimalDataIndex];

    private void OnEnable()
    {
        currentAnimalDataIndex = 0;
        DrawAnimalData();
    }

    private void DrawAnimalData()
    {
        previousButton.gameObject.SetActive(currentAnimalDataIndex > 0);
        
        animalImage.sprite = CurrentAnimalData.AnimalSprite;

        animalNameText.SetText(CurrentAnimalData.AnimalName);
        animalFoodChainText.SetText(GetAnimalFoodChain());
        animalNutritionText.SetText(GetAnimalNutrition());
        animalEnergySourceText.SetText(GetAnimalEnergySource());
    }

    public void NextAnimal()
    {
        ++currentAnimalDataIndex;
        if (currentAnimalDataIndex >= animalDatas.Length)
        {
            //TUM HAYVANLARI KONTROL ETTI
            
            gameObject.SetActive(false);
            aresScene.StartTutorial(1);
            

            // middleTutorialText.SetText("Daha temkinli ilerlemek için yaptıklarını not almalısın. Bunları raporlamamız gerekecek.");
            // middleTutorialPanel.Open();
            // middleTutorialNextButton.Open();
            // middleTutorialBackButton.Open();
            // ekoImage.gameObject.SetActive(true);
            //
            // Button backButton = middleTutorialBackButton.GetComponent<Button>();
            // Button nextButton = middleTutorialNextButton.GetComponent<Button>();
            //
            // backButton.onClick.RemoveListener(Back);
            // nextButton.onClick.RemoveListener(Next);
            //
            // backButton.onClick.AddListener(Back);
            // nextButton.onClick.AddListener(Next);
            // return;
        }
        DrawAnimalData();
    }

    private void TutoPanelCloseAnimationOnFinishedEvent()
    {
        firstReport.SetActive(true);
    }
    
    private void Next()
    {
        middleTutorialPanel.Close();
        middleTutorialPanel.CloseAnimation.OnFinishedEvent.RemoveListener(TutoPanelCloseAnimationOnFinishedEvent);
        middleTutorialPanel.CloseAnimation.OnFinishedEvent.AddListener(TutoPanelCloseAnimationOnFinishedEvent);
        
        ekoImage.gameObject.SetActive(false);
    }
    private void Back()
    {
        ekoImage.gameObject.SetActive(false);
        middleTutorialPanel.Close();
        middleTutorialPanel.CloseAnimation.OnFinishedEvent.AddListener(delegate
        {
            gameObject.SetActive(true);
        });
    }
    public void PreviousAnimal()
    {
        --currentAnimalDataIndex;
        DrawAnimalData();
    }

    public string GetAnimalFoodChain()
    {
        return CurrentAnimalData.AnimalFoodChain switch
        {
            AnimalFoodChain.Creator => "Üretici",
            AnimalFoodChain.FirstConsumer => "Birincil tüketici",
            AnimalFoodChain.SecondConsumer => "İkincil tüketici",
            AnimalFoodChain.ThirdConsumer => "Üçüncül tüketici",
            AnimalFoodChain.Shredder => "Ayrıştırıcı",
            _ => "DEFAULT ARM",
        };
    }

    public string GetAnimalNutrition()
    {
        return CurrentAnimalData.AnimalNutrition switch
        {
            AnimalNutrition.Photosynthesis => "Fotosentez yapar",
            AnimalNutrition.Herbivorous => "Otobur",
            AnimalNutrition.Carnivorous => "Etobur",
            AnimalNutrition.Saprophyte => "Çürükçül",
            _ => "DEFAULT ARM",
        };
    }

    public string GetAnimalEnergySource()
    {
        return CurrentAnimalData.AnimalEnergySource switch
        {
            AnimalEnergySource.Sun => "Güneş",
            AnimalEnergySource.Plants => "Bitkiler",
            AnimalEnergySource.Herbivores => "Otçullar",
            AnimalEnergySource.Carnivores => "Etçiller",
            AnimalEnergySource.OrganicWaste => "Organik Atıklar",
            _ => "DEFAULT ARM",
        };
    }
}

[System.Serializable]
public class AnimalData
{
    [SerializeField] private Sprite animalSprite = null;
    [SerializeField] private string animalName = string.Empty;
    [SerializeField] private AnimalFoodChain animalFoodChain = AnimalFoodChain.Creator;
    [SerializeField] private AnimalNutrition animalNutrition = AnimalNutrition.Photosynthesis;
    [SerializeField] private AnimalEnergySource animalEnergySource = AnimalEnergySource.Sun;


    public Sprite AnimalSprite => animalSprite;
    public string AnimalName => animalName;
    public AnimalFoodChain AnimalFoodChain => animalFoodChain;
    public AnimalNutrition AnimalNutrition => animalNutrition;
    public AnimalEnergySource AnimalEnergySource => animalEnergySource;
}

public enum AnimalFoodChain
{
    Creator = 0,
    FirstConsumer = 1,
    SecondConsumer = 2,
    ThirdConsumer = 3,
    Shredder = 4,
}

public enum AnimalNutrition
{
    Photosynthesis = 0,
    Herbivorous = 1,
    Carnivorous = 2,
    Saprophyte = 3,
}

public enum AnimalEnergySource
{
    Sun = 0,
    Plants = 1,
    Herbivores = 2,
    Carnivores = 3,
    OrganicWaste = 4,
}