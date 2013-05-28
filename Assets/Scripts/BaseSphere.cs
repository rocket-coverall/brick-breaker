using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSphere : MonoBehaviour {
	
	public float speed;
	
	public AudioClip wallsound;
	public AudioClip padsound;
	public AudioClip shootsound;
	public AudioClip DeathZonesound;
	public AudioClip Bricksound;
	
	protected Level level;
	protected bool stuck;
	
	Transform _transform;
	Transform stuckPos;
	Vector3 velocity;
	float origSpeed;
	

	// Use this for initialization
	public virtual void Start () 
	{
		stuckPos =GameObject.FindWithTag("Pad").transform.Find("Stuck").transform; 
		level = GameObject.Find ("LevelMaker").GetComponent<Level>();
		_transform = transform;
		origSpeed = speed;
    	BallStuck();
	}
	
	
	// Update is called once per frame
	public virtual void Update () 
	{
		_transform.Translate(velocity * Time.deltaTime);

		RaycastHit hit;
		if (rigidbody.SweepTest (velocity, out hit,0.70f)) {
            SphereCollision(hit);
			
        }	  	
	}
//	void LateUpdate(){
//		//_transform.Translate(velocity * Time.deltaTime);
//	}

// seems to work much worse than SweepTest solution
//	void OnCollisionEnter(Collision other)
//	{
//		print ("Collision with"+other.gameObject.tag);
//		
////		RaycastHit hit;
////		Physics.Raycast(transform.position, velocity, out hit);
////		SphereCollision(hit);
////		if (rigidbody.SweepTest (velocity, out hit,0.70f)) {
////            SphereCollision(hit);
////        }	  
////		
//		print ("Contact points amount " + other.contacts.Length);
//		foreach (ContactPoint p in other.contacts)
//		{
//			Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal, Color.red); 
//		}
//		
//	    if(other.gameObject.CompareTag("Brick"))
//		{
//			SphereBounce(other.contacts[0].normal,Bricksound);
//	        Destroy(other.collider.gameObject);
//			level.HitBrick(transform.position);
//		}
//		else if(other.gameObject.CompareTag("Side"))
//    	{ 
//			SphereBounce(other.contacts[0].normal,wallsound);
//	    }
//		else if(other.gameObject.CompareTag("Pad"))
//		{
//			SphereBounce(other.contacts[0].normal,padsound);
//    	}
//	}
	
	public virtual void OnTriggerEnter(Collider other)
	{
    	if(other.gameObject.name == "DeathZone")
		{ 		
			audio.PlayOneShot(DeathZonesound,0.5f);	
    	}
	} 	
	
	public virtual void BallStuck()
	{
		transform.position = stuckPos.position;
	    transform.parent = stuckPos;
		
		rigidbody.velocity = Vector3.zero; //sometimes ball somehow has a non-zero velocity for rigidbody TODO this sometimes fails with a runtime exception
		velocity = Vector3.zero;
		
	    stuck = true;	
	}
	
	protected void LaunchSphere()
	{
		transform.parent = null;
    	velocity = Vector3.forward;
		speed = origSpeed;
		velocity *= speed;
    	stuck = false;
		
		//rigidbody.velocity = velocity;
		
		audio.PlayOneShot(shootsound,0.5f);
	}
	
	
	
	protected void SphereBounce(Vector3 norm, AudioClip audioClip) // this is called by SphereCollision() method
	{
		velocity = velocity - 2 * norm * Vector3.Dot(velocity, norm);
    	velocity.y = 0;
		velocity.Normalize();
		velocity = velocity * speed;
		audio.PlayOneShot(audioClip,0.5f);
	}
	
	
	void SphereCollision(RaycastHit hit) //this is called in the Update
	{
		if(hit.collider.CompareTag("Side"))
    	{ 
			SphereBounce(hit.normal,wallsound);
	    }
		if(hit.collider.CompareTag("Pad"))
		{
			SphereBounce(hit.normal,padsound);
    	}
		else if(hit.collider.CompareTag("Brick"))
		{
			SphereBounce(hit.normal,Bricksound);
	        Destroy(hit.collider.gameObject);
			level.HitBrick(transform.position);
    	}
	}
	
	protected void MineSweeper()
	{
		Vector3 vel = velocity;
		vel = Quaternion.Euler(0, -90, 0) * vel;
		Quaternion sweeper = Quaternion.Euler(0.0f, 2f, 0f);
		int i;
		List<RaycastHit> list = new List<RaycastHit>();
		
		for(i = 0; i < 90; i++)
		{
			RaycastHit hit;
			if(Physics.Raycast(_transform.position,vel,out hit,0.9f)){
				list.Add (hit);
			}
			vel = sweeper * vel;
			
		}
		if(list.Count!= 0){
			float distance = Mathf.Infinity;
			RaycastHit shortestHit = new RaycastHit();
			foreach(RaycastHit h in list){
				if(h.distance < distance){
					distance = h.distance;
					shortestHit = h;
				}
			}
			if(shortestHit.collider.CompareTag("Side"))
	    	{ 
				SphereBounce(shortestHit.normal,wallsound);
		    }
			if(shortestHit.collider.CompareTag("Pad"))
			{
				SphereBounce(shortestHit.normal,padsound);
	    	}
			else if(shortestHit.collider.CompareTag("Brick"))
			{
				SphereBounce(shortestHit.normal,Bricksound);
		        Destroy(shortestHit.collider.gameObject);
				level.HitBrick(transform.position);
	    	}
		}
	}
}
