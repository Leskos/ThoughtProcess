using UnityEngine;
using System.Collections;

public class PortalBoxTrigger : MonoBehaviour {
	
	public bool boxMode = false;
	private GameObject boxPortal;
	private GameObject flatPortal;


	// Use this for initialization
	void Start () 
	{
		foreach (Transform child in transform) {
			
			if (child.name == "FlatPortal") {
				flatPortal = child.gameObject;	
			}
			if (child.name == "BoxPortal") {
				boxPortal = child.gameObject;	
			}
		}
	}

	void Update()
	{
		if (boxMode) {
			boxPortal.SetActive (  true  );
			flatPortal.SetActive ( false );
		} else {
			boxPortal.SetActive (  false );
			flatPortal.SetActive ( true  );
		}
	}

	void OnTriggerEnter( Collider other )
	{
		Debug.Log ( "Trigger entered " );
		if( other.tag == "PlayerHeadCollider" )
		{
			boxMode = true;
		}
	}

	void OnTriggerExit( Collider other )
	{
		Debug.Log ( "Trigger entered " );
		if( other.tag == "PlayerHeadCollider" )
		{
			boxMode = false;
		}
	}
	
}
