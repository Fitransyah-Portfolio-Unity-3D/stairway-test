using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    enum GameMode
    {
        Fishing,
        NotFishing
    }

    [SerializeField] GameMode currentGameMode;
    private void Start()
    {
        currentGameMode = GameMode.NotFishing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentGameMode == GameMode.NotFishing)
        {
            currentGameMode = GameMode.Fishing; 
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentGameMode == GameMode.Fishing)
        {
            currentGameMode = GameMode.NotFishing;
        }
    }
}
