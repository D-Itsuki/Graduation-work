using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Enemy1Fist enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            enemy.Damage(1);//テスト用
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("New Scene");
    }
}
