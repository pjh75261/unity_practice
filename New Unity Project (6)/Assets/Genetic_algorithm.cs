using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genetic_algorithm : MonoBehaviour
{
    // Start is called before the first frame update


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
    [SerializeField] float foodDesireability = 1f;



    //가중치
    [SerializeField] float reproducePeriodeWeightWeight = 20;
    [SerializeField] float maxReproduceTimesWeightWeight = 13;
    [SerializeField] float reproduceMultiplyFactorWeightWeight = 30;
    [SerializeField] float reproduceAddFactorWeightWeight = 40;
    [SerializeField] float iniHungerWeightWeight = 20;
    [SerializeField] float maxHungerWeightWeight = 45;
    [SerializeField] float maxLifeTimeWeightWeight = 40;
    [SerializeField] float maxObjectNumWeightWeight = 20;
    [SerializeField] float moveTimeWeightWeight = 15;
    [SerializeField] float speedFactorWeightWeight = 75;
    [SerializeField] float foodDesireabilityWeightWeight = 85;

    //최초
    float reproducePeriodeIni = 5.5f;
    int maxReproduceTimesIni = 8;
    float reproduceMultiplyFactorIni = 0.8f;
    float reproduceAddFactorIni = 0.5f;
    float iniHungerIni = 20f;
    float maxHungerIni = 50f;
    float maxLifeTimeIni = 600f;
    int maxObjectNumIni = 300;
    float moveTimeIni = 1.0f;
    float speedFactorIni = 100f;
    float foodDesireabilityIni = 1f;

    //MAX
    [SerializeField] float reproducePeriodeMax = 5.5f;
    [SerializeField] int maxReproduceTimesMax = 20;
    [SerializeField] float reproduceMultiplyFactorMax = 0.9f;
    [SerializeField] float reproduceAddFactorMax = 0.9f;
    [SerializeField] float iniHungerMax = 70f;
    [SerializeField] float maxHungerMax = 100f;
    [SerializeField] float maxLifeTimeMax = 1000f;
    [SerializeField] int maxObjectNumMax = 400;
    [SerializeField] float moveTimeMax = 50f;
    [SerializeField] float speedFactorMax = 400f;
    [SerializeField] float foodDesireabilityMax = 30f;

    //Min
    [SerializeField] float reproducePeriodeMin = 0f;
    [SerializeField] int maxReproduceTimesMin = 1;
    [SerializeField] float reproduceMultiplyFactorMin = 0;
    [SerializeField] float reproduceAddFactorMin = 0;
    [SerializeField] float iniHungerMin = 0;
    [SerializeField] float maxHungerMin = 1;
    [SerializeField] float maxLifeTimeMin = 1;
    [SerializeField] int maxObjectNumMin = 1;
    [SerializeField] float moveTimeMin = 0.1f;
    [SerializeField] float speedFactorMin = 0.1f;
    [SerializeField] float foodDesireabilityMin = 0f;

    //normalize
    float temp = 0;


    public void setValue()
    {
        MinMax();
        Normalize();
    }

    public void MinMax()
    {
        if (reproducePeriode > reproducePeriodeMax) { reproducePeriode = reproducePeriodeMax; }
        if (maxReproduceTimes > maxReproduceTimesMax) { maxReproduceTimes = maxReproduceTimesMax; }
        if (reproduceMultiplyFactor > reproduceMultiplyFactorMax) { reproduceMultiplyFactor = reproduceMultiplyFactorMax; }
        if (reproduceAddFactor > reproduceAddFactorMax) { reproduceAddFactor = reproduceAddFactorMax; }
        if (iniHunger > iniHungerMax) { iniHunger = iniHungerMax; }
        if (maxHunger > maxHungerMax) { maxHunger = maxHungerMax; }
        if (maxLifeTime > maxLifeTimeMax) { maxLifeTime = maxLifeTimeMax; }
        if (maxObjectNum > maxObjectNumMax) { maxObjectNum = maxObjectNumMax; }
        if (moveTime > moveTimeMax) { moveTime = moveTimeMax; }
        if (speedFactor > speedFactorMax) { speedFactor = speedFactorMax; }
        if (foodDesireability > foodDesireabilityMax) { foodDesireability = foodDesireabilityMax; }
        if (reproducePeriode < reproducePeriodeMin) { reproducePeriode = reproducePeriodeMin; }
        if (maxReproduceTimes < maxReproduceTimesMin) { maxReproduceTimes = maxReproduceTimesMin; }
        if (reproduceMultiplyFactor < reproduceMultiplyFactorMin) { reproduceMultiplyFactor = reproduceMultiplyFactorMin; }
        if (reproduceAddFactor < reproduceAddFactorMin) { reproduceAddFactor = reproduceAddFactorMin; }
        if (iniHunger < iniHungerMin) { iniHunger = iniHungerMin; }
        if (maxHunger < maxHungerMin) { maxHunger = maxHungerMin; }
        if (maxLifeTime < maxLifeTimeMin) { maxLifeTime = maxLifeTimeMin; }
        if (maxObjectNum < maxObjectNumMin) { maxObjectNum = maxObjectNumMin; }
        if (moveTime < moveTimeMin) { moveTime = moveTimeMin; }
        if (speedFactor < speedFactorMin) { speedFactor = speedFactorMin; }
        if (foodDesireability < foodDesireabilityMin) { foodDesireability = foodDesireabilityMin; }
    }

    public void Normalize()
    {
        temp = 0;
        temp += (1 + (reproducePeriodeIni - reproducePeriode) / reproducePeriodeIni) * 20;
        temp += (1 + (maxReproduceTimesIni - maxReproduceTimes) / maxReproduceTimesIni) * 13;
        temp += (1 + (reproduceMultiplyFactorIni - reproduceMultiplyFactor) / reproduceMultiplyFactorIni) * 30;
        temp += (1 + (reproduceAddFactorIni - reproduceAddFactor) / reproduceAddFactorIni) * 40;
        temp += (1 + (iniHungerIni - iniHunger) / iniHungerIni) * 20;
        temp += (1 + (maxHungerIni - maxHunger) / maxHungerIni) * 45;
        temp += (1 + (maxLifeTimeIni - maxLifeTime) / maxLifeTimeIni) * 40;
        temp += (1 - (maxObjectNumIni - maxObjectNum) / maxObjectNumIni) * 20;
        temp += (1 - (moveTimeIni - moveTime) / moveTimeIni) * 15;
        temp += (1 + (speedFactorIni - speedFactor) / speedFactorIni) * 75;
        temp += (1 + (foodDesireabilityIni - foodDesireability) / foodDesireabilityIni) * 85;

        temp = 403 / temp;

        if (1 > temp) {
            reproducePeriode = reproducePeriode * temp;
            maxReproduceTimes = Mathf.RoundToInt(maxReproduceTimes * temp);
            reproduceMultiplyFactor = reproduceMultiplyFactor * temp;
            reproduceAddFactor = reproduceAddFactor * temp;
            iniHunger = iniHunger * temp;
            maxHunger = maxHunger * temp;
            maxLifeTime = maxLifeTime * temp;
            maxObjectNum = Mathf.RoundToInt(maxObjectNum * temp);
            moveTime = moveTime * temp;
            speedFactor = speedFactor * temp;
            foodDesireability = foodDesireability * temp;
        }


    }

    public float getreproducePeriode() { return reproducePeriode; }
    public int getmaxReproduceTimes() { return maxReproduceTimes; }
    public float getreproduceMultiplyFactor() { return reproduceMultiplyFactor; }
    public float getreproduceAddFactor() { return reproduceAddFactor; }
    public float getiniHunger() { return iniHunger; }
    public float getmaxHunger() { return maxHunger; }
    public float getmaxLifeTime() { return maxLifeTime; }
    public int getmaxObjectNum() { return maxObjectNum; }
    public float getmoveTime() { return moveTime; }
    public float getspeedFactor() { return speedFactor; }
    public float getfoodDesireability() { return foodDesireability; }

    public void inputreproducePeriode(float a) { reproducePeriode = a; }
    public void inputmaxReproduceTimes(int a) { maxReproduceTimes = a; }
    public void inputreproduceMultiplyFactor(float a) { reproduceMultiplyFactor = a; }
    public void inputreproduceAddFactor(float a) { reproduceAddFactor = a; }
    public void inputiniHunger(float a) { iniHunger = a; }
    public void inputmaxHunger(float a) { maxHunger = a; }
    public void inputmaxLifeTime(float a) { maxLifeTime = a; }
    public void inputmaxObjectNum(int a) { maxObjectNum = a; }
    public void inputmoveTime(float a) { moveTime = a; }
    public void inputspeedFactor(float a) { speedFactor = a; }
    public void inputfoodDesireability(float a) { foodDesireability = a; }

}
