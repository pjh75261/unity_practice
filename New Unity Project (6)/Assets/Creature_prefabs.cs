using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature_prefabs : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float powerFactor;

    //know Sphere
    public GameObject ball;

    Rigidbody rigid;
    Vector3 moveForce;

    float elapsedMoveTime = 0.0f;
    


    // Start is called before the first frame update
    void Start()
    {
        //ini setting
        
        moveTime = 1.0f;
        powerFactor = 50.0f;

        //get component
        rigid = GetComponent<Rigidbody>();

        //move ini ball
        elapsedMoveTime = moveTime + 1;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
 
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


}
