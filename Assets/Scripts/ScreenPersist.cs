using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPersist : MonoBehaviour
{
    void Awake() {
        int numScenePersists = FindObjectsOfType<ScreenPersist>().Length;

        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
