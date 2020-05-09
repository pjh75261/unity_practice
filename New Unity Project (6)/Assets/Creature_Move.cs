using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature_Move : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float speedFactor;
    [SerializeField] bool foodDetect;
    [SerializeField] float foodDesireability;
    [SerializeField] float creatureSenseRadius;
    [SerializeField] Vector3 foodLocation;
    
    //know Sphere
    public GameObject ball;


    //know Game Manager
    public GameHelper manager;

    Rigidbody rigid;
    SphereCollider detectCollider;

    Vector3 moveForce;

    float elapsedMoveTime = 0.0f;

    bool first = true;


    // Start is called before the first frame update
    void Start()
    {

        //get component
        rigid = GetComponent<Rigidbody>();
        detectCollider = GetComponent<SphereCollider>();

        //ini setting
        moveTime = manager.getgenmoveTime();
        speedFactor = manager.getgenspeedFactor();
        foodDesireability = manager.getgenfoodDesireability();
        creatureSenseRadius = manager.getgencreatureSenseRadius();
        detectCollider.radius = creatureSenseRadius;

        foodLocation = Vector3.zero;


        //move ini ball
        elapsedMoveTime = moveTime + 1;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        detectCollider.radius = creatureSenseRadius;
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
            if (foodDetect)
            {
                moveForce = (moveForce + (foodLocation - transform.position) * foodDesireability).normalized;
            }
            
            rigid.AddForce(moveForce * speedFactor, ForceMode.Impulse);

            //disable Sensor
            {
                foodLocation = transform.position;
                foodDetect = false;
            }
            elapsedMoveTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            foodDetect = true;
            foodLocation = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            foodDetect = false;
        }
    }
}
