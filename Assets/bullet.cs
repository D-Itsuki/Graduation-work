using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int  bulletDamege;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colldmg = collision.gameObject.GetComponent<IDamageble>();
        colldmg.Damage(bulletDamege);
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 10f;
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
