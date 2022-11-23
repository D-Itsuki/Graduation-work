using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Enemy1Fist enemy;
    LoadSceneManager loadSceneManager;

    private void Start()
    {
        loadSceneManager = GetComponent<LoadSceneManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //enemy.Damage(1);//テスト用
        }
    }

    public void GameOver()
    {
        loadSceneManager.Fade("New Scene");
    }
}
