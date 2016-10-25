using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class PortalDebugger : MonoBehaviour 
{
	

	public bool       active = false;
	[Range(1,2)]
	public int         layerNumber;
	public LayerInfo[] layerInfo;
	public GameObject quad;
	private int prevLayerNumber;

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
		
		if ( active ) {
			if (layerNumber <= layerInfo.Length) {
				quad.SetActive (true);

				if( prevLayerNumber != layerNumber ) {
					SwitchLayer ();
				}
				prevLayerNumber = layerNumber;

				if (SceneView.lastActiveSceneView != null) {					
					transform.position = SceneView.lastActiveSceneView.camera.transform.position;
					transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
				}		
			} else {
				Debug.Log ( "Invalid layer index" );
			}

		} else {
			quad.SetActive (false);
		}

	}

	private void SwitchLayer()
	{
		quad.GetComponent<MeshRenderer> ().material = layerInfo[layerNumber - 1].layerMaterial;

		foreach (LayerInfo layer in layerInfo) {
			Tools.lockedLayers |= 1 << LayerMask.NameToLayer (layer.layerName); // Add all portal layers to locked layers
		}

		Tools.lockedLayers &= ~(1 << LayerMask.NameToLayer ( layerInfo[layerNumber - 1].layerName )); // Add all portal layers to locked layers
	}
}


[System.Serializable]
public struct LayerInfo
{
	public Material layerMaterial;
	public string   layerName;
}