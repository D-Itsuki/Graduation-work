using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAA : MonoBehaviour
{
    [SerializeField] float meleeDamege;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        var colldmg = collision.gameObject.GetComponent<IDamageble>();
        colldmg.Damage(meleeDamege);
        //gameObject.SetActive(false);
    }

}