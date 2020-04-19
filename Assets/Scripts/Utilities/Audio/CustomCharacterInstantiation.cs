using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class CustomCharacterInstantiation : MonoBehaviourPunCallbacks
{
    public Transform SpawnPosition;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate; // set in inspector

    public delegate void OnCharacterInstantiated(GameObject character);
    public static event OnCharacterInstantiated CharacterInstantiated;


    public override void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            int index = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % 4;
            Vector3 spawnPos = Vector3.zero;
            if (this.SpawnPosition != null)
            {
                spawnPos = this.SpawnPosition.position;
            }
            Vector3 random = Random.insideUnitSphere;
            random = this.PositionOffset * random.normalized;
            spawnPos += random;
            spawnPos.y = 0;
            Camera.main.transform.position += spawnPos;

            GameObject o = PrefabsToInstantiate[index];
            o = PhotonNetwork.Instantiate(o.name, spawnPos, Quaternion.identity);
            if (CharacterInstantiated != null)
            {
                CharacterInstantiated(o);
            }
        }
    }

}
