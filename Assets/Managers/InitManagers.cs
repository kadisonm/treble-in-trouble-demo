using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManagers : MonoBehaviour
{
    public void OnStart()
    {
        Debug.Log(GameManager.Instance.SomeMethod());
    }
}
