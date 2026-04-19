using System;
using UnityEngine;

namespace Others
{
    public class StartRound : MonoBehaviour
    {
        [SerializeField] private RoundManager roundManager;
        [SerializeField] private bool playerInside;
        [SerializeField] private GameObject marca;

        private void Awake()
        {
            marca.SetActive(false);
        }

        private void Start()
        {
            InputManager.Instance.OnInteractionInitiated += StartRounds;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                marca.SetActive(true);
                playerInside = true;
            }
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                marca.SetActive(false);
                playerInside = false;
            }
        }

        private void Update()
        {
            if (!playerInside)
            {
                return;
            }
            
        }
        private void StartRounds()
        {
            roundManager.ConfirmAndStartNextRound();
        }
        private void OnDestroy()
        {
            InputManager.Instance.OnInteractionCanceled -= StartRounds;
        }

    }


}
