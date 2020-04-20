using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class LoadCardboard : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadDevice("cardboard"));
    }

    IEnumerator LoadDevice(string newDevice)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }
}
