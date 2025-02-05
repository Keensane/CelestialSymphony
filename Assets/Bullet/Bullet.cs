using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    public AudioSource bulletSound;

    GameObject Target;
    Vector3 Look;
    bool isHit = false;

    private void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.velocity = new Vector2(0f, speed);
    }

    private void Update()
    {
        if (isHit)
        {
            transform.up = transform.position - Look;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Look = other.transform.position;
        isHit = true;
    }
}
