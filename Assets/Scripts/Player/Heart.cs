using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour, IDamageable
{
    [SerializeField] public float health;
    [SerializeField] public GameObject loseWidget;

    private void Awake()
    {
        loseWidget.SetActive(false);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        

        if (health <= 0)
        {
            StartCoroutine(HandleLost());

        }
    }
    private IEnumerator HandleLost()
    {
        if (loseWidget != null)
            loseWidget.SetActive(true);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

}
