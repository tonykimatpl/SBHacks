﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Solutions are a collection of liquids that cause interesting properties.
/// </summary>
public class Solution : ScriptableObject{
    public List<Liquid> liquidComponents = new List<Liquid>();
    public float currentAmount = 0;


    //Temp in Celcius because fahrenheit is for nubs
    public float temperature = 0;

    /// <summary>
    /// Get the color of the liquid based on the colors of the components.
    /// </summary>
    /// <returns>The appropriate color.</returns>
    public Color getColor()
    {
        float totalAmount = getAmount();

        Color finalColor = new Color();


        for (int i = 0; i < liquidComponents.Count; i++)
        {
            finalColor += liquidComponents[i].color * liquidComponents[i].amount / totalAmount;
        }
        return finalColor;
    }

    
    /// <summary>
    /// Get the total amount of liquid including components.
    /// </summary>
    /// <returns>The total amount of liquid</returns>
    public float getAmount()
    {
        float totalAmount = 0f;
        for (int i = 0; i < liquidComponents.Count; i++)
        {
            totalAmount += liquidComponents[i].amount;
        }

        return totalAmount;
    }

    /// <summary>
    /// A method to multiply the solution, so the amount is increased or decreased while keeping proportions of the components.
    /// </summary>
    /// <param name="factor">The amount to multiply by.</param>
    public void multiplyByFactor(float factor)
    {
        Debug.Log("fact" + factor);
        for (int i = 0; i < liquidComponents.Count; i++)
        {
            Debug.Log("count" + liquidComponents.Count);
            liquidComponents[i].amount *= factor;
            Debug.Log(liquidComponents[i].amount);
        }
        //currentAmount *= factor;
    }

    /// <summary>
    /// Add one solution to another, setting temps, adding components, setting amount.
    /// </summary>
    /// <param name="other">The other solution to be added to this one.</param>
    public void addToSolution(Solution other)
    {
        //Set temperature.
        this.temperature = temperature * currentAmount + other.temperature * other.currentAmount;

        for (int i = 0; i < other.liquidComponents.Count; i++)
        {
            liquidComponents.Add(other.liquidComponents[i]);
        }

        currentAmount = getAmount();

    }

    /// <summary>
    /// Add one liquid to another, setting temps, adding components, setting amount.
    /// </summary>
    /// <param name="other">The other solution to be added to this one.</param>
    public void addToSolution(Liquid liquidToAdd)
    {
        //No temperature.
        //this.temperature = temperature * currentAmount + other.temperature * other.currentAmount;

        liquidComponents.Add(liquidToAdd);

        currentAmount += liquidToAdd.amount;
    }

    /// <summary>
    /// Solution copy ctor.
    /// </summary>
    /// <param name="other"></param>
    public Solution(Solution other)
    {
        //Set temperature.
        this.temperature = other.temperature;

        for (int i = 0; i < other.liquidComponents.Count; i++)
        {
            this.liquidComponents.Add(new Liquid(other.liquidComponents[i]));
        }
        currentAmount = other.currentAmount;
    }

    /// <summary>
    /// Solution copy ctor.
    /// </summary>
    /// <param name="other"></param>
    public void init(Solution other)
    {
        //Set temperature.
        this.temperature = other.temperature;

        for (int i = 0; i < other.liquidComponents.Count; i++)
        {
            Liquid newLiquid = ScriptableObject.CreateInstance<Liquid>();
            newLiquid.init(other.liquidComponents[i]);
            this.liquidComponents.Add(newLiquid);
        }
        currentAmount = other.currentAmount;
    }


    /// <summary>
    /// Solution empty ctor. Basically nothing, everything is already initialized.
    /// </summary>
    /// <param name="other"></param>
    public Solution(){}
}