using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decoracaoManager : MonoBehaviour
{
    public GameObject[] decoracoes;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in decoracoes)
        {
            g.SetActive(false);
        }

        decoracoes[Random.Range(0, decoracoes.Length)].SetActive(true);
    }

  
}
