using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{

    public Text score;
    public Text distance;
    public Text hiScore;
    public Text hiDistance;
    public GameObject panelgameOver;

    private bool jogarNovamente;

    public GameObject textoJogarNovamente;

    private bool isStart;


    // Start is called before the first frame update
    void Start()
    {
        hiScore.text = "HiScore: " + PlayerPrefs.GetInt("hiScore").ToString("N0");
        hiDistance.text = "HiDistance: " + PlayerPrefs.GetInt("hiDistance").ToString("N0") + " m";
        score.text = "Score: " + PlayerPrefs.GetInt("score").ToString("N0");
        distance.text = "Distance: " + PlayerPrefs.GetInt("distance").ToString("N0") + " m";

        StartCoroutine("gameover");

        textoJogarNovamente.SetActive(false);
        panelgameOver.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown &&jogarNovamente == true && isStart == false)
        {
            StartCoroutine("StartGame");
        }
    }

    IEnumerator gameover()
    {

        audioManage._instance.PlayMusic(audioManage._instance.gameOver, false);

        yield return new WaitForSeconds(0.5f);
        fadeinOut._instance.Fase();
        yield return new WaitUntil(() => !audioManage._instance.Music.isPlaying);
        fadeinOut._instance.Fase();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);

        panelgameOver.SetActive(false);


        audioManage._instance.PlayMusic(audioManage._instance.jogarNovamente, false);
        yield return new WaitForSeconds(1f);
        fadeinOut._instance.Fase();
        yield return new WaitForSeconds(2f);
        textoJogarNovamente.SetActive(true);
        jogarNovamente = true;

    }


    IEnumerator StartGame()
    {
        isStart = true;

       
       
        fadeinOut._instance.Fase();
        yield return new WaitForEndOfFrame();
        audioManage._instance.StartCoroutine("PlayMusicGamePlayer");
        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);
       // audioManage._instance.PlayFX(audioManage._instance.FXTitulo);
        fadeinOut._instance.GoScene("GamePlay");


    }
}
