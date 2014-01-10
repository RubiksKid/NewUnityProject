using UnityEngine;
using System.Collections;

public class SpecialTrigger : MonoBehaviour {
	public Transform cameraTrigger;
	public BoxCollider2D box;
	public GameObject worldCameraObj;
	private WorldCamera worldCamera;
	public float lerpFloat = 5f;

	private float deltaD;
	// Use this for initialization
	void Start () {

		worldCamera = worldCameraObj.GetComponent<WorldCamera>();
		//box = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		
	 deltaD = Vector3.Distance(Camera.main.transform.position,cameraTrigger.transform.position);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.name == "Character")
		{

			worldCamera.isTriggered = true;
		Camera.main.transform.position = new Vector3( 
			                                             Mathf.Lerp(Camera.main.transform.position.x,cameraTrigger.position.x,(lerpFloat/deltaD)*Time.deltaTime),
			                                             Mathf.Lerp(Camera.main.transform.position.y,cameraTrigger.position.y,(lerpFloat/deltaD)*Time.deltaTime),
		                                             Camera.main.transform.position.z);
		}
	}

	void OnTriggerExit2D()
	{
		worldCamera.isTriggered = false;
	}
}
