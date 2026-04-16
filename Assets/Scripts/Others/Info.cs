using System;
using UnityEngine;

public class Info : MonoBehaviour, IPrizable
{
    public static Info Instance { get; private set; }
    [SerializeField] private int currentMoney;
    [SerializeField] private bool hasTower;
    private GameObject tower;

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        tower = FindFirstObjectByType(typeof(Tower)) as GameObject;
    }
    
    public bool HasTower => hasTower;

    public void BuyTower(int cost)
    {
        if (hasTower) return;

        hasTower = true;
    }


    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }
}
