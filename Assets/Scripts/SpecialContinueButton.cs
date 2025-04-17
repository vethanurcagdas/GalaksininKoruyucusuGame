using System;
using UI;
using UI.Scenes;
using UnityEngine;

public class SpecialContinueButton : MonoBehaviour
{
    [SerializeField] private AresScene aresScene = null;

    [SerializeField] private UIElement selfContinueButton = null;
    [SerializeField] private UIElement otherContinueButton = null;
    [SerializeField] private UIElement otherBackButton = null;

    private void OnEnable()
    {
        if (!aresScene || aresScene.CurrentTutorialIndex > 0)
        {
            gameObject.SetActive(false);
            return;
        }

        otherContinueButton.gameObject.SetActive(false);
        otherBackButton.gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
        selfContinueButton.Open();
    }
}