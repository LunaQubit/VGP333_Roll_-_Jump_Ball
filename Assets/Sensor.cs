using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
    {
        transform.parent.SendMessage("SensorHit", this, SendMessageOptions.DontRequireReceiver);
    }
}
