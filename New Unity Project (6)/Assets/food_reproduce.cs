using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class food_reproduce : MonoBehaviour
{
    [SerializeField] float iniReproducePeriode;
    [SerializeField] float reproducePeriode;
    [SerializeField] int maxFoodNum;


    [SerializeField] float destroyDueTime;
    float totalElapsedTime = 0.0f;

    public GameObject food;

    //know Game Manager
    public GameHelper manager;

    Rigidbody rigid;
    Vector3 position;

    float elapsedReproduceTime = 0.0f;
    bool isFoodNear = false;

    bool initialBoost;


    // Start is called before the first frame update
    void Start()
    {
        //ini setting
        maxFoodNum = 500;
        iniReproducePeriode = 10f;
        reproducePeriode = iniReproducePeriode;
        destroyDueTime = 30f;
        initialBoost = true;

        //get component
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initialBoost)
        {
            elapsedReproduceTime = reproducePeriode + 1;
            isFoodNear = false;
            if (manager.getLeftFood() > 400)
            {
                initialBoost = false;
            }
        }


        if(manager.getLeftFood() < maxFoodNum)
        {
            Reproduce();
        }

        totalElapsedTime += Time.deltaTime;
        if (totalElapsedTime >= destroyDueTime)
        {

            elapsedReproduceTime = reproducePeriode + 1;
            isFoodNear = false;
            Reproduce();


            //notice Gamehelper
            manager.noticedDestroyedFood();
            Destroy(food);
        }

    }

    void getRandomPosition()
    {
        position = new Vector3(getRandomValue(-800, 800), 35, getRandomValue(-800, 800));
    }

    public float getRandomValue(float a, float b) => UnityEngine.Random.Range(a, b);

    public float getRandomValue() => UnityEngine.Random.value;
    void Reproduce()
    {
        elapsedReproduceTime += Time.deltaTime;

        if (elapsedReproduceTime >= reproducePeriode)
        {
            getPosition();
            GameObject foodPrefab = Instantiate(food, position, Quaternion.identity);
            rigid = foodPrefab.GetComponent<Rigidbody>();
            foodPrefab.name = "food" + Mathf.RoundToInt(UnityEngine.Random.value * 10);

            //add color
            Color newColor = new Color(getRandomValue(), getRandomValue(), getRandomValue(), getRandomValue());
            foodPrefab.GetComponent<Renderer>().material.color = newColor;

            //notice Gamehelper
            manager.noticeCreatedFood();

            elapsedReproduceTime = 0;
        }
    }

    void getPosition()
    {
        if (isFoodNear)
        {
            position = transform.position + new Vector3(getRandomValue(-5, 5), 0.8f, getRandomValue(-5, 5));
        }
        else
        {
            getRandomPosition();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            reproducePeriode = iniReproducePeriode/2.5f;
            isFoodNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            reproducePeriode = iniReproducePeriode;
            isFoodNear = false;
        }
    }

}

