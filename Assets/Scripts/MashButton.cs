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

    public float sliderStartingValue;
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
        sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderMinValue, 1.8f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E))
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, 80f * Time.deltaTime);

           if (sliderMain.value >= 9.7f)
            {
                Debug.Log("Got Fish,yeay!");

                sliderStartingValue = 4f;

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

            sliderStartingValue = 4f;

            if (OnFishingFailed != null)
            {
                OnFishingFailed();
            }


        }
    }
}
