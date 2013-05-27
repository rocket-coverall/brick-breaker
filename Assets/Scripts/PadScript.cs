using UnityEngine;
using System.Collections;

public class PadScript : MonoBehaviour 
{

	public float speed = 0.1F;
	public float minPos;
	public float maxPos;
	
	public bool tweak;
	public float tweakTime;
	
	float amplitude = 1.5f;
	float omega = 6;
	float startPos;
	
	static int mouseSensitivity;
	
	Camera gameView;
	Level level;
	Transform _transform;
	
	void Start()
	{
	    gameView = (Camera)FindObjectOfType(typeof(Camera));
		level = GameObject.Find("LevelMaker").GetComponent<Level>();
		mouseSensitivity = 2;
		_transform = transform;
		tweak = false;
    	startPos = transform.position.z;
	}
	
	void Update () 
	{ 
	    RaycastHit hit;
		#if UNITY_EDITOR
		
		 if(Physics.Raycast(gameView.ScreenPointToRay(new Vector3(Input.mousePosition.x, Screen.height / (mouseSensitivity+1),0)), out hit))
			{
				_transform.position = new Vector3(Mathf.Clamp(hit.point.x, minPos, maxPos), _transform.position.y, _transform.position.z);
			}

		#endif
		 
		#if UNITY_ANDROID
		if(Input.touchCount < 2)
		{
		    if(Physics.Raycast(gameView.ScreenPointToRay(new Vector3(Input.mousePosition.x, Screen.height / (mouseSensitivity+1),0)), out hit))
			{
				_transform.position = new Vector3(Mathf.Clamp(hit.point.x, minPos, maxPos), _transform.position.y, _transform.position.z);
			}
		}
		#endif
		
		if(tweak)
		{
			StartCoroutine(SwingingPad());
			tweak = false; 
    	} 
    	
	}
		
	void OnTriggerEnter(Collider other)
	{	
	    if(other.gameObject.tag == "PowerUp" && !level.GetPowerUp())
		{
			Vector3 pos = transform.Find("Stuck").transform.position;
			level.PowerUp(pos);
			level.SetPowerUp(true);
	        Destroy(other.gameObject);
	    }
		else if(other.gameObject.tag == "PowerUp" && level.GetPowerUp() == true)
		{
			Destroy(other.gameObject);
		}
	}
	
	IEnumerator SwingingPad(){
		float timer = 0;
	    while(timer < tweakTime)
		{   
            Vector3 vec = transform.position;
			vec.z = startPos+amplitude*(-Mathf.Cos(omega*timer)+1);
			_transform.position = vec;
			timer += Time.deltaTime;
			yield return null;
        }
		level.SetPowerUp(false);
		float ratio = 0;
			
		while(ratio < 1){
			Vector3 vec = _transform.position;
			vec.z = Mathf.Lerp (vec.z,startPos,ratio);
			_transform.position = vec;
			ratio += Time.deltaTime;
			yield return null;
		}
	}	
}
