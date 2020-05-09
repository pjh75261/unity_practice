using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameHelper : MonoBehaviour
{
    // 개수 *초기
    [SerializeField] int totalDestroyedObject;
    [SerializeField] int totalCreatedObject;
    [SerializeField] int totalObjectLeft;
    [SerializeField] int totalCreatedFood;
    [SerializeField] int totalDestroyedFood;
    [SerializeField] int totalFoodLeft;

    [SerializeField] int creatureNum = 0;
    [SerializeField] int totalObjectLeft1;


/*
    [SerializeField] float[] currentBallStats;
    [SerializeField] float[] IniBallStats;
    [SerializeField] float[] minBallStats;
    [SerializeField] float[] maxBallStats;
    [SerializeField] float[] weightBallStats;
*/



    //주요 11개 변수
    /*    [SerializeField] float reproducePeriode;
        [SerializeField] float reproduceMultiplyFactor;
        [SerializeField] float reproduceAddFactor;
        [SerializeField] float maxHunger;
        [SerializeField] float iniHunger;
        [SerializeField] float maxLifeTime;
        [SerializeField] float moveTime;
        [SerializeField] float speedFactor;
        [SerializeField] float foodDesireability;
        [SerializeField] int maxReproduceTimes;
        [SerializeField] int maxObjectNum;*/

    /*
        [SerializeField] float reproducePeriode = 5.5f;
        [SerializeField] int maxReproduceTimes = 8;
        [SerializeField] float reproduceMultiplyFactor = 0.8f;
        [SerializeField] float reproduceAddFactor = 0.5f;
        [SerializeField] float iniHunger = 20f;
        [SerializeField] float maxHunger = 50f;
        [SerializeField] float maxLifeTime = 600f;
        [SerializeField] int maxObjectNum = 300;
        [SerializeField] float moveTime = 1.0f;
        [SerializeField] float speedFactor = 100f;
        [SerializeField] float foodDesireability = 1f;*/

    [SerializeField] float genreproducePeriode = 5.5f;
    [SerializeField] float genmaxReproduceTimes = 8;
    [SerializeField] float genreproduceMultiplyFactor = 0.8f;
    [SerializeField] float genreproduceAddFactor = 0.5f;
    [SerializeField] float geniniHunger = 20f;
    [SerializeField] float genmaxHunger = 50f;
    [SerializeField] float genmaxLifeTime = 120f;
    [SerializeField] float genmaxObjectNum = 300;
    [SerializeField] float genmoveTime = 1.0f;
    [SerializeField] float genspeedFactor = 100f;
    [SerializeField] float genfoodDesireability = 1f;
    [SerializeField] float gencreatureSenseRadius = 10f;

    //몇번째 자손인지 (12)
    [SerializeField] float gencreatureGeneration = 1;


    //weight fluctuation
    [SerializeField] float totalWeight = 550;
    float weightFluctuation = 0.02f;




    //가중치
    [SerializeField] float reproducePeriodeWeightWeight = 60;
    [SerializeField] float maxReproduceTimesWeightWeight = 10;
    [SerializeField] float reproduceMultiplyFactorWeightWeight = 30;
    [SerializeField] float reproduceAddFactorWeightWeight = 40;
    [SerializeField] float iniHungerWeightWeight = 20;
    [SerializeField] float maxHungerWeightWeight = 45;
    [SerializeField] float maxLifeTimeWeightWeight = 40;
    [SerializeField] float maxObjectNumWeightWeight = 20;
    [SerializeField] float moveTimeWeightWeight = 15;
    [SerializeField] float speedFactorWeightWeight = 70;
    [SerializeField] float foodDesireabilityWeightWeight = 50;
    [SerializeField] float creatureSenseRadiusWeightWeight = 900;



    //최초
    [SerializeField] float reproducePeriodeIni = 5.5f;
    [SerializeField] float maxReproduceTimesIni = 8;
    [SerializeField] float reproduceMultiplyFactorIni = 0.8f;
    [SerializeField] float reproduceAddFactorIni = 0.5f;
    [SerializeField] float iniHungerIni = 20f;
    [SerializeField] float maxHungerIni = 50f;
    [SerializeField] float maxLifeTimeIni = 120f;
    [SerializeField] float maxObjectNumIni = 300;
    [SerializeField] float moveTimeIni = 1.0f;
    [SerializeField] float speedFactorIni = 100f;
    [SerializeField] float foodDesireabilityIni = 1f;
    [SerializeField] float creatureSenseRadiusIni = 10f;


    //MAX
    [SerializeField] float reproducePeriodeMax = 5.5f;
    [SerializeField] float maxReproduceTimesMax = 20f;
    [SerializeField] float reproduceMultiplyFactorMax = 0.9f;
    [SerializeField] float reproduceAddFactorMax = 0.9f;
    [SerializeField] float iniHungerMax = 70f;
    [SerializeField] float maxHungerMax = 100f;
    [SerializeField] float maxLifeTimeMax = 300f;
    [SerializeField] float maxObjectNumMax = 400;
    [SerializeField] float moveTimeMax = 50f;
    [SerializeField] float speedFactorMax = 400f;
    [SerializeField] float foodDesireabilityMax = 30f;
    [SerializeField] float creatureSenseRadiusMax = 100f;

    float totalWeightMax = 800;

    //Min
    [SerializeField] float reproducePeriodeMin = 0f;
    [SerializeField] float maxReproduceTimesMin = 1;
    [SerializeField] float reproduceMultiplyFactorMin = 0;
    [SerializeField] float reproduceAddFactorMin = 0;
    [SerializeField] float iniHungerMin = 0;
    [SerializeField] float maxHungerMin = 1;
    [SerializeField] float maxLifeTimeMin = 1;
    [SerializeField] float maxObjectNumMin = 1;
    [SerializeField] float moveTimeMin = 0.1f;
    [SerializeField] float speedFactorMin = 0.1f;
    [SerializeField] float foodDesireabilityMin = 0f;
    [SerializeField] float creatureSenseRadiusMin = 0f;

    float elapsedTimeGamehelper = 0f;


    float totalWeightMin = 400;

    //normalize
    float temp = 0;

    //fluctuation
    float fluctuation = 0.05f;



    // Start is called before the first frame update
    void Start()
    {
        totalDestroyedObject = 0;
        totalCreatedObject = 0;
        totalObjectLeft = 1;

        totalCreatedFood = 1;
        totalDestroyedFood = 0;
        totalFoodLeft = 0;

        //ini setting
        /*        reproducePeriode = 5.5f;
                maxReproduceTimes = 8;
                reproduceMultiplyFactor = 0.8f;
                reproduceAddFactor = 0.5f;
                iniHunger = 20f;
                maxHunger = 50f;
                maxLifeTime = 600f;
                maxObjectNum = 300;
                moveTime = 1.0f;
                speedFactor = 100f;
                foodDesireability = 1f;*/

        genreproducePeriode = 5.5f;
        genmaxReproduceTimes = 8;
        genreproduceMultiplyFactor = 0.8f;
        genreproduceAddFactor = 0.5f;
        geniniHunger = 20f;
        genmaxHunger = 50f;
        genmaxLifeTime = 60f;
        genmaxObjectNum = 300;
        genmoveTime = 1.0f;
        genspeedFactor = 100f;
        genfoodDesireability = 1f;
        gencreatureSenseRadius = 10f;
        gencreatureGeneration = 1;



    //최초
        reproducePeriodeIni = 5.5f;
        maxReproduceTimesIni = 8;
      reproduceMultiplyFactorIni = 0.8f;
      reproduceAddFactorIni = 0.5f;
      iniHungerIni = 20f;
      maxHungerIni = 50f;
      maxLifeTimeIni = 120f;
      maxObjectNumIni = 300;
      moveTimeIni = 1.0f;
      speedFactorIni = 100f;
      foodDesireabilityIni = 1f;
      creatureSenseRadiusIni = 10f;


    //MAX
      reproducePeriodeMax = 10f;
      maxReproduceTimesMax = 20f;
      reproduceMultiplyFactorMax = 0.9f;
      reproduceAddFactorMax = 0.9f;
      iniHungerMax = 70f;
      maxHungerMax = 100f;
      maxLifeTimeMax = 300f;
      maxObjectNumMax = 400;
      moveTimeMax = 50f;
      speedFactorMax = 400f;
     foodDesireabilityMax = 30f;
      creatureSenseRadiusMax = 100f;

     totalWeightMax = 800;

    //Min
      reproducePeriodeMin = 0f;
      maxReproduceTimesMin = 1;
      reproduceMultiplyFactorMin = 0;
      reproduceAddFactorMin = 0;
      iniHungerMin = 0;
      maxHungerMin = 1;
      maxLifeTimeMin = 1;
      maxObjectNumMin = 1;
      moveTimeMin = 0.1f;
      speedFactorMin = 0.1f;
      foodDesireabilityMin = 0f;
      creatureSenseRadiusMin = 0f;

     elapsedTimeGamehelper = 0f;


     totalWeightMin = 400;

}

    public void Update()
    {
        elapsedTimeGamehelper += Time.deltaTime;
        if(elapsedTimeGamehelper > 10)
        {
            totalObjectLeft1 = creatureNum;
            totalObjectLeft = (totalObjectLeft * 9 + totalObjectLeft1) / 10;
            creatureNum = 0;
            elapsedTimeGamehelper = 0;


        }
    }



    public void noticeDestroyedObject()
    {
        totalDestroyedObject++;
        totalObjectLeft--;
        if (totalDestroyedObject > 100000)
            totalDestroyedObject = 0;
    }

    public void noticeCreatedObject()
    {
        totalCreatedObject++;
        totalObjectLeft++;
        if (totalCreatedObject > 100000)
            totalCreatedObject = 0;
    }

    public void noticeCreatedFood()
    {
        totalCreatedFood++;
        totalFoodLeft++;
        if (totalCreatedFood > 100000)
            totalCreatedFood = 0;
    }
    public void noticedDestroyedFood()
    {
        totalDestroyedFood++;
        totalFoodLeft--;
        if (totalDestroyedFood > 100000)
            totalDestroyedFood = 0;
    }


    public void countCreature()
    {
        creatureNum++;
    }


    public int getLeftFood()
    {
        return totalFoodLeft;
    }

    public int getLeftObject()
    {
        return totalObjectLeft;
    }



    public void setValue()
    {
        fluctuate();
        MinMax();
        Normalize();
    }

    public void fluctuate() {
        genreproducePeriode = genreproducePeriode * generateRandom();
        //genmaxReproduceTimes = Mathf.RoundToInt(genmaxReproduceTimes * (1 + Random.Range(-fluctuation, fluctuation)));
        genmaxReproduceTimes = genmaxReproduceTimes * generateRandom();
        genreproduceMultiplyFactor = genreproduceMultiplyFactor * generateRandom();
        genreproduceAddFactor = genreproduceAddFactor * generateRandom();
        geniniHunger = geniniHunger * generateRandom();
        genmaxHunger = genmaxHunger * generateRandom();
        genmaxLifeTime = genmaxLifeTime * generateRandom();
        //genmaxObjectNum = Mathf.RoundToInt(genmaxObjectNum * (1 + Random.Range(-fluctuation, fluctuation)));
        genmaxObjectNum = genmaxObjectNum * generateRandom();
        genmoveTime = genmoveTime * generateRandom();
        genspeedFactor = genspeedFactor * generateRandom();
        genfoodDesireability = genfoodDesireability * generateRandom();
        gencreatureSenseRadius = gencreatureSenseRadius * generateRandom();

        // Weight
        totalWeight = totalWeight * generateRandomUp();
    }

    public float generateRandom()
    {
        return (1 + UnityEngine.Random.Range(-fluctuation, fluctuation));
    }

    public float generateRandomUp()
    {
        return (1 + UnityEngine.Random.Range(-fluctuation, fluctuation*1.003f));
    }

    public void MinMax()
    {
        if (genreproducePeriode > reproducePeriodeMax) { genreproducePeriode = reproducePeriodeMax; }
        if (genmaxReproduceTimes > maxReproduceTimesMax) { genmaxReproduceTimes = maxReproduceTimesMax; }
        if (genreproduceMultiplyFactor > reproduceMultiplyFactorMax) { genreproduceMultiplyFactor = reproduceMultiplyFactorMax; }
        if (genreproduceAddFactor > reproduceAddFactorMax) { genreproduceAddFactor = reproduceAddFactorMax; }
        if (geniniHunger > iniHungerMax) { geniniHunger = iniHungerMax; }
        if (genmaxHunger > maxHungerMax) { genmaxHunger = maxHungerMax; }
        if (genmaxLifeTime > maxLifeTimeMax) { genmaxLifeTime = maxLifeTimeMax; }
        if (genmaxObjectNum > maxObjectNumMax) { genmaxObjectNum = maxObjectNumMax; }
        if (genmoveTime > moveTimeMax) { genmoveTime = moveTimeMax; }
        if (genspeedFactor > speedFactorMax) { genspeedFactor = speedFactorMax; }
        if (genfoodDesireability > foodDesireabilityMax) { genfoodDesireability = foodDesireabilityMax; }
        if (gencreatureSenseRadius > creatureSenseRadiusMax) { gencreatureSenseRadius = creatureSenseRadiusMax; }

        if(totalWeight > totalWeightMax) { totalWeight = totalWeightMax; }

        if (genreproducePeriode < reproducePeriodeMin) { genreproducePeriode = reproducePeriodeMin; }
        if (genmaxReproduceTimes < maxReproduceTimesMin) { genmaxReproduceTimes = maxReproduceTimesMin; }
        if (genreproduceMultiplyFactor < reproduceMultiplyFactorMin) { genreproduceMultiplyFactor = reproduceMultiplyFactorMin; }
        if (genreproduceAddFactor < reproduceAddFactorMin) { genreproduceAddFactor = reproduceAddFactorMin; }
        if (geniniHunger < iniHungerMin) { geniniHunger = iniHungerMin; }
        if (genmaxHunger < maxHungerMin) { genmaxHunger = maxHungerMin; }
        if (genmaxLifeTime < maxLifeTimeMin) { genmaxLifeTime = maxLifeTimeMin; }
        if (genmaxObjectNum < maxObjectNumMin) { genmaxObjectNum = maxObjectNumMin; }
        if (genmoveTime < moveTimeMin) { genmoveTime = moveTimeMin; }
        if (genspeedFactor < speedFactorMin) { genspeedFactor = speedFactorMin; }
        if (genfoodDesireability < foodDesireabilityMin) { genfoodDesireability = foodDesireabilityMin; }
        if (gencreatureSenseRadius < creatureSenseRadiusMin) { gencreatureSenseRadius = creatureSenseRadiusMin; }
        
        if (totalWeight < totalWeightMin) { totalWeight = totalWeightMin; }
    }

    public void Normalize()
    {
        temp = 0;
        temp += (1 + (reproducePeriodeIni - genreproducePeriode) / reproducePeriodeIni) * 20;
        temp += (1 + (maxReproduceTimesIni - genmaxReproduceTimes) / maxReproduceTimesIni) * 13;
        temp += (1 + (reproduceMultiplyFactorIni - genreproduceMultiplyFactor) / reproduceMultiplyFactorIni) * 30;
        temp += (1 + (reproduceAddFactorIni - genreproduceAddFactor) / reproduceAddFactorIni) * 40;
        temp += (1 + (iniHungerIni - geniniHunger) / iniHungerIni) * 20;
        temp += (1 + (maxHungerIni - genmaxHunger) / maxHungerIni) * 45;
        temp += (1 + (maxLifeTimeIni - genmaxLifeTime) / maxLifeTimeIni) * 40;
        temp += (1 - (maxObjectNumIni - genmaxObjectNum) / maxObjectNumIni) * 20;
        temp += (1 - (moveTimeIni - genmoveTime) / moveTimeIni) * 15;
        temp += (1 + (speedFactorIni - genspeedFactor) / speedFactorIni) * 75;
        temp += (1 + (foodDesireabilityIni - genfoodDesireability) / foodDesireabilityIni) * 85;
        temp += (1 + (creatureSenseRadiusIni - gencreatureSenseRadius) / creatureSenseRadiusIni) * 99;

        temp = totalWeight / temp;

        if (1 > temp)
        {
            genreproducePeriode = genreproducePeriode * temp;
            genmaxReproduceTimes = Mathf.RoundToInt(genmaxReproduceTimes * temp);
            genreproduceMultiplyFactor = genreproduceMultiplyFactor * temp;
            genreproduceAddFactor = genreproduceAddFactor * temp;
            geniniHunger = geniniHunger * temp;
            genmaxHunger = genmaxHunger * temp;
            genmaxLifeTime = genmaxLifeTime * temp;
            genmaxObjectNum = Mathf.RoundToInt(genmaxObjectNum * temp);
            genmoveTime = genmoveTime * temp;
            genspeedFactor = genspeedFactor * temp;
            genfoodDesireability = genfoodDesireability * temp;
            gencreatureSenseRadius = gencreatureSenseRadius * temp;
        }


    }

    public float getgenreproducePeriode() { return genreproducePeriode; }
    public float getgenmaxReproduceTimes() { return genmaxReproduceTimes; }
    public float getgenreproduceMultiplyFactor() { return genreproduceMultiplyFactor; }
    public float getgenreproduceAddFactor() { return genreproduceAddFactor; }
    public float getgeniniHunger() { return geniniHunger; }
    public float getgenmaxHunger() { return genmaxHunger; }
    public float getgenmaxLifeTime() { return genmaxLifeTime; }
    public float getgenmaxObjectNum() { return genmaxObjectNum; }
    public float getgenmoveTime() { return genmoveTime; }
    public float getgenspeedFactor() { return genspeedFactor; }
    public float getgenfoodDesireability() { return genfoodDesireability; }
    public float getgencreatureSenseRadius() { return gencreatureSenseRadius; }
    public float getgencreatureGeneration() { return gencreatureGeneration; }


    public void setgenreproducePeriode(float a) { genreproducePeriode = a; }
    public void setgenmaxReproduceTimes(float a) { genmaxReproduceTimes = a; }
    public void setgenreproduceMultiplyFactor(float a) { genreproduceMultiplyFactor = a; }
    public void setgenreproduceAddFactor(float a) { genreproduceAddFactor = a; }
    public void setgeniniHunger(float a) { geniniHunger = a; }
    public void setgenmaxHunger(float a) { genmaxHunger = a; }
    public void setgenmaxLifeTime(float a) { genmaxLifeTime = a; }
    public void setgenmaxObjectNum(float a) { genmaxObjectNum = a; }
    public void setgenmoveTime(float a) { genmoveTime = a; }
    public void setgenspeedFactor(float a) { genspeedFactor = a; }
    public void setgenfoodDesireability(float a) { genfoodDesireability = a; }
    public void setgencreatureSenseRadius(float a) { gencreatureSenseRadius = a; }
    public void setgencreatureGeneration(float a) { gencreatureGeneration = a+1; }

}

