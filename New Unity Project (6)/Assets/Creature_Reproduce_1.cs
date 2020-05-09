using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature_Reproduce_1 : MonoBehaviour
{
    [SerializeField] float reproduceTime;
    [SerializeField] int ballCount = 0;

    public GameObject ball;

    //know Game Manager
    public GameHelper manager;

    Rigidbody rigid;

    float elapsedReproduceTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        //ini setting
        reproduceTime = 2.5f;

        //get component
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Reproduce();
    }

    void Reproduce()
    {
        elapsedReproduceTime += Time.deltaTime;
        if (elapsedReproduceTime > reproduceTime)
        {
            GameObject myBall = Instantiate(ball, transform.position, Quaternion.identity);
            rigid = myBall.GetComponent<Rigidbody>();

            //add color
            Color newColor = new Color(Random.value, Random.value, Random.value, Random.value);
            myBall.GetComponent<Renderer>().material.color = newColor;

            //Count ball
            ballCount++;
            //notice created
            manager.noticeCreatedObject();

            elapsedReproduceTime = 0;
        }

    }
}
