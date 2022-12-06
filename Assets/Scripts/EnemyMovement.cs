using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemyMoveSpeed = 1f;

    Rigidbody2D enemyRigidBody;

    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2(enemyMoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemyMoveSpeed *= -1;

        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
}
