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

    [SerializeField] float bitingTime = 7f;
    [SerializeField] bool isFishing;

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
        if (Input.GetMouseButtonDown(0) && currentGameMode == GameMode.NotFishing && !isFishing)
        {
            currentGameMode = GameMode.Fishing;
            bitingTime = 7f;
            isFishing = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentGameMode == GameMode.Fishing)
        {
            currentGameMode = GameMode.NotFishing;
            isFishing = false;
        }

        RunGameMode(currentGameMode);

        if (currentGameMode == GameMode.Fishing)
        {
            if (bitingTime > 0)
            {
                bitingTime -= Time.deltaTime;
            }
            
            if (bitingTime <= 0)
            {
                sliderSystem.SetActive(true);
                animator.SetBool("Reeling", true);
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
        animator.Play("Victory");
        animator.SetBool("Fishing", false);
        animator.SetBool("Reeling", false);
        currentGameMode = GameMode.NotFishing;
    }

    private void FishingFailed()
    {
        animator.Play("Defeat");
        animator.SetBool("Fishing", false);
        animator.SetBool("Reeling", false);
        currentGameMode = GameMode.NotFishing;
    }

    public void ResetFishingState()
    {
        isFishing = false;
    }
}
