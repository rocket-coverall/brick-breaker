using UnityEngine;
using System.Collections;

public class LevelOne : Level
{
	
	public GameObject cannon;
	public Transform extraBallPreFab;
	public Transform prefabBrick;
	public Transform powerUpPreFab;
	
	AudioClip winning;
 	AudioClip losing;
 	bool won;	
	Transform[] bricks = new Transform[36];
	PadScript padScript;
	SphereScript sphereScript;
	
	
	void Start () 
	{
		padScript = GameObject.Find("Pad").GetComponent<PadScript>();
		sphereScript = GameObject.Find("Sphere").GetComponent<SphereScript>();
		cannon.SetActive(false);
		
		int index = 0;
		int x = 0, z = 0;
		
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < 12; j++)
			{
				float yPos = 0;
				bricks[index] = (Transform)Instantiate(prefabBrick, new Vector3(x, yPos, z), new Quaternion(0,180,0,0));
				index++;
				x += 3;
			}
			x=0;
			z += -2;
		}
		_brickCounter = bricks.Length;
		_count = 0;
	}
	
	//PowerUp is called in PadScript, in the OnTriggerEnter function
	public override void PowerUp(Vector3 position)
	{
		int choice = Random.Range (0,3);
		switch(choice)
		{			
            case 0: 
            Instantiate(extraBallPreFab,position, Quaternion.identity); 
            break;
			
            case 1:
            cannon.SetActive(true);
            break;
			
			case 2:
			print ("tweaking");
            padScript.tweak = true;
            break;  
        }
	}
	
	public override void HitBrick(Vector3 position){
		_brickCounter--;
		_count++;
			
		if(_count == 4)
		{
			Instantiate(powerUpPreFab, position , Quaternion.identity);
			_count = 0;
		}
	}
	
	public override void EndLevel(GameObject other)
	{
	    EndGame();
	    other.audio.Stop();
	    other.audio.loop=false;
	    if(won)
		{
	        other.audio.clip = winning;
	        other.audio.Play();
	        other.animation.Play("ChestTopAnim");
	    }
		else
		{ 
	        other.audio.clip = losing;
	        other.audio.Play();
	    } 
	}

	public override void EndGame()
	{
	    gameObject.renderer.enabled = false;
	    sphereScript.BallStuck();
	}
	
}
