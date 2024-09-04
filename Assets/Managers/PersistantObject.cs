using UnityEngine;

public class PersistantObject : MonoBehaviour
{
    void Awake()
    {
        PersistantObject[] objs = FindObjectsByType<PersistantObject>(FindObjectsSortMode.None);

        if (objs.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}