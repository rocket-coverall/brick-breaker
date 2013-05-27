using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour 
{
	public AudioClip shoot;
	public GameObject fireBall;
	public GameObject fireParticle;
	
	float speed = 50;
	int shotCount;
	Transform spawn;
	
	Level level;

	// Use this for initialization
	void Start () 
	{
		spawn = transform.Find("Spawn"); 
		shotCount = 0;	
		level = GameObject.Find("LevelMaker").GetComponent<Level>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(gameObject.activeSelf && shotCount !=5)
		{
            if(Input.GetMouseButtonDown(0) && !gameObject.animation.isPlaying)
			{
				audio.PlayOneShot(shoot, 0.5f);
				shotCount++;
				gameObject.animation.Play("CannonAnimation");
				Instantiate(fireParticle,spawn.position,Quaternion.identity);
              	GameObject ball =  (GameObject)Instantiate(fireBall, spawn.position, Quaternion.identity);
                ball.rigidbody.AddForce(spawn.forward*speed, ForceMode.Impulse);
				
            }
			if (shotCount == 5)
			{
	            level.SetPowerUp(false);
				gameObject.SetActive(false);
	            shotCount =0;  
        	}
		}
	}		
}
