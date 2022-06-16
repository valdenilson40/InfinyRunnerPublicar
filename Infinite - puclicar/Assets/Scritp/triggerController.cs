using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "coletavel":
                coletavel item = other.gameObject.GetComponent<coletavel>();
                GameManager._instance.GetItem(item.type);
                Destroy(other.gameObject);

                break;

            case "morrer":
                GameManager._instance.morrer();

                break;
        }
    }

}
