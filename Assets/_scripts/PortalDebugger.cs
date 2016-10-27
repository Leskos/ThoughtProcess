using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
#endif

public class PortalDebugger : MonoBehaviour 
{
	#if UNITY_EDITOR

	public bool        active = false;
	public int         layerNumber;
	public LayerInfo[] layerInfo;
	public GameObject  quad;

	private int        prevLayerNumber;
	private bool       prevActive;

	void OnEnable()
	{
		EditorApplication.update += PositionPortal;
	}

	void OnDisable()
	{
		EditorApplication.update -= PositionPortal;
	}


	void PositionPortal()
	{

		// Re-enable all layers when deactivating
		if( prevActive && !active ) { 
			Debug.Log ( "Portal Debugger : Reactivating all layers" );
			foreach ( LayerInfo layer in layerInfo ) {
				Tools.lockedLayers  &= ~(1 << LayerMask.NameToLayer ( layer.layerName )); // Unlock all portal layers
				Tools.visibleLayers |=   1 << LayerMask.NameToLayer ( layer.layerName );  // Show all portal layers
			}
		}

		// Re-solo layer when reactiveating
		if( !prevActive && active ) { 
			Debug.Log ( "Portal Debugger : Re-soloing layer" );
			SoloLayer ();
		}
		prevActive = active;

		if ( active ) {
			if ( layerNumber > 0 && layerNumber <= layerInfo.Length ) {
				
				// Switch layers if needed
				if( prevLayerNumber != layerNumber ) { SoloLayer (); }
				prevLayerNumber = layerNumber;

				// Reposition quad to match scene view
				quad.SetActive (true);
				if (SceneView.lastActiveSceneView != null) {					
					transform.position = SceneView.lastActiveSceneView.camera.transform.position;
					transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
				}		
			} else {
				Debug.Log ( "Invalid layer index : " + layerNumber );
			}

		} else {
			quad.SetActive (false);
		}
	}


	private void SoloLayer()
	{
		Debug.Log ( "Portal Debugger : Soloing layer" );
		quad.GetComponent<MeshRenderer> ().material = layerInfo[layerNumber - 1].layerMaterial;

		foreach ( LayerInfo layer in layerInfo ) {
			Tools.lockedLayers  |= 1 << LayerMask.NameToLayer (    layer.layerName );  // Lock all portal layers
			Tools.visibleLayers  &= ~(1 << LayerMask.NameToLayer ( layer.layerName )); // Hide all portal layers
		}
		Tools.lockedLayers  &= ~(1 << LayerMask.NameToLayer ( layerInfo[layerNumber - 1].layerName )); // Unlock selected portal layer
		Tools.visibleLayers |=   1 << LayerMask.NameToLayer ( layerInfo[layerNumber - 1].layerName );  // Show selected portal layer
	}
	#endif
}


[System.Serializable]
public struct LayerInfo
{
	public Material layerMaterial;
	public string   layerName;
}