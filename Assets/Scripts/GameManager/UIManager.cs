using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image healthBar;
    public void UpdateHealthBar(float current, float max)
    {

        
        healthBar.fillAmount = current / max;
        healthBar.color = Color.Lerp(Color.red,  Color.green, current / max);
    }
    
    
}
