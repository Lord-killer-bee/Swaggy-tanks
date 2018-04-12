using UnityEngine;
using System.Collections;

public class SyncManager : Photon.MonoBehaviour {

    Vector3 newPosition = Vector3.zero;
    Quaternion newRotation = Quaternion.identity;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            //do nothing
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.1f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            newPosition = (Vector3)stream.ReceiveNext();
            newRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
