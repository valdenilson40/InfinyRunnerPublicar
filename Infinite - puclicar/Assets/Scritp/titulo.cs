using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titulo : MonoBehaviour
{
    public Animator anim;
    private bool isStart;

    public Text hiScore;
    public Text hiDistance;
    public Touch toque;
    
    // Start is called before the first frame update
    void Start()
    {
        hiScore.text = "Hi-Score " + PlayerPrefs.GetInt("hiScore").ToString("N0");
        hiDistance.text = "Hi-Distance " + PlayerPrefs.GetInt("hiDistance").ToString("N0") + " m";

        if (fadeinOut._instance != null)
        {
            fadeinOut._instance.Fase();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            toque = Input.GetTouch(0);

           if(toque.phase == TouchPhase.Began && isStart == false)
            {
                StartCoroutine("StartGame");
            }
  

        }
       /* 
        if (Input.anyKeyDown && isStart == false)
        {
            StartCoroutine("StartGame");
        }
       */
    }

    IEnumerator StartGame()
    {
        isStart = true;

        anim.SetTrigger("StartGame");
        yield return new WaitForSeconds(1f);
       // audioManage._instance.PlayFX(audioManage._instance.FXStartGamePlay);

        yield return new WaitForSeconds(1f);
        fadeinOut._instance.Fase();

        yield return new WaitForEndOfFrame();
        audioManage._instance.StartCoroutine("PlayMusicGamePlayer");

        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);

     
        audioManage._instance.PlayFX(audioManage._instance.FXTitulo);

        fadeinOut._instance.GoScene("GamePlay");


    }
}
