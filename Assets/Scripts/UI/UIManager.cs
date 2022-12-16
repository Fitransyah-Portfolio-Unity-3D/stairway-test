using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject sliderSystem;
    [SerializeField] MashButton mashButton;
    [SerializeField] Fishing fishing;

    [SerializeField] Image panel;
    [SerializeField] TMP_Text infoText;

    [SerializeField] InformationText[] informationTexts;

    private void Start()
    {
        ShowInformationText("FishingCancel");
    }

    private void OnEnable()
    {
        mashButton.OnFishingSucces += HideSliderSystem;
        mashButton.OnFishingFailed += HideSliderSystem;
        fishing.OnFishingCancelled += HideSliderSystem;
        fishing.OnFishingEvent += ShowInformationText;
    }

    private void OnDisable()
    {
        mashButton.OnFishingSucces -= HideSliderSystem;
        mashButton.OnFishingFailed -= HideSliderSystem;
        fishing.OnFishingCancelled -= HideSliderSystem;
        fishing.OnFishingEvent -= ShowInformationText;
    }
    public void ShowSliderSystem()
    {
        sliderSystem.SetActive(true);
    }
    private void HideSliderSystem()
    {
        sliderSystem.SetActive(false);
    }

    private void ShowInformationText(string infoName)
    {
        foreach (InformationText info in informationTexts)
        {
            if (info.name == infoName)
            {
                infoText.text = info.GetMessage();
            }
        }
    }
}
