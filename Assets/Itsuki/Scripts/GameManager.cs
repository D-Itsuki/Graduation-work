using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Enemy1Controller enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            enemy.Damage(1);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("New Scene");
    }
}
