using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class Creature_Reproduce : MonoBehaviour
{
    //몇대인지
    [SerializeField] float creatureGeneration;

    //ini setting
    [SerializeField] float reproducePeriode;
    [SerializeField] float maxReproduceTimes;
    [SerializeField] int reproduceCount = 0;
    [SerializeField] bool isReproducible;
    [SerializeField] float reproducePower;
    [SerializeField] float reproduceMultiplyFactor;
    [SerializeField] float reproduceAddFactor;
    [SerializeField] float hunger;
    [SerializeField] float iniHunger;
    [SerializeField] float maxHunger;
    [SerializeField] float maxLifeTime;
    [SerializeField] float maxObjectNum;
    [SerializeField] float creatureSenseRadius;



    //Move에서 쓰는것이나, 정보 가지고 있다가 수정해서 송부 필요
    [SerializeField] float moveTime;
    [SerializeField] float speedFactor;
    [SerializeField] float foodDesireability;

    //생명시간
    [SerializeField] float elapsedReproduceTime = 0.0f;
    [SerializeField] float totalElapsedTime = 0.0f;

    //생존보고 여부
    [SerializeField] bool isLive = true;

    public GameObject ball;

    //know Game Manager
    public GameHelper manager;

    Rigidbody rigid;

    bool first = true;




    // Start is called before the first frame update
    void Start()
    {
        creatureGeneration = 0;


        //initial setting
        reproducePeriode = manager.getgenreproducePeriode(); ;
        maxReproduceTimes = manager.getgenmaxReproduceTimes(); ;
        reproduceMultiplyFactor = manager.getgenreproduceMultiplyFactor(); ;
        reproduceAddFactor = manager.getgenreproduceAddFactor(); ;
        iniHunger = manager.getgeniniHunger(); ;
        maxHunger = manager.getgenmaxHunger(); ;
        maxLifeTime = manager.getgenmaxLifeTime(); ;
        maxObjectNum = manager.getgenmaxObjectNum(); ;
        moveTime = manager.getgenmoveTime(); ;
        speedFactor = manager.getgenspeedFactor(); ;
        foodDesireability = manager.getgenfoodDesireability(); ;
        creatureSenseRadius = manager.getgencreatureSenseRadius();
        creatureGeneration = manager.getgencreatureGeneration();



        //local setting
        isReproducible = false;
        reproducePower = 0;
        hunger = maxHunger + iniHunger;
        totalElapsedTime = 0;
        reproduceCount = 0;

        //get component
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Live();

        //Destory if Old
        if (reproduceCount >= maxReproduceTimes || hunger <= 0 || totalElapsedTime > maxLifeTime)
        {
            Die();
        }

    }

    void Live()
    {
        //total
        totalElapsedTime += Time.deltaTime;

        //essential used now
        elapsedReproduceTime += Time.deltaTime;

        //reproducePeriode 주기마다 실행
        if (elapsedReproduceTime > reproducePeriode)
        {
            hunger--;            
            reproducePower = reproducePower - 0.1f;

            if (reproducePower >= 1 && !isReproducible)
            {
                reproducePower--;
                isReproducible = true;
            }

            if (manager.getLeftObject() < maxObjectNum)
            {
                hunger--;
                if(isReproducible)
                    Reproduce();
            }

            elapsedReproduceTime = 0;
        }

        //생존 보고 10초마다
        if (Mathf.RoundToInt(totalElapsedTime) % 10 == 0 && !isLive)
        {
            manager.countCreature(); 
            isLive = true;
        }

        if (Mathf.RoundToInt(totalElapsedTime) % 10 == 9)
        {
            isLive = false;
        }


    }

    void Reproduce()
    {
        //reproduce unable
        isReproducible = false;

        //input variable
        manager.setgenreproducePeriode(reproducePeriode);
        manager.setgenmaxReproduceTimes(maxReproduceTimes);
        manager.setgenreproduceMultiplyFactor(reproduceMultiplyFactor);
        manager.setgenreproduceAddFactor(reproduceAddFactor);
        manager.setgeniniHunger(iniHunger);
        manager.setgenmaxHunger(maxHunger);
        manager.setgenmaxLifeTime(maxLifeTime);
        manager.setgenmaxObjectNum(maxObjectNum);
        manager.setgenmoveTime(moveTime);
        manager.setgenspeedFactor(speedFactor);
        manager.setgenfoodDesireability(foodDesireability);
        manager.setgencreatureSenseRadius(creatureSenseRadius);
        manager.setgencreatureGeneration(creatureGeneration);


        //set variable
        manager.setValue();

        //reproduce
        GameObject myBall = Instantiate(ball, transform.position, Quaternion.identity);
        rigid = myBall.GetComponent<Rigidbody>();
        myBall.gameObject.name = "Ball" + manager.getgencreatureGeneration();

        //add color
        Color newColor = new Color(Random.value, Random.value, Random.value, Random.value);
        myBall.GetComponent<Renderer>().material.color = newColor;

        //Count reproduce
        reproduceCount++;

        //Gamemanager notice created
        manager.noticeCreatedObject();

    }

    void Die()
    {
        manager.noticeDestroyedObject();
        Destroy(ball);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            reproducePower = (reproducePower + 1) * reproduceMultiplyFactor + reproduceAddFactor;
            hunger += 10;
            if(hunger > maxHunger)
            {
                hunger = maxHunger;
            }

            manager.noticedDestroyedFood();
            Destroy(collision.gameObject);
        }
    }


}
