using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (levelIndex + 1 <= SceneManager.sceneCountInBuildSettings)
        {
            yield return new WaitForSecondsRealtime(1);

            FindObjectOfType<ScreenPersist>().Reset();
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            FindObjectOfType<ScreenPersist>().Reset();
            Debug.Log("You Win!");
        }
    }
}
