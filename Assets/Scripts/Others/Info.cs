using System;
using Others;
using UnityEngine;

public class Info : MonoBehaviour, IPrizable
{
    public Round currentRound = Round.Round1;
    public static Info Instance { get; private set; }
    [SerializeField] private int currentMoney;
    [SerializeField] private bool hasTower;
    private GameObject tower;
    public bool roundCompleted = false;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        tower = FindFirstObjectByType(typeof(Tower)) as GameObject;
        
    }
    
    public bool HasTower => hasTower;

    public void BuildTower(int cost)
    {
        if (hasTower) return;

        hasTower = true;
    }


    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }
}
