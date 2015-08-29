using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
		private GameObject _runner;
		private GameObject _creater;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void setRunner(GameObject runner,GameObject creater)
	{
		_runner = runner;
		_creater = creater;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//check hit gameobject
		if (other.gameObject != _runner)
		{
			return;
		}
		else
		{
		}
	}
}
