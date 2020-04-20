using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;

    public Quaternion rotate = Quaternion.identity;

    // Update is called once per frame
    void LateUpdate()
    {
        rotate.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, playerCamera.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = rotate;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerCamera.transform.forward, 1.0f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
