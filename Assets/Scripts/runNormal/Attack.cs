using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private Animator _animator;
	private bool _isSetted=false;

	public const string ATTCK_ANIMATION_FLAG_NAME="isAttack";

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		// change running animation
		if (Input.GetKeyUp(KeyCode.T))
		{
			_animator.SetBool("isAttack",false);
		}

		//change attack animation
		if (Input.GetKeyDown (KeyCode.T))
		{
			_animator.SetBool("isAttack",true);
		}
	}
}
