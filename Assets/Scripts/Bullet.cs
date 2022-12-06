using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D bulletRigidBody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        bulletRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D() {
        Destroy(gameObject);
    }
}
