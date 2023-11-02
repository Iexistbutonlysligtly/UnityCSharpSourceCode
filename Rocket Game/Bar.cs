using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

    private Image SlideBar;
    public float CurrentHealth;
    private float BarInitialValue;
    Flap Player;

    private void Start()
    {
        SlideBar = GetComponent<Image>();
        Player = FindObjectOfType<Flap>();
        BarInitialValue = Player.exhaustFuel;



    }



    private void Update()
    {
        CurrentHealth = Mathf.Clamp(Player.exhaustFuel, 0, Player.exhaustFuel);
        SlideBar.fillAmount = CurrentHealth / BarInitialValue;
    }
}

