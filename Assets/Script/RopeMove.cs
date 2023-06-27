using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RopeMove : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    Vector3 dir;

    void Start() => Destroy(gameObject, 3.5f);

    private void Update()
    {
        transform.up = dir;
        transform.position += (dir * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PV.RPC("DestoryRPC", RpcTarget.AllBuffered);
        }
    }


    [PunRPC]
    void DirRPC(Vector3 dir) => this.dir = dir;

    [PunRPC]
    void DestoryRPC() => Destroy(gameObject);

}
