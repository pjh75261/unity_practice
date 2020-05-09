using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float reproduceTime;
    [SerializeField] float powerFactor;
    [SerializeField] int ballCount = 0;

    public GameObject ball;

    //know Game Manager
    public GameHelper manager;

    

    Rigidbody rigid;
    Vector3 moveForce;

    float elapsedMoveTime = 0.0f;
    float elapsedReproduceTime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //ini setting
        reproduceTime = 1.5f;
        moveTime = 0.81f;
        powerFactor = 50.0f;

        //get component
        rigid = GetComponent<Rigidbody>();
        manager.noticeCreatedObject();

        //move ini ball
        elapsedMoveTime = moveTime + 1;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Reproduce();
    }

    void getRandomForce()
    {
        moveForce = Random.insideUnitSphere.normalized;
        moveForce.y = 0;
    }

    void Move()
    {
        elapsedMoveTime += Time.deltaTime;
        if (elapsedMoveTime > moveTime)
        {
            getRandomForce();
            rigid.AddForce(moveForce * powerFactor, ForceMode.Impulse);
            elapsedMoveTime = 0;
        }
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

            //moveObject
            Move();

            //Count ball
            ballCount++;
            //notice created
            manager.noticeCreatedObject();

            elapsedReproduceTime = 0;
        }

    }
}
