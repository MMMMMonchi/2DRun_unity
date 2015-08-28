using UnityEngine;
using System.Collections;
using System.IO;

public class EnemySample : MonoBehaviour {

	// Use this for initialization
	
		public AudioClip destroySE;
	private GameObject _runner;
	void Start () {
	
	}

	public void setRunner(GameObject runner){
		_runner = runner;
	}

	// Update is called once per frame
	void Update () {
				//自分自身を消滅させる処理（暫定）
				Vector2 localTrans = this.transform.localPosition;
				if (localTrans.y<-10) {
						initialise ();
				}
	}
		private int count=0;
	void OnCollisionEnter2D(Collision2D other){
				count++;
				Debug.Log (count + "回目の呼び出し"+other.gameObject.name);
		if (other.gameObject != _runner) {
			return;
		}
		
		Animator animator = _runner.GetComponent<Animator> ();
		
		Jump jumpComponent = _runner.GetComponent<Jump> ();

		//BoxColiderを取得する
		BoxCollider2D boxColider = this.gameObject.GetComponent<BoxCollider2D>();

		if (jumpComponent.isFalling()) {
						audio.PlayOneShot (destroySE);
						SpriteRenderer spRender = this.GetComponent<SpriteRenderer> ();
						spRender.enabled = false;
						foreach (Transform child in transform){
								SpriteRenderer spChildRender = child.GetComponent<SpriteRenderer> ();
								spChildRender.enabled = false;

								//BoxCollider boxCol=child.GetComponent<BoxCollider>();
								//boxCol.enabled=false;
						}
						boxColider.enabled = false;
						Invoke ("delete", 3);
		} else {
			jumpComponent.initialise ();
		}
	}

		void delete(){
				Destroy (this.gameObject);	
		}

		//オブジェクトの配置を初期に戻す際の処理
		 void initialise(){
				Vector2 localTrans = this.transform.localPosition;
				localTrans.y = 11f;
				localTrans.x -= 5f;
				this.transform.localPosition = localTrans;
		}
}
