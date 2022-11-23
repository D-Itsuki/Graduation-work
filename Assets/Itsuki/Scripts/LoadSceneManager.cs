using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] Image fadePanel;//‰æ–Ê‚ðˆÃ“]‚³‚¹‚éˆ×‚Ìƒpƒlƒ‹
    float elapsedTime = 0;
    float rate;

    public static LoadSceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Fade(string sceneName)
    {
        DontDestroyOnLoad(gameObject);
        fadePanel.enabled = true;
        StartCoroutine(FadeLoadScene(2, sceneName));
    }

    IEnumerator FadeLoadScene(float sec, string sceneName)
    {
        elapsedTime = 0;
        while (true)
        {
            elapsedTime += Time.deltaTime;  //Œo‰ßŽžŠÔ‚Ì‰ÁŽZ
            rate = Mathf.Clamp01(elapsedTime / sec + 0.01f);
            fadePanel.color = new Color(0, 0, 0, rate);
            if (elapsedTime > sec)
            {
                break;
            }
            yield return null;
        }
        LoadScene(sceneName);
    }

    public void LoadScene(string scneName)
    {
        SceneManager.LoadScene(scneName);
    }

    //public void TitleAnimationPlay()
    //{
    //    if (director != null)
    //    {
    //        director.Play();
    //    }
    //}

    public void Quit()
    {
        Application.Quit();
    }
}
