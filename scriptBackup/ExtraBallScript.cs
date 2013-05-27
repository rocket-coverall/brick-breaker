using UnityEngine;
using System.Collections;

public class ExtraBallScript : BaseSphere
{
	Transform mainSphere;
	public override void Start() 
	{
		base.Start ();
		mainSphere = GameObject.Find ("Sphere").GetComponent<Transform>();	
		collider.enabled = false;
	}
	
	
	 
	public override void Update() 
	{
		
		#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0) && stuck == true)
		{
			collider.enabled = true;
			Physics.IgnoreCollision(collider,mainSphere.collider);
			level.SetPowerUp(false);
			LaunchSphere();
	    } 
		#endif
		
		
		#if UNITY_ANDROID
		if(Input.touchCount == 2 && stuck == true)
		{
			collider.enabled = true;
			Physics.IgnoreCollision(collider,mainSphere.collider);
			level.SetPowerUp(false);
		}
		#endif
		base.Update();
		MineSweeper();
	}
	
	
	
	public override void OnTriggerEnter(Collider other)
	{
    	base.OnTriggerEnter(other);
		if(other.CompareTag("DeathZone"))
			Destroy (gameObject);
	} 	
}
