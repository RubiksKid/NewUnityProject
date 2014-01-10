using UnityEngine;
using System.Collections;

public class WorldCamera : FollowPlayer{
	public float orthSize= 5f;
	public float editorAspect= 1.6f;
	public Camera childCam;//lol child cam... sounds illegal
	public bool isTriggered= false;
	// Use this for initialization
	public override void Start () {
		cam.orthographicSize = editorAspect * orthSize / cam.aspect;
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();

		cam.orthographicSize  =  editorAspect * orthSize / cam.aspect;
	}

	public override void MidCase()
	{
	



		float deltaD = Vector3.Distance(childCam.transform.position,followee.transform.position);

		if(!isTriggered)
		{
			childCam.transform.position = new Vector3( 
		                                          	Mathf.Lerp(childCam.transform.position.x,followee.transform.position.x,(lerpFloat/deltaD)*Time.deltaTime),
		                                          	Mathf.Lerp(childCam.transform.position.y,followee.transform.position.y,(lerpFloat/deltaD)*Time.deltaTime),
		                                          	childCam.transform.position.z);

				if(deltaD < 3f )
				{
				childCam.transform.position = new Vector3( 
				                                          followee.transform.position.x,
				                                          followee.transform.position.y,
				                                          childCam.transform.position.z);
				}
		}
	}
	public override void CenterCase()
	{
		MidCase();
	}
	public override void RightCase()
	{
		MidCase();
	}

	public override void OffScreen()
	{

	}

}
