using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public PlayerController player;
    public Text myHP;
    public Image rectHP;
    
    private void Start()
    {
    }

    
    void Update()
    {
        rectHP.fillAmount = (player.hp / player.hpMax);
        myHP.text = ((int)player.hp) + " / " + player.hpMax;
    }
}
