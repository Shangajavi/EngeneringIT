
using Others;
using UnityEngine;

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

    private Info info;
    private Spawner activeSpawner;

    private void Start()
    {
        info = Info.Instance;

        // Si la ronda NO está completada, activamos su spawner.
        // Si está completada, solo abrimos la gate y esperamos confirmación.
        SetupSceneState();
    }

    private void SetupSceneState()
    {
        if (info == null) return;

        if (info.roundCompleted)
        {
            // Ronda ya terminada -> puerta abierta y NO spawnear
            DeactivateAllSpawners();
            OpenGate();
        }
        else
        {
            // Ronda en curso -> puerta cerrada y spawner activo
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
        // ✅ Aquí NO avanzamos ronda: solo abrimos la gate y marcamos completada
        if (info != null)
            info.roundCompleted = true;

        OpenGate();
        DeactivateAllSpawners();
    }

    public void ConfirmAndStartNextRound()
    {
        if (info == null) return;

        // Solo si la ronda estaba completada
        if (!info.roundCompleted) return;

        // Avanzar ronda
        switch (info.currentRound)
        {
            case Round.Round1: info.currentRound = Round.Round2; break;
            case Round.Round2: info.currentRound = Round.Round3; break;
            case Round.Round3:
                // Fin del juego: aquí podrías hacer victoria
                return;
        }

        info.roundCompleted = false;

        // Iniciar nueva ronda en esta escena
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

    private void OpenGate()
    {
        if (gate != null) gate.position = gateOpenPos;
    }
}

