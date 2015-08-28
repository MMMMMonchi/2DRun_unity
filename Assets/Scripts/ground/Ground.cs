using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	// Use this for initialization

	private float _initPositionX=0;
	private float _initPositionY=0;

	private float _hidePositionX=0;

	private float _speed=0;

	void Start () {

		setPosition (_initPositionX, _initPositionY);
	}

	//this method called at initializing
	public void Institate(float positionX,float positionY,float hidePosX){
		_initPositionX = positionX;
		_initPositionY = positionY;
		_hidePositionX = hidePosX;
	}

	void setPosition(float positionX,float positionY){

		//set ground position
		Vector2 trans = this.transform.localPosition;
		trans.x = positionX;
		trans.y = positionY;
		this.transform.localPosition = trans;
	}

	// Update is called once per frame
	void Update () {
		Vector2 parentTrans = this.transform.parent.localPosition;
		Vector2 localTrans = this.transform.localPosition;
		if (parentTrans.x+localTrans.x<_hidePositionX) {
			Destroy(this.gameObject);	
		}
	}
}
