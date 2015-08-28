using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.IO;
using System;

public class CameraView : MonoBehaviour {

	// Use this for initialization
		public GameObject clibor;
		public GameObject hanachan;
		public GameObject nokonoko;
		public GameObject patapata;
		public GameObject rex;
		public GameObject yoshi;
		public GameObject teresa;
		public GameObject kameku;
		public GameObject dosun;

	private string[] _files;
	private int _currentIndex;
	private bool _isLoading=false;
	private int _count=0;
	void Start () {

		/*WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices [0].name;
		WebCamTexture texture = new WebCamTexture (deviceName, 300, 400, 15);
		renderer.material.mainTexture = texture;
		texture.Play ();*/

		//Androidのみで機能するためコメントアウト
				_files = System.IO.Directory.GetFiles ("Assets/Resources/photo/", "*.png", System.IO.SearchOption.AllDirectories);
		for (int i = 0; i < _files.Length; i++) {
			Debug.Log (_files [i] + "==" + i.ToString () + "番目");
						_files[i]=_files[i].Replace("Assets/Resources/", "");
						_files[i]=_files[i].Replace(".png", "");
						Debug.Log (_files [i] + "==" + i.ToString () + "番目");
		}

				//load (_files.Length - 1);
	}

	private void load(int index){
		if (_files != null && _files.Length > index&&index>=0&&_isLoading==false) {

			_isLoading = true;
			_currentIndex = index;
			Texture2D tex = new Texture2D (0,0);
			tex.LoadImage (loadBytes (_files[index]));
			renderer.material.mainTexture = tex;

			_isLoading = false;
		}
	}


	byte[] loadBytes(string path){
		FileStream fs = new FileStream (path, FileMode.Open);
		BinaryReader bin = new BinaryReader (fs);
		byte[] result = bin.ReadBytes ((int)bin.BaseStream.Length);
		bin.Close ();
		return result;
	}

	
	// Update is called once per frame
	void Update () {
	
	}


	public void onTapNextButton(){
		load (_currentIndex + 1);
	}

	public void onTapPrevButton(){
				//load (_currentIndex - 1);

				for (int i = 0; i < _files.Length; i++) {

				}
	}
		
	public void onCreateButton(){



				for (int i = 0; i < _files.Length; i++) {


						GameObject prefab;
						System.Random random = new System.Random();
						int dice = random.Next (9);
						if (dice == 1) {
								prefab = clibor;
						} else if (dice == 2) {
								prefab = hanachan;
						} else if (dice == 3) {
								prefab = nokonoko;
						} else if (dice == 4) {
								prefab = patapata;
						} else if (dice == 5) {
								prefab = rex;
						} else if (dice == 6) {
								prefab = yoshi;
						} else if (dice == 7) {
								prefab = teresa;
						}else if (dice == 8) {
								prefab = kameku;
						}else{
								prefab = dosun;
						}



						GameObject newPrefab = Instantiate (prefab)as GameObject;

						//非アクティブにする
						newPrefab.SetActive (false);

						Sprite tex = Resources.Load<Sprite> (_files [i]);
						Debug.Log ("photo/" + _files [i]);

						SpriteRenderer spRender = newPrefab.transform.FindChild ("face").GetComponent<SpriteRenderer> ();
						Debug.Log (spRender + "==dou");
						spRender.sprite = tex;

						_count++;

						newPrefab.name = _count.ToString ();
						string newPrefabName = "Assets/Resources/" + newPrefab.name + ".prefab";

						//Editorのみの機能のためいったんコメントアウト
						#if UNITY_EDITOR
						UnityEngine.Object newPrefab2 = PrefabUtility.CreateEmptyPrefab (newPrefabName);
						PrefabUtility.ReplacePrefab (newPrefab, newPrefab2, ReplacePrefabOptions.ConnectToPrefab);
						#endif
				}
	}

}
