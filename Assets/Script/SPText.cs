using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPText : MonoBehaviour
{
    PlayerController player;
    Text mySP;
    public RectTransform rectSP;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        mySP = GetComponent<Text>();
    }

    
    void Update()
    {
        rectSP.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (player.sp / player.spMax) * 190);
        mySP.text = ((int)(player.sp)) + " / " + player.spMax;
    }
}
