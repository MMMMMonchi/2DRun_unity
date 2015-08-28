using UnityEngine;
using System.Collections;
using System.IO;

public class GroundGreator : MonoBehaviour {

		public int WORLD_NUM = 1;


		public AudioClip endingBgm;
		public GameObject happyWedding;
		public GameObject fireWorksPrefab;

		public const float initPositionX=300;
		public const float initPositionY=0;
		public const float hidePositionX=-60;
		public const float speed=7f;

		public int interval=1;
		public int enemyInterval=1;

		private int currentCount=0;
		private int currentCountForObejct=0;

		//game contents
		public GameObject groundPrefab;
		public GameObject enemy;

		//Scroll
		public GameObject gameScrollerPrefab;
		private GameObject _gameScroller;

		//runner
		public GameObject runnerPrefab;
		private GameObject _runner;
		// Use this for initialization


		public GameObject _kuppaPrefab;
		public GameObject _dokanPrefab;
		public GameObject _blockPrefab;

		public GameObject _flagPrefab;
		private GameObject _flagInstance;
		public GameObject _castlePrefab;

		public GameObject peachPrefab;
		private GameObject _peachInstance;
		public GameObject peachCastlePrefab;

		private bool _isStart=false;
		private bool _isEnd=false;
		private int ENEMY_NUM=0;
		private int _createrPosIndex = 0;
		private int _currentDataIndex = 0;
		private int[] _enemyData =    {
				4,  1,2, 4, 1, 3,2, 1,3,2,1,2,4,2,1
		};
	
		private const int DOWN = 1;
		private const int UP = 2;

		private const int DOKAN = 3;
		private const int BLOCK = 4;

		private const int OFF = 0;
		private const int END = 2;//終了アニメ処理
		private const int NON = -1;

		private const int INTERVAL = 1850;

		private int _currentEnemyIndex = 0;


		private const int ENEMY_SUM_NUM=63;

		private string[] _files;

		private int blockInterval;
		private float blockSpeed;
		private int objectInterval;
	

	void Start () {

				//_files = System.IO.Directory.GetFiles ("Assets/Resources/", "*.prefab", System.IO.SearchOption.AllDirectories);

				if (WORLD_NUM == 1) {
						_currentEnemyIndex = 1;
						ENEMY_NUM=ENEMY_SUM_NUM/2;
				} else {
						_currentEnemyIndex = ENEMY_SUM_NUM/2;
						ENEMY_NUM = ENEMY_SUM_NUM+1;
				}

		//保存されているPrefabのファイル名を取得
		/*Debug.Log (Application.dataPath + "==dataPathだよ");
		string fileStorePath = "Assets/Resources";

		string[] paths = Directory.GetFiles (fileStorePath, "*.prefab");
		for (int i = 0; i < paths.Length; i++) {
			Debug.Log (paths[i]+"=="+i.ToString()+"番目のパス");
			paths [i]=paths [i].Replace (fileStorePath + "/","");
			paths [i]=paths [i].Replace (".prefab", "");
			Debug.Log (paths[i]+"=="+i.ToString()+"番目のパス　修正後");
		}*/


				startGame ();
				_isStart=true;

	}


	private void startGame(){
			_gameScroller = Instantiate (this.gameScrollerPrefab)as GameObject;
			
				_runner = Instantiate (this.runnerPrefab)as GameObject;
			
				create (this.groundPrefab, -30);

				for (int i = 0; i < 8; i++) {

						if(i==1)createEnemy (DOWN,80);
						block=create (this.groundPrefab, i * 48);
						_createrPosIndex++;
				}
	}
	
		private GameObject block;
		private GameObject enemyTemp;

	// Update is called once per frame
	void Update () {

				if(_isEnd==false)checkGoal ();

			if (_isStart == false) {
				// ゲーム中ではなく、マウスクリックされたらtrueを返す。
				if (Input.GetMouseButtonDown (0)) {
					startGame ();
					_isStart=true;
				}

			} else {

						blockInterval = (int)(interval * Time.deltaTime);
						blockSpeed=speed*Time.deltaTime;
						objectInterval = (int)(INTERVAL * Time.deltaTime);
				
						if (_isEnd == false) {
								Vector2 trans = _gameScroller.transform.localPosition;
								trans.x -=blockSpeed;
								_gameScroller.transform.localPosition = trans;
				
								currentCount++;
								currentCountForObejct++;

								//Debug.Log (block.transform.position.x + "  is objectinte");

								if (block!=null&&block.transform.position.x<252) {
										currentCount = 0;
										block=create (this.groundPrefab);
										//createObject ();
								}



								if (_currentEnemyIndex >= ENEMY_NUM) {
								
										createGoal ();
								}


								System.Random random = new System.Random();
								int dice = random.Next (25);
								if (enemyTemp!=null&&enemyTemp.transform.position.x < 40+dice) {
										Debug.Log ("create obj");
										currentCountForObejct = 0;
										createObject ();
										System.Random random2 = new System.Random();
										int dice2 = random2.Next (5);
										Debug.Log (dice2 + "sss");
										if (dice2 == 4) {
												createObject ();
										}
								}


						} else {
								if (Input.GetMouseButtonDown (0)&&WORLD_NUM==1) {
										Application.LoadLevel ("ToWorld2Scene");
								}
						}
						/*if (currentCount % enemyInterval == 0) {
				
					if (_currentEnemyIndex >= _enemyPrefabNames.Length) {
						_currentEnemyIndex = 0;
					}

					Debug.Log (_enemyPrefabNames [_currentEnemyIndex]);
					GameObject enemyPrefab = (GameObject)Resources.Load (_enemyPrefabNames [_currentEnemyIndex]);
					enemyPrefab.SetActive (true);
					EnemySample contents=create(enemyPrefab,initPositionX,5).GetComponent<EnemySample>();
					contents.setRunner(_runner);

					_currentEnemyIndex++;
				}	*/
			}



	}

