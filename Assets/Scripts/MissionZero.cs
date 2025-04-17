using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class MissionZero : MonoBehaviour
{
   [SerializeField] private UIElement feedbackPanel = null;
   [SerializeField] private UIElement willCloseContinueButton = null;
   [SerializeField] private GameObject tablet = null;

   private void Awake()
   {
      willCloseContinueButton.gameObject.SetActive(true);
   }

   private void Start()
   {
      willCloseContinueButton.gameObject.transform.localScale = Vector3.zero;
      
      willCloseContinueButton.GetComponent<Button>().onClick.AddListener(delegate
      {
         feedbackPanel.Close();
         tablet.SetActive(true);
      });
   }
}
