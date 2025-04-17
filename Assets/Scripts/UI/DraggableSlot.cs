using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DraggableSlot : MonoBehaviour
{
    [SerializeField] private Image trueImage = null, falseImage = null;
    private ImageType currentType = ImageType.None;
    [SerializeField] private ImageType targetType = ImageType.None;

    public void SetState(ImageType type)
    {
        currentType = type;
        trueImage.gameObject.SetActive(false);
        falseImage.gameObject.SetActive(false);

        switch (currentType)
        {
            case ImageType.True:
                trueImage.gameObject.SetActive(true);
                break;
            case ImageType.False:
                falseImage.gameObject.SetActive(true);
                break;
        }
    }

    public bool IsCorrect(int index)
    {
        bool correct = currentType.Equals(targetType);

        if (!correct)
        {
            GetComponent<Image>().DOColor(Color.red, 1f).OnComplete(delegate
            {
                GetComponent<Image>().DOColor(Color.clear, 0.50f);
            });
        }
        
        return correct;
    }
}