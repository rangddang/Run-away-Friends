using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SPText : MonoBehaviourPunCallbacks
{
    public PlayerController player;
    public Text mySP;
    public Image rectSP;

    private void Start()
    {
    }

    
    void Update()
    {
        rectSP.fillAmount = (player.sp / player.spMax);
        mySP.text = ((int)(player.sp)) + " / " + player.spMax;
    }
}
