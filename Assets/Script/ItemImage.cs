using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour
{
    public Texture[] item ;
    public PlayerController player;
    RawImage image;


    private void Start()
    {
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
