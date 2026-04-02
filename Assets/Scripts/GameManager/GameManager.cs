using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [field : SerializeField] public Vector3 SavedPosition { get; private set; }
    public Vector3 SavedOrientation { get; private set; }
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNewScene(Vector3 targetPosition, Vector3 targetOrientation, int targetSceneIndex)
    {
        SavedPosition = targetPosition;
        SavedOrientation = targetOrientation;
        SceneManager.LoadScene(targetSceneIndex);
    }
}
