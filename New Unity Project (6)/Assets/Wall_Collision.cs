using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Collision : MonoBehaviour
{
    public GameHelper manager;
    // Start is called before the first frame update


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "creature")
        {
            manager.noticeDestroyedObject();
        }
        else if(collision.gameObject.tag== "food")
        {
            manager.noticedDestroyedFood();
        }

        Destroy(collision.gameObject);

    }
}
