using UnityEngine;
using System.Collections;

public abstract class Level : MonoBehaviour {

	protected int _brickCounter;
	protected int _count;
	protected bool _powerUp = false;
	
	public int GetBrickCounter() {
		return _brickCounter;
	}
	
	public int GetCount (){
		return _count;
	}
	
	public void SetCount(int setter){
		_count = setter;
	}
	
	public void SetBrickCounter(int setter){
		_brickCounter = setter;
	}
	
	public void AddCount(int n){
		_count += n;
	}
	
	public void AddBrick(int n){
		_brickCounter += n;
	}
	public void SetPowerUp(bool b){
		_powerUp = b;
	}
	public bool GetPowerUp(){
		return _powerUp;
	}
	public abstract void HitBrick(Vector3 position);
	public abstract void PowerUp(Vector3 position);
	public abstract void EndLevel(GameObject camera);
	public abstract void EndGame();
}
