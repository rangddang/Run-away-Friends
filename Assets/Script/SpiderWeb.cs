using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpiderWeb : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    bool isItem;

    private void Awake() => isItem = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isItem && other.GetComponent<PhotonView>().IsMine)
        {
            isItem = false;
        }
        else if (other.gameObject.tag == "Player")
        {

            PV.RPC("DestoryRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestoryRPC() => Destroy(gameObject);
}
