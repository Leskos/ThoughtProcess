using UnityEngine;
using System.Collections;

public class SpawnStars : MonoBehaviour {

	public float minRadius = 1.0f;
	public float maxRadius = 2.0f;
	public float minSize   = 0.001f;
	public float maxSize   = 0.03f;

	// Use this for initialization
	void Awake () 
	{
		foreach (Transform star in transform) 
		{
			star.localPosition = Random.onUnitSphere * Random.Range( minRadius, maxRadius );
			if( star.localPosition.z < 0 ) {
				star.localPosition = new Vector3 ( star.localPosition.x, star.localPosition.y, -star.localPosition.z );
			}
			star.LookAt (transform);
			float randomScale = Random.Range (minSize,maxSize);
			star.localScale = new Vector3 ( randomScale, randomScale, randomScale );
		}
	}

}
