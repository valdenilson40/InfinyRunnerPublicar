using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeinOut : MonoBehaviour
{

    public static fadeinOut _instance;


    private Animator anim;
    public bool isFadeComplete;


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


    }

    public void Fase()
    {
        anim.SetTrigger("fade");
    }
    
    void faceComplete(bool b)
    {
        isFadeComplete = b;
    }

    public void GoScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
