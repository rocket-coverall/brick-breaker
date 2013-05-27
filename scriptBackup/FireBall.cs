using UnityEngine;
using System.Collections;

public class FireBall:MonoBehaviour 
{
	void Start(){
		GameObject mainSphere = GameObject.Find("Sphere");
		Physics.IgnoreCollision(gameObject.collider,mainSphere.collider);
	}
	
	void OnCollisionEnter(Collision other)
	{
	    if(other.gameObject.CompareTag("Brick"))
		{
	        Destroy(other.gameObject);
	        Destroy(gameObject);
			//audio.PlayOneShot(Bricksound, 0.5f);
	    }
		else if(other.gameObject.CompareTag("Side"))
		{ 
	        Destroy(gameObject);
			//audio.PlayOneShot(wallsound, 0.5f);
	    }
	   
	}
	
	
}
