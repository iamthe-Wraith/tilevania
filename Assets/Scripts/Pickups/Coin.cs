using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int pointValue = 100;
    [SerializeField] AudioClip coinPickupSFX;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().UpdateScore(pointValue);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
