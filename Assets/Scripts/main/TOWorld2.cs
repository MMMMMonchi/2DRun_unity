using UnityEngine;
using System.Collections;

public class TOWorld2 : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		// ゲーム中ではなく、マウスクリックされたらtrueを返す。
		if (Input.GetMouseButtonDown (0))
		{
			Application.LoadLevel ("KuppaCastleScene");
		}
	}
}
