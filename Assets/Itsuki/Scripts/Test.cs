using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Vector2 ver;
    Vector2 hor;
    Vector2 vec;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ver = Vector2.zero;
        hor = Vector2.zero;

        hor = new Vector2(Input.GetAxis("Horizontal"), 0);
        hor.Normalize();
        Debug.Log("Hor" + hor);

        ver = new Vector2(0, Input.GetAxis("Vertical"));
        ver.Normalize();
        Debug.Log("ver" + ver);

        vec = hor + ver;
        vec.Normalize();

        if (hor != Vector2.zero || ver != Vector2.zero)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
        }
        rb.velocity = vec;
    }
}
