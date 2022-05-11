using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPText : MonoBehaviour
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
