using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] coins;
    public GameObject stair;

    // Start is called before the first frame update
    void Start()
    {
       
        if(stair != null)
        {
            stair.SetActive(false);
        }


        foreach (GameObject g in coins)
        {
            g.SetActive(false);
        }

        coins[Random.Range(0, coins.Length)].SetActive(true);

        if(stair != null)
        {
            if (Random.Range(0, 100) < GameManager._instance.percPowerUp)
            {
                stair.SetActive(true);
            }
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
