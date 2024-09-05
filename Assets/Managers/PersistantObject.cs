using UnityEngine;

public class PersistantObject : MonoBehaviour
{
    void Awake()
    {
        PersistantObject[] objs = FindObjectsByType<PersistantObject>(FindObjectsSortMode.None);

        if (objs.Length > 1) {
            print("Destroy");
            Destroy(gameObject);
        } else {
            print("create persistant");
            DontDestroyOnLoad(gameObject);
        }
    }
}