using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public float speed;
	
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 temp = transform.position;
		temp.z -= speed * Time.deltaTime;
		transform.position = temp;
	}
	
	void OnTriggerEnter(Collider other)
	{
	    if(other.gameObject.CompareTag("DeathZone"))
		{
	        Destroy(gameObject);
	    }
	}	
}
