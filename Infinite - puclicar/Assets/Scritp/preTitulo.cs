using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preTitulo : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("introGame");
    }

   IEnumerator introGame()
    {
        audioManage._instance.PlayMusic(audioManage._instance.Titulo, true); //busca a música, informa se é em loop ou não

        

        yield return new WaitForSeconds(1.5f);
        fadeinOut._instance.Fase();
        yield return new WaitForSeconds(3.0f);
        fadeinOut._instance.Fase();
        yield return new WaitForEndOfFrame();
        //audioManage._instance.PlayFX(audioManage._instance.FXItsMa);
        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);
        fadeinOut._instance.GoScene("titulo.");

    }
}
