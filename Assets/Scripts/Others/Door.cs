using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetOrientation;
    [SerializeField] private int targetSceneIndex;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LoadNewScene(targetPosition, targetOrientation, targetSceneIndex);
        }
    }
    
}
