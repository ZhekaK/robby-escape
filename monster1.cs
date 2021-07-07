using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster1 : MonoBehaviour
{

    [SerializeField]
    Transform center;
    [SerializeField]
    float radius = 2f, angularSpeed = 2f;
    float positionX, positionY, angle = 0f;

    // Update is called once per frame
    void Update()
    {
        positionX = center.position.x + Mathf.Cos(angle) * radius;
        positionY = center.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(positionX, positionY);
        angle = angle + Time.deltaTime * angularSpeed;

        if(angle >= 360f)
        {
            angle = 0f;
        }
    }
}
