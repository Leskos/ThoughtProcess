using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour 
{
	public Vector3 rotationsPerMinute = Vector3.zero;

	void Update () 
	{
		transform.Rotate ( rotationsPerMinute.x*6.0f*Time.deltaTime, rotationsPerMinute.y*6.0f*Time.deltaTime, rotationsPerMinute.z*6.0f*Time.deltaTime );
	}
}
