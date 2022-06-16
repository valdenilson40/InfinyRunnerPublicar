using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletavel : MonoBehaviour
{
    public itemType type;

    public bool isFolow; // ir atrás do personagem

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "powerUp" && isFolow == false)
        {
            isFolow = true;

        }
    }

    private void Update()
    {
       if(type == itemType.STAIR && GameManager._instance.isPoweUp == true)
        {
            Destroy(this.gameObject);
        }
        if(isFolow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager._instance.posicaoColetavel.position, (GameManager._instance.movimentSpeed +2)* Time.deltaTime);
        }
    }


}
