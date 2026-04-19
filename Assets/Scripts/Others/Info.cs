using System;
using Others;
using UnityEngine;

public class Info : MonoBehaviour, IPrizable
{
    public Round currentRound = Round.Round1;
    public static Info Instance { get; private set; }
    [SerializeField] private int currentMoney;
    [SerializeField] private bool hasTower;
    [SerializeField] private bool hasSpikes = false;
    [SerializeField]private BoxCollider2D roundSartedTrigger;
    [SerializeField]private GameObject roundManager;
    [SerializeField]private GameObject spikes;
    [SerializeField] private GameObject victoryWidget;
    private GameObject tower;
    private GameObject nextRound;
    [SerializeField]private bool hasRoundsStarted = false;
    public bool roundCompleted = false;
    [SerializeField]private GameObject mark;

    private void Awake()
    {
        victoryWidget.SetActive(false);
        mark.SetActive(false);
        roundSartedTrigger = GetComponent<BoxCollider2D>();
        

        
        
        DontDestroyOnLoad(gameObject);
        tower = FindFirstObjectByType<Tower>()?.gameObject;
        spikes = FindFirstObjectByType<Spikes>()?.gameObject;
        nextRound = FindFirstObjectByType<StartRound>()?.gameObject;
        roundManager = FindObjectOfType<RoundManager>()?.gameObject;
        roundManager.gameObject.SetActive(false);
        nextRound.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        InputManager.Instance.OnInteractionInitiated += NextRound;
        if (spikes != null)
        {
            spikes.SetActive(hasSpikes);
        }

        if (tower != null)
        {
            tower.SetActive(hasTower);
        }
    }
    

    private bool playerInside;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mark.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mark.SetActive(false);
            playerInside = false;
        }
    }


    public bool HasTower => hasTower;

    public void BuildSpikes()
    {
        
        if (hasSpikes) return;
        hasSpikes = true;
        if (spikes != null)
        {
            spikes.SetActive(true);
        }


    }
    
    public void BuildTower()
    {
        if (hasTower) return;
        hasTower = true;
        if (tower != null)
        {
            tower.SetActive(true);
        }
    }


    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }

    private void NextRound()
    {
        if(playerInside == true)
        {
            roundManager.gameObject.SetActive(true);
            roundSartedTrigger.enabled = false;
            nextRound.gameObject.SetActive(true);
        }
    }
    
    
}
