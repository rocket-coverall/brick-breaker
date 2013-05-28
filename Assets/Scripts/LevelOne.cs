using UnityEngine;
using System.Collections;

public class LevelOne : Level
{
	
	public GameObject cannon;
	public Transform extraBallPreFab;
	public Transform prefabBrick;
	public Transform powerUpPreFab;
	
	//dimensions of brick will be defined by BrickArea dimensions and their amount and gaps
	public int brickRows = 2;
	public int brickColumns = 6;
	
	public float brickWidthGap = 0f;
	public float brickHeightGap = 0f;
	
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
		
				
		GameObject brickArea = GameObject.Find("BrickArea");
		
		float brickWidth  = ( brickArea.renderer.bounds.size.x - brickWidthGap * (brickColumns - 1) ) / brickColumns;
		print (brickWidth);
		
		float brickHeight = ( brickArea.renderer.bounds.size.z - brickHeightGap * (brickRows - 1) ) / brickRows;
		print (brickHeight);
		
		Vector3 bricksStart = new Vector3();
		
		bricksStart = brickArea.transform.position -  brickArea.renderer.bounds.size/2;
		bricksStart.z = brickArea.transform.position.z +  brickArea.renderer.bounds.size.z/2;
		
		bricksStart.x += brickWidth / 2;
		bricksStart.z -= brickHeight / 2;
		
		print (bricksStart);
		
		
		int index = 0;
		float x = bricksStart.x, z = bricksStart.z;
		
		for(int i = 0; i < brickRows; i++)
		{
			for(int j = 0; j < brickColumns; j++)
			{
				float yPos = 0;
				
				bricks[index] = (Transform)Instantiate(prefabBrick, new Vector3(x, yPos, z), new Quaternion(0,180,0,0));
				bricks[index].localScale = new Vector3(brickWidth, bricks[index].localScale.y, brickHeight);
				index++;

				x +=  brickWidth + brickWidthGap;
			}
			x = bricksStart.x;// reset x bricks start to initial one

			z -= brickHeight + brickHeightGap;
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
