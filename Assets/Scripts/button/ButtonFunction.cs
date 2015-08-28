using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goPlayGameScene(){
		Application.LoadLevel ("CreateScene");
	}

	public void goSettingScene(){
		Application.LoadLevel ("SettingScene");
	}

	public void goOpeningScene(){
		Application.LoadLevel ("OpeningScene");
	}

	public void goCameraScene(){
		Application.LoadLevel ("CameraScene");
	}
}
