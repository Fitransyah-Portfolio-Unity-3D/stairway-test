using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Casting : MonoBehaviour
{
    [SerializeField] TMP_Text debugText;
    
    float castingPower = 0f;
    float castingSpeed = 100f;

    public bool isPowerMove = false;
    bool isDirectionRight = true;

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            StartCasting();
        }
        else if (!Input.GetMouseButton(0))
        {
            EndCasting();
        }

        if (isPowerMove)
        {
            CastingActive();
        }
    }

    private void CastingActive()
    {
        if (isDirectionRight)
        {
            castingPower += Time.deltaTime * castingSpeed;
            if (castingPower > 100f)
            {
                isDirectionRight = false;
                castingPower = 100f;
            }
        }
        else
        {
            castingPower -= Time.deltaTime * castingSpeed;
            if (castingPower < 0)
            {
                isDirectionRight = true;
                castingPower = 0f;
            }
        }

        Debug.Log(castingPower);
        debugText.text = castingPower.ToString();
    }
    private void StartCasting()
    {
        isPowerMove = true;
        castingPower = 0f;
        isDirectionRight = true;
    }

    private void EndCasting()
    {
        isPowerMove = false;
    }
}
