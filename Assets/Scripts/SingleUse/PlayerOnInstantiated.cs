using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

public class PlayerOnInstantiated : MonoBehaviourPun
{
    [SerializeField]
    Behaviour[] componentsToEnable;

    private void Awake()
    {
        
    }

    private void Start()
    {
        // enable player camera after enabling XR settings
        // if player is local player enable the camera and reticle pointer
        if (photonView.IsMine)
        {
            for (int i = 0; i < componentsToEnable.Length; i++)
            {
                componentsToEnable[i].enabled = true;
                componentsToEnable[i].gameObject.SetActive(true);
                Debug.Log($"Enabled: {componentsToEnable[i].name}");
            }
            StartCoroutine(LoadDevice("cardboard"));
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);

        yield return null;

        XRSettings.enabled = true;
    }

}
