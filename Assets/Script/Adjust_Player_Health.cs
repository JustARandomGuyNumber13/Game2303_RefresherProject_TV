using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust_Player_Health : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private float coolDownDuration;
    private float coolDownCount = 0;

    private void Update()
    {
        if(coolDownCount < coolDownDuration)
            coolDownCount += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && coolDownCount >= coolDownDuration)
        {
            other.GetComponent<Player>().PublicAdjustHealth(value);
            coolDownCount = 0;
        }
    }
}
