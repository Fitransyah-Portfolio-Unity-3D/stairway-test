using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StairwayTest/Create Infromation Text", fileName = "New Information Text")]
public class InformationText : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string message;

    public string GetMessage()
    {
        return message;
    }
}
