using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("player"))
        {
            Invoke("fp", 1f);
            Destroy(gameObject, 3f);
        }
    }
    void fp()
    {
        rb.isKinematic = false;
    }
}
