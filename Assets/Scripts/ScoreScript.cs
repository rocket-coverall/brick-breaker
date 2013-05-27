using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour 
{
	
	int _scoreTemp = 0;
	static int score;
	int _life = 3;
	
	public int scoreTemp {
		get
		{
			return _scoreTemp;
		}
		set
		{ 
				_scoreTemp += value;
		}
	}
	
	public int life {
		get
		{
			return _life;
		}
		set
		{ 
				_life += value;
		}
	}
	
	


	// Use this for initialization
	void Start () 
	{	
		scoreTemp = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
