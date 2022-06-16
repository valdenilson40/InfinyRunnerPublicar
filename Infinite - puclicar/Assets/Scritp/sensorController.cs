using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorController : MonoBehaviour
{

    public Transform parent;
    public bool ativado;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ativado == true)
        {
            if(Vector3.Distance(transform.position, GameManager._instance.player.position)> GameManager._instance.tamanhoFase){
                Destroy(parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ativado == false)
        {
            ativado = true;

            GameManager._instance.NovosBlocos();
        }
    }
}
