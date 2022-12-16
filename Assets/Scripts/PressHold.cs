using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressHold : MonoBehaviour
{
    [SerializeField] Slider sliderMain;
    [SerializeField] TMP_Text sliderCounter;
    [SerializeField] TMP_Text sliderStatus;
    [SerializeField] TMP_Text sliderColorStatus;
    [SerializeField] Gradient sliderGradientColor;
    [SerializeField] Gradient sliderPlainColor;
    [SerializeField] Image sliderFill;

    public float sliderStartingValue;
    public float sliderEndValue;
    public float sliderMinValue;

    public bool sliderBGFill;

    void Start()
    {
        sliderEndValue = sliderStartingValue * 10f;
        sliderMinValue = 0f;
        sliderCounter.text = sliderMinValue.ToString();
        sliderMain.maxValue = sliderEndValue;
        sliderFill.color = sliderGradientColor.Evaluate(1f);
    }

    void Update()
    {
        // call a funtion for sequence or an event to next action
        // the cast target stop
        // cast animation cast is playing
        // phyisics for bait into target position

        //return;

        if (Input.GetKey(KeyCode.E))
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, 1.5f * Time.deltaTime);
            sliderStatus.text = "The button E is in hold state";
        }
        else
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderMinValue, 4f * Time.deltaTime);
            sliderStatus.text = "The button is released or no action performed";

        }

        sliderMain.value = sliderStartingValue;
        sliderCounter.text = Mathf.RoundToInt(sliderStartingValue).ToString();

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!sliderBGFill)
            {
                sliderBGFill = true;
            }
            else
            {
                sliderBGFill = false;
            }
        }

        if (sliderBGFill)
        {
            sliderFill.color = sliderGradientColor.Evaluate(sliderMain.normalizedValue);
            sliderColorStatus.text = "BG Fill is in gradient";
        }
        else
        {
            sliderFill.color = sliderPlainColor.Evaluate(sliderMain.normalizedValue);
            sliderColorStatus.text = "BG is in plain color";
        }
    }
}
