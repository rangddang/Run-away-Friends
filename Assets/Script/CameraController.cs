using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraController : MonoBehaviourPunCallbacks
{
    //public PhotonView PV;

    
    public GameObject target;

    public float turnSpeed;

    public Vector3 offset;
    Vector3 Dir;

    private void Start()
    {
        
    }

    void Update()
    {
        
        CameraMove();
    }

    void CameraMove()
    {

        Vector3 direction = (target.transform.position - transform.position);
        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        //Dir = player.moveVec;
        Vector3 dir = (target.transform.position + offset - transform.position) * 0.2f;
        transform.position += dir;
    }
}
