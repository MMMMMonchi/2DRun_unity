using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {


	public AudioClip jumpSE;
	public AudioClip deadSE;
	public AudioClip destroySE;
	public LayerMask GraundLayer;

	private Animator _animator;
	private float _jump = 3f;
	private int _jumpFrame=20;
	private int _frameCount=20;
	private bool _facingRight = true;
	private bool _grounded=false;
	private bool _initEnd = true;//オブジェクトの配置を初期に戻す際の処理

	void Start ()
	{
		_animator = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_grounded = Physics2D.Linecast
		(
			transform.position + transform.up * 10f,
			transform.position - transform.up * 10f,
			GraundLayer
		);
						
		//アニメーションの処理
		//地面に接地している場合には、通常のランニングアニメーション
		//落下している場合にはFallingアニメーション 速度が下向きかどうかで判定
		if (this.gameObject.rigidbody2D.velocity.y<0)
		{
			_animator.SetBool ("isFalling", true);
		} else {
			_animator.SetBool ("isFalling", false);
		}

		//自分自身を消滅させる処理（暫定）
		Vector2 localTrans = this.transform.localPosition;

		if (localTrans.y<-13)
		{
			initialise ();
			return;
		}
		if (localTrans.x < -30f)
		{
			initialise ();
			return;
		}

		if (Input.GetMouseButtonDown (0)&&_grounded&&_animator.GetBool("isFalling")==false)
		{
			excuteJump();
		}
	}

	void FixedUpdate()
	{

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		//右を向いていて、左の入力があったとき、もしくは左を向いていて、右の入力があったとき
		if((h > 0 && !_facingRight) || (h < 0 && _facingRight))
		{
			//右を向いているかどうかを、入力方向をみて決める
			_facingRight = (h > 0);
			//localScale.xを、右を向いているかどうかで更新する
			transform.localScale = new Vector3((_facingRight ? 1 : -1), 1, 1);
		}
	}

	void excuteJump()
	{
		audio.PlayOneShot (jumpSE);
		this.gameObject.rigidbody2D.AddForce (Vector2.up * 1000);
	}

	void setPos()
	{

		SpriteRenderer spRender = this.GetComponent<SpriteRenderer> ();
		spRender.enabled = true;

		foreach (Transform child in transform)
		{
			SpriteRenderer spChildRender = child.GetComponent<SpriteRenderer> ();
			spChildRender.enabled = true;
		}

		this.gameObject.SetActive (false);
		this.gameObject.SetActive (true);
		Vector2 localTrans = this.transform.localPosition;
		localTrans.y = 21f;
		localTrans.x = -1f;
		this.transform.localPosition = localTrans;

		_initEnd = true;
	}

	public void initialise()
	{
		if (_initEnd == false)return;
		
		_initEnd = false;
		
		SpriteRenderer spRender = this.GetComponent<SpriteRenderer> ();
		spRender.enabled = false;
		
		foreach (Transform child in transform)
		{
			SpriteRenderer spChildRender = child.GetComponent<SpriteRenderer> ();
			spChildRender.enabled = false;
		}
		
		audio.PlayOneShot (deadSE);
		Invoke ("setPos", 1);
	}
	
	public void callAttackSE()
	{
		audio.PlayOneShot (destroySE);
	}

	public bool isFalling()
	{
		return _animator.GetBool ("isFalling");
	}
}