	GameObject create(GameObject prefab,float initPostX=initPositionX,float initPosY=initPositionY,float hidePosX=hidePositionX){
			//scroll position x
			Vector2 trans = _gameScroller.transform.localPosition;

			GameObject ground1=Instantiate(prefab) as GameObject;
			Ground groundComponent = ground1.GetComponent<Ground> ();
			ground1.transform.parent = _gameScroller.transform;
			groundComponent.Institate (initPostX-trans.x, initPosY,hidePosX);
			return ground1;
	}

		//地面、敵などを一定の間隔で作成するメソッド
		void createObject(){

				int currentEnemyNum = _enemyData [_currentDataIndex % _enemyData.Length];

				//現在のインデックスのところがONになっている場合は敵を生成する。
				if (currentEnemyNum == UP||currentEnemyNum==DOWN) {
						createEnemy (currentEnemyNum,80);
				}


				//敵がすべて出終わっている場合は、ブロックや土管の精製処理も止める
				if (_currentEnemyIndex >= ENEMY_NUM) {
						return;
				}


				if (currentEnemyNum == DOKAN) {
						enemyTemp=create (_dokanPrefab,80,3);
				}
				if (currentEnemyNum == BLOCK) {
						enemyTemp=create (_blockPrefab,80,7);
				}

				_currentDataIndex++;
		}

		private bool _isEndCreating=false;

		void createEnemy(float enemyInfoId,int positionX=0){

				float positionY = 0;
				if (enemyInfoId == DOWN) {
						positionY=8f;
				} else if (enemyInfoId == UP) {
						positionY=10f;
				}
				if(_currentEnemyIndex<ENEMY_NUM) {
						Debug.Log (_currentEnemyIndex );
						GameObject enemyPrefab = (GameObject)Resources.Load (_currentEnemyIndex.ToString());
						enemyPrefab.SetActive (true);
						GameObject enemyIns = create (enemyPrefab, positionX, positionY);
						EnemySample contents=enemyIns.GetComponent<EnemySample>();
						contents.setRunner(_runner);
						enemyTemp = enemyIns;

				}
				_currentEnemyIndex++;
		}

		void createGoal(){

				if (_isEndCreating==false) {

						_isEndCreating = true;

						//indexを0に戻すのではなく・・・・
						//_currentEnemyIndex = 0;


						if (WORLD_NUM == 1) {
								create (groundPrefab,160);
								_flagInstance = create (_flagPrefab, 160, 9);
								create (_castlePrefab, 170, 7);

						} else {

								//クッパ生成
								EnemySample contents=create(_kuppaPrefab,170,7).GetComponent<EnemySample>();
								create (groundPrefab,160);
								contents.setRunner(_runner);


								create (groundPrefab,150);
								create (groundPrefab,210);	
								create (groundPrefab,270);

								_peachInstance=create (peachPrefab, 222, 4.3f);
								create (peachCastlePrefab, 220, 18);
						}

				} 
				_currentEnemyIndex++;
		}


		void checkGoal(){

				if (WORLD_NUM == 1) {

						if (_runner == null || _flagInstance == null) {
								return;
						}
	
						Vector2 runnerPos = _runner.transform.position;
						Vector2 goalPos = _flagInstance.transform.position;

						if (goalPos.x - runnerPos.x < 3.5) {
								//Destroy (_runner.gameObject);
								//Destroy (_flagInstance.gameObject);
								_isEnd = true;
						}
			
				} else {

						if (_runner == null || _peachInstance == null) {
								return;
						}

						Vector2 runnerPos = _runner.transform.position;
						Vector2 peachPos = _peachInstance.transform.position;
						Debug.Log (runnerPos + "runnn");
						Debug.Log (peachPos + "goalpos");

						if (peachPos.x - runnerPos.x < 6.5) {
								//Destroy (_runner.gameObject);
								//Destroy (_flagInstance.gameObject);
								_isEnd = true;

								Animator runnerAnimator = _runner.GetComponent<Animator> ();
								runnerAnimator.SetBool ("isStopping", true);

								Animator peachAnimator = _peachInstance.transform.Find ("dist_peach_0").GetComponent<Animator> ();
								peachAnimator.SetBool ("isStopping", true);

								audio.Stop ();
								audio.PlayOneShot (endingBgm);

								Instantiate(happyWedding);
								Instantiate(fireWorksPrefab);
						}
				}
		}
}
