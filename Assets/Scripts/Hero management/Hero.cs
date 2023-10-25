using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum HeroName
{
    Hawk,
    Gull,
    Owl,
    Crow,
    HeroCount
}

public class Hero : MonoBehaviour
{
    public int points = 0;

    [SerializeField] private int_GameEvent heroChosen;
    
    public void ChooseHero()
    {
        heroChosen.Raise(transform.GetSiblingIndex());
    }

    private void Start()
    {
        UpdatePointsText();
    }

    public void UpdatePointsText()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (points >= 0 ? "+" + points.ToString() : points.ToString());  
    }
}

