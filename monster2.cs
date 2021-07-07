using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster2 : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    bool onright = true;
    public GameObject rrr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(onright == true)
        {
            Invoke("left", 2);
            Invoke("fl", 2);
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (onright == false)
        {
            Invoke("right", 2);
            Invoke("tr", 2);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    void tr()
    {
        onright = true;
    }
    void fl()
    {
        onright = false;
    }
    void left()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void right()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "tim")
        {
            rrr.SetActive(false);
        }
    }
}
