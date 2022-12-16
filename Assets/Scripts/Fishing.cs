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

    [SerializeField] Animator animator;
    [SerializeField] GameObject fishingStick;
    [SerializeField] GameObject sliderSystem;
    [SerializeField] MashButton mashButton;

    [SerializeField] GameMode currentGameMode;

    float bitingTime = 7f;

    [SerializeField] bool isFishBiting;
    private void Start()
    {
        currentGameMode = GameMode.NotFishing;

        if (animator == null)
        {
            animator = GetComponent<Animator>();    
        }
    }

    private void OnEnable()
    {
        mashButton.OnFishingSucces += FishingSuccess;
        mashButton.OnFishingFailed += FishingFailed;
    }

    private void OnDisable()
    {
        mashButton.OnFishingSucces -= FishingSuccess;
        mashButton.OnFishingFailed -= FishingFailed;
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

        RunGameMode(currentGameMode);

        if (currentGameMode == GameMode.Fishing)
        {
            bitingTime -= Time.deltaTime;

            if (bitingTime <= 0)
            {
                isFishBiting = true;
                sliderSystem.SetActive(true);
                animator.SetBool("Reeling", true);
                bitingTime = 7f;
            }
        }

    }

    private void RunGameMode(GameMode newGameMode)
    {
        switch (newGameMode)
        {
            case GameMode.Fishing:
                animator.SetBool("Fishing", true); 
                fishingStick.SetActive(true);
                break;
                case GameMode.NotFishing:
                animator.SetBool("Fishing", false);
                fishingStick.SetActive(false);
                break;
        }
    }

    private void FishingSuccess()
    {
        animator.SetBool("Reeling", false);
        currentGameMode = GameMode.NotFishing;
    }

    private void FishingFailed()
    {
        animator.SetBool("Reeling", false);
        currentGameMode = GameMode.NotFishing;
    }
}
