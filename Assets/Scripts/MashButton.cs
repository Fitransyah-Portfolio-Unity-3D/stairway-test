using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashButton : MonoBehaviour
{
    [SerializeField] Slider sliderMain;
    [SerializeField] Gradient sliderGradientColor;
    [SerializeField] Image sliderFill;

    public float sliderStartingValue = 5f;
    public float sliderEndValue;
    public float sliderMinValue;


    public event Action OnFishingSucces;
    public event Action OnFishingFailed;

    private void Start()
    {
        sliderEndValue = 10f;
        sliderMinValue = 0f;
        sliderMain.maxValue = sliderEndValue;
        sliderFill.color = sliderGradientColor.Evaluate(1f);
    }

    private void Update()
    {
        sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderMinValue, 2f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E))
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, 86f * Time.deltaTime);

           if (sliderMain.value >= 9.2f)
            {
                Debug.Log("Got Fish,yeay!");

                if (OnFishingSucces != null)
                {
                    OnFishingSucces();
                }
            }

        }

        sliderMain.value = sliderStartingValue;
        sliderFill.color = sliderGradientColor.Evaluate(sliderMain.normalizedValue);

        if (sliderMain.value <= 0f)
        {
            Debug.Log("Fish Lost :(");

            if (OnFishingFailed != null)
            {
                OnFishingFailed();
            }
        }
    }
}
