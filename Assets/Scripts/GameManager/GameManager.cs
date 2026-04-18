using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [field : SerializeField] public Vector3 SavedPosition { get; private set; }
    public Vector3 SavedOrientation { get; private set; }
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene(Vector3 targetPosition, Vector3 targetOrientation, int sceneNumber)
    {
        SavedPosition = targetPosition;
        SavedOrientation = targetOrientation;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneNumber);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.transform.position = SavedPosition;
            player.transform.eulerAngles = SavedOrientation;
        }
    }
}
