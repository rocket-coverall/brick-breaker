using UnityEngine;
using System.Collections;

public class SphereScript : BaseSphere {
	

	public override void Start () 
	{
		base.Start ();
	}
	
	
	 
	public override void Update () 
	{
		#if UNITY_EDITOR
		    if(Input.GetMouseButtonDown(1) && stuck == true)
			{
		    	LaunchSphere();
	        } 
		#endif	
		
		#if UNITY_ANDROID
			if(Input.touchCount == 2 && stuck == true)
			{
				LaunchSphere();
			}
		#endif
		base.Update ();
		MineSweeper();
	}

	

	public override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
    	if(other.gameObject.name == "DeathZone")
		{ 		
			BallStuck();
    	}
	}
	
	
}
