using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManage : MonoBehaviour
{

    public static audioManage _instance;

    public AudioSource Music;
    public AudioSource FX;


    [Header("Music")]

    public AudioClip Titulo;
    public AudioClip introducaoEstado;
    public AudioClip musicaFase;
    public AudioClip gameOver;
    public AudioClip lostLife;
    public AudioClip jogarNovamente;
    public AudioClip powerUp;


    [Header("FX")]
    public AudioClip FXCoin;
    public AudioClip FXJump;

    [Header("Falas")]
    public AudioClip FXItsMa;
   public AudioClip FXGameOver;
   public  AudioClip FXStartGamePlay;
    public AudioClip FXTitulo;





    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;


        DontDestroyOnLoad(this.gameObject);
       
    }

   public void PlayMusic(AudioClip clip, bool loop)
    {
        Music.loop = loop;
        Music.clip = clip;
        Music.Play();

    }

    public void PlayFX(AudioClip clip)
    {
        FX.PlayOneShot(clip);
    }

   public  IEnumerator PlayMusicGamePlayer()
    {
        PlayMusic(introducaoEstado, false);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => !Music.isPlaying);
        PlayMusic(musicaFase, true);

    }
}
