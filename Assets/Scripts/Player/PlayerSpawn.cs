using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    private void Start()
    {
        if (GameManager.instance != null)
        {
            transform.position = GameManager.instance.SavedPosition;
            transform.eulerAngles = GameManager.instance.SavedOrientation;
        }
    }

}
