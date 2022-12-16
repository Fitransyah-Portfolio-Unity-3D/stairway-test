using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject sliderSystem;
    [SerializeField] MashButton mashButton;

    private void OnEnable()
    {
        mashButton.OnFishingSucces += HideSliderSystem;
        mashButton.OnFishingFailed += HideSliderSystem;
    }

    private void OnDisable()
    {
        mashButton.OnFishingSucces -= HideSliderSystem;
        mashButton.OnFishingFailed -= HideSliderSystem;
    }
    public void ShowSliderSystem()
    {
        sliderSystem.SetActive(true);
    }
    private void HideSliderSystem()
    {
        sliderSystem.SetActive(false);
    }
}
