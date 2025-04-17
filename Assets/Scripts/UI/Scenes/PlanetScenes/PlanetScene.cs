using System;
using System.Collections;
using Manager;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.Scenes
{
    public class PlanetScene : UIScene
    {
        [Space(7)] [Header("PLANET PROPERTIES")] 
        public bool IsCompleted;
        [SerializeField] private Sprite infoPanelSprite;
        [SerializeField] private Mission.Mission[] missions;

        [ReadOnly] public Mission.Mission CurrentMission;
        public int currentMissionIndex;

        public void SetIsCompleted(bool state)
        {
            Debug.Log( "PlanetScene: SetIsCompleted: " + state);
            IsCompleted = state;
        }

        private void OnEnable()
        {
            GameManager.Instance.SetInfoPanelSprite(infoPanelSprite);
        }

        public virtual void StartMission()
        {
        }


        public void NextMission()
        {
            Debug.Log( "PlanetScene: Mission Increase To: " + currentMissionIndex+1);
            if (CurrentMission != null) CurrentMission.Deactivate();
            if (currentMissionIndex < missions.Length)
            {
                CurrentMission = missions[currentMissionIndex];
            }

            CurrentMission.Activate();

            GameManager.Instance.SetCurrentMission(CurrentMission);

            currentMissionIndex++;
        }

        public virtual void NextStep()
        {
        }

        public override void PlayTutorial()
        {
        }
    }
}