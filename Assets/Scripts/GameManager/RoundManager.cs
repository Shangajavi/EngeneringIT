
using System.Collections;
using Others;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private Spawner spawner1;
    [SerializeField] private Spawner spawner2;
    [SerializeField] private Spawner spawner3;

    [Header("Gate")]
    [SerializeField] private Transform gate;
    [SerializeField] private Vector2 gateClosedPos;
    [SerializeField] private Vector2 gateOpenPos;
    
    
    [Header("Victory")]
    [SerializeField] private GameObject victoryWidget;


    private Info info;
    private Spawner activeSpawner;

    private void Start()
    {
        info = Info.Instance;
        SetupSceneState();
    }

    private void SetupSceneState()
    {
        if (info == null) return;

        if (info.roundCompleted)
        {
            DeactivateAllSpawners();
            OpenGate();
        }
        else
        {
            CloseGate();
            ActivateSpawnerForRound(info.currentRound);
        }
    }

    
    private void ActivateSpawnerForRound(Round round)
    {
        if (activeSpawner != null)
            activeSpawner.OnFinished -= HandleSpawnerFinished;

        DeactivateAllSpawners();

        switch (round)
        {
            case Round.Round1: activeSpawner = spawner1; break;
            case Round.Round2: activeSpawner = spawner2; break;
            case Round.Round3: activeSpawner = spawner3; break;
        }

        if (activeSpawner != null)
        {
            activeSpawner.OnFinished += HandleSpawnerFinished;
            activeSpawner.gameObject.SetActive(true);
        }
    }


    private void HandleSpawnerFinished()
    {
        if (info != null)
            info.roundCompleted = true;

        OpenGate();
        DeactivateAllSpawners();
        
        if (info.currentRound == Round.Round3)
        {
            StartCoroutine(HandleVictory());
        }

    }

    public void ConfirmAndStartNextRound()
    {
        if (info == null) return;

        
        if (!info.roundCompleted) return;

        
        switch (info.currentRound)
        {
            case Round.Round1: info.currentRound = Round.Round2; break;
            case Round.Round2: info.currentRound = Round.Round3; break;
            case Round.Round3:
                return;
        }

        info.roundCompleted = false;
        CloseGate();
        ActivateSpawnerForRound(info.currentRound);
    }

    private void DeactivateAllSpawners()
    {
        if (spawner1 != null) spawner1.gameObject.SetActive(false);
        if (spawner2 != null) spawner2.gameObject.SetActive(false);
        if (spawner3 != null) spawner3.gameObject.SetActive(false);
    }

    private void CloseGate()
    {
        if (gate != null) gate.position = gateClosedPos;
    }

    
    private IEnumerator HandleVictory()
    {
        if (victoryWidget != null)
            victoryWidget.SetActive(true);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    private void OpenGate()
    {
        if (gate != null) gate.position = gateOpenPos;
    }
}

