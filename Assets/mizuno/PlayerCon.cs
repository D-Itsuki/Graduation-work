using UnityEngine;
using System.Collections;

public class PlayerCon : MonoBehaviour,IDamageble
{
    GameManager _gamemaneger;
    private float speed = 10F;
    private float Hp = 10; 
    [SerializeField] public float fireSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject barrel;
    Rigidbody2D rb;

    Vector2 vir = Vector2.zero;
    Vector2 hor = Vector2.zero;
    Vector2 vec = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gamemaneger = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            hor = new Vector2(-1,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            vir = new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vir = new Vector2(0, -1);
        }
        vec = vir + hor;
        vec.Normalize();
        rb.velocity = vec;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
        vec = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Bullet = (GameObject)Instantiate(bullet,barrel.transform.position, barrel.transform.rotation);
            
        }
    }

    public void Damage(float damage)
    {
        if (Hp <= 0)
            return;

        Hp -= damage;

        if (Hp < 1)
            Dead();
    }
    public void Dead()
    {
        _gamemaneger.GameOver();
    }

}
