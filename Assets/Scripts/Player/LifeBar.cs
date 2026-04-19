
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Heart nucleusHeart;

    private float maxHealth = 1f;

    private void Awake()
    {
        if (nucleusHeart == null)
        {
            nucleusHeart = GameObject.Find("Nucleo")?.GetComponent<Heart>();
        }

        if (nucleusHeart != null)
        {
            maxHealth = Mathf.Max(1f, nucleusHeart.health);
        }

    }

    private void Update()
    {
        if (nucleusHeart == null) 
        {
            lifeBar.fillAmount = 0f;
            return;
        }

        float normalized = nucleusHeart.health / maxHealth;
        lifeBar.fillAmount = Mathf.Clamp01(normalized);
    }
}

