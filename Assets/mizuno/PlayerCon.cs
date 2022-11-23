using UnityEngine;
using System.Collections;

public class PlayerCon : MonoBehaviour,IDamageble
{
    GameManager _gamemaneger;
    private float Hp = 10; 
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject barrel;
    Rigidbody2D rb;

    Vector2 ver = Vector2.zero;
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
        vec = ver + hor;
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
        Debug.Log("GAMEOVER");
        _gamemaneger.GameOver();
    }

}
