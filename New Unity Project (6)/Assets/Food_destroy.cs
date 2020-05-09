using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_destroy : MonoBehaviour
{

    [SerializeField] float destroyDueTime;
    float totalElapsedTime = 0.0f;


    public GameObject food;

    //know Game Manager
    public GameHelper manager;

    // Start is called before the first frame update
    void Start()
    {
        destroyDueTime = 45f;

    }

    // Update is called once per frame
    void Update()
    {
        totalElapsedTime += Time.deltaTime;
        if (totalElapsedTime >= destroyDueTime)
        {
            //notice Gamehelper
            manager.noticedDestroyedFood();
            Destroy(food);
        }
    }
}
