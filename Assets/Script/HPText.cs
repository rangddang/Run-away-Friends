using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HPText : MonoBehaviourPunCallbacks
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
