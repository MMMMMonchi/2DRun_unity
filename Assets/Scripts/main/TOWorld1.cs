using UnityEngine;
using System.Collections;

public class TOWorld1 : MonoBehaviour {

	public GameObject controller;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			Vector3 newVector = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

			Vector2 tapPoint = new Vector2(newVector.x, newVector.y);
			Collider2D colition2d = Physics2D.OverlapPoint(tapPoint);

			if(colition2d)
			{
				RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
				if(hitObject)
				{
					Debug.Log("hit object is " + hitObject.collider.gameObject.name);

					if (hitObject.collider.gameObject == controller)
					{
						Application.LoadLevel ("CreateScene");
					}
				}
			}
		}
	}
}
