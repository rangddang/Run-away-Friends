using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    PlayerController player;
    Text myHP;
    public RectTransform rectHP;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        myHP =  GetComponent<Text>();
    }

    
    void Update()
    {
        rectHP.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (player.hp/player.hpMax)*190);
        myHP.text = ((int)player.hp) + " / " + player.hpMax;
    }
}
