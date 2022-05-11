using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour
{
    public Texture[] item ;
    PlayerController player;
    RawImage image;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        image = GetComponent<RawImage>();

        image.texture = (Texture)item[0];
    }

    void Update()
    {
        if(player.item > 0)
        {
            image.texture = item [player.item];
        }
        else
        {
            image.texture=  item [0];
        }
    }
}
