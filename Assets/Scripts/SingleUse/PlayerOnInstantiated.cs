using UnityEngine;
using Photon.Pun;

public class PlayerOnInstantiated : MonoBehaviourPun
{
    [SerializeField]
    Behaviour[] componentsToEnable;
    private void Start()
    {
        // if player is not local player disable the camera and reticle pointer
        if(photonView.IsMine)
        {
            for (int i = 0; i < componentsToEnable.Length; i++)
            {
                componentsToEnable[i].enabled = true;
                componentsToEnable[i].gameObject.SetActive(true);
                Debug.Log($"Enabled: {componentsToEnable[i].name}");
            }
        }
    }
}
