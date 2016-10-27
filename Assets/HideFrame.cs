using UnityEngine;
using System.Collections;
using SVGImporter;

public class HideFrame : MonoBehaviour 
{

	private SVGRenderer svgRenderer;
	public  Transform playerTranform;

	// Use this for initialization
	void Awake() {
		svgRenderer = GetComponent<SVGRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTranform.position.z > transform.position.z+0.01) {
			svgRenderer.enabled = false;
		} else {
			svgRenderer.enabled = true;
		}
	}
}
