using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject followee;
	public Camera cam;
	public float lerpFloat = 15f;
	public enum STATE {left=-1,mid=0,right=1,offscreen=2,centerline=3,special=4};
	public float deltaD;
	public STATE thirdLocOfFollowee;
	public Vector3 viewPos;


	public STATE CheckState()
	{
		var unitX = cam.WorldToViewportPoint(new Vector3(followee.transform.position.x,0,0)).x;
		var unitY = cam.WorldToViewportPoint(new Vector3(0, followee.transform.position.y,0)).y;
		viewPos = new Vector2(unitX,unitY);

			if( viewPos.y < 1 && viewPos.y > 0)
			{
				if( viewPos.x > 0 && viewPos.x < .333f)
					return STATE.left;
				else if( viewPos.x < .666f && viewPos.x >.333f)
				{
					if(viewPos.x < .505f && viewPos.x > .495f)
						return STATE.centerline;
					else
						return STATE.mid;
				}
				else if( viewPos.x > .666f && viewPos.x < 1f)
					return STATE.right;
				else
					return STATE.offscreen;
			}
			else
				return STATE.offscreen;
				
	}
	public STATE overrideState()
	{
		return STATE.special;
	}
	public virtual void LeftCase()
	{

	}

	public virtual void MidCase()
	{

	}

	public virtual void CenterCase()
	{

	}
	public virtual void RightCase()
	{

	}

	// Use this for initialization
	public virtual void Start () 
	{


	}

	public virtual void OffScreen()
	{
		deltaD = Vector3.Distance(cam.transform.position,followee.transform.position);
		cam.transform.position = new Vector3( 
		                                     Mathf.Lerp(cam.transform.position.x,followee.transform.position.x,(lerpFloat/deltaD)*Time.deltaTime),
		                                     Mathf.Lerp(cam.transform.position.y,followee.transform.position.y,(lerpFloat/deltaD)*Time.deltaTime),
		                                     cam.transform.position.z);
	}
	// Update is called once per frame
	public virtual void Update () 
	{
		float deltaD = Vector3.Distance(cam.transform.position,followee.transform.position);
		switch (CheckState())
		{
			case STATE.left:
			LeftCase();
					break;

			case STATE.mid:
			MidCase();		
					break;

			case STATE.centerline:
			CenterCase();
					break;

			case STATE.right:
			RightCase();
					break;

			case STATE.offscreen:
			OffScreen();
					break;
		}
		thirdLocOfFollowee = CheckState();

		var unitX = (1/cam.aspect)*cam.WorldToViewportPoint(new Vector3(followee.transform.position.x,0,0)).x;
		var unitY = cam.aspect*cam.WorldToViewportPoint(new Vector3(0, followee.transform.position.y,0)).y;
		viewPos = new Vector2(unitX,unitY);
	}


}
