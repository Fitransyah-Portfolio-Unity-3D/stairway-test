using System;
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
    [SerializeField] GameObject fishPrefab;

    [SerializeField] AudioSource reelInSound;
    [SerializeField] AudioSource fishingResultSound;
    [SerializeField] AudioClip succesFishingSound;
    [SerializeField] AudioClip failedFishingSound;


    [SerializeField] GameMode currentGameMode;

    [SerializeField] float bitingTime = 7f;
    [SerializeField] bool isFishing;

    public event Action OnFishingCancelled;
    public event Action<string> OnFishingEvent;

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
        // Game State 
        RunGameMode(currentGameMode);

        // Player start fishing
        if (Input.GetMouseButtonDown(0) && currentGameMode == GameMode.NotFishing && !isFishing)
        {
            currentGameMode = GameMode.Fishing;
            bitingTime = 7f;
            isFishing = true;

            if (OnFishingEvent != null)
            {
                OnFishingEvent("FishingStart");
            }

        }
        // Player cancel fishing
        else if (Input.GetMouseButtonDown(1) && currentGameMode == GameMode.Fishing)
        {
            currentGameMode = GameMode.NotFishing;
            isFishing = false;

            if (OnFishingCancelled != null)
            {
                OnFishingCancelled();
            }


            if (OnFishingEvent != null)
            {
                OnFishingEvent("FishingCancel");
            }
        }
        // Fish eating the bait
        if (currentGameMode == GameMode.Fishing)
        {
            if (bitingTime > 0)
            {
                bitingTime -= Time.deltaTime;
            }
            
            if (bitingTime <= 0)
            {
                reelInSound.Play();
                sliderSystem.SetActive(true);
                animator.SetBool("Reeling", true);
                
                if (OnFishingEvent != null)
                {
                    OnFishingEvent("FishingBites");
                }
            }
        }

        // Quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
                isFishing = false;
                reelInSound.Stop();
                animator.SetBool("Fishing", false);
                animator.SetBool("Reeling", false);
                fishingStick.SetActive(false);
                break;
        }
    }

    private void FishingSuccess()
    {
        fishingResultSound.PlayOneShot(succesFishingSound, 0.4f);
        fishPrefab.SetActive(true);
        animator.Play("Victory");
        currentGameMode = GameMode.NotFishing;

        if (OnFishingEvent != null)
        {
            OnFishingEvent("FishingWin");
        }
    }

    private void FishingFailed()
    {
        fishingResultSound.PlayOneShot(failedFishingSound, 0.4f); ;
        animator.Play("Defeat");
        currentGameMode = GameMode.NotFishing;

        if (OnFishingEvent != null)
        {
            OnFishingEvent("FishingLost");
        }
    }

    public void ResetFishingState()
    {
        isFishing = false;

        if (OnFishingEvent != null)
        {
            OnFishingEvent("FishingCancel");
        }
    }

    public void DisableTheFishPrefab()
    {
        fishPrefab.SetActive(false);
    }
}
