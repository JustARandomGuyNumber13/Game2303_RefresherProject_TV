using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorScript.Door door;

    private void OnTriggerEnter(Collider other)
    {
        door.OpenDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        door.OpenDoor();
    }
}
