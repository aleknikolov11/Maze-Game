using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	public static MazeGenerator instance;
	public GameObject CellType1;
	public GameObject CellType2;
	public GameObject CellType3;
	public GameObject CellType4;
	public GameObject CellType5;
	private GameObject CurrentCell;
	Vector3 lastPos;	 

	void Awake(){
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void mazeGenerator(int[,] n, int[,] m, int k) {


		for(int i = 0; i < k; i++){
			for(int j = 0; j < k; j++){
				if (n [i, j] == 1) {
					generateType1 (m [i, j], i, j);
				} else if (n [i, j] == 2) {
					generateType2 (m[i,j], i, j);
				} else if (n [i, j] == 3) {
					generateType3 (m[i,j], i, j);
				} else if(n [i, j] == 4){
					generateType4 (m[i,j], i, j);
				}else if(n [i, j] == 5){
					generateType5 (m[i,j], i, j);
				}
			}
		}
	}

	public void generateType1(int m, int l, int f){
		float typeRotation = (float)m;
		lastPos = CellType1.transform.position;
		Vector3 pos = lastPos;
		pos.x = l;
		pos.z = f;
		lastPos = pos;
		CurrentCell = CellType1;
		Instantiate (CurrentCell, lastPos, Quaternion.Euler (0,typeRotation*90f,0));

	}

	public void generateType2(int m, int l, int f){
		float typeRotation = (float)m;
		lastPos = CellType2.transform.position;
		Vector3 pos = lastPos;
		pos.x = l;
		pos.z = f;
		lastPos = pos;
		CurrentCell = CellType2;
		Instantiate (CurrentCell, lastPos, Quaternion.Euler (0,typeRotation*90f,0));
	}

	public void generateType3(int m, int l, int f){
		float typeRotation = (float)m;
		lastPos = CellType3.transform.position;
		Vector3 pos = lastPos;
		pos.x = l;
		pos.z = f;
		lastPos = pos;
		CurrentCell = CellType3;
		Instantiate (CurrentCell, lastPos, Quaternion.Euler (0,typeRotation*90f,0));
	}

	public void generateType4(int m, int l, int f){
		float typeRotation = (float)m;
		lastPos = CellType4.transform.position;
		Vector3 pos = lastPos;
		pos.x = l;
		pos.z = f;
		lastPos = pos;
		CurrentCell = CellType4;
		Instantiate (CurrentCell, lastPos, Quaternion.Euler (0,typeRotation*90f,0));
	}

	public void generateType5(int m, int l, int f){
		float typeRotation = (float)m;
		lastPos = CellType5.transform.position;
		Vector3 pos = lastPos;
		pos.x = l;
		pos.z = f;
		lastPos = pos;
		CurrentCell = CellType5;
		Instantiate (CurrentCell, lastPos, Quaternion.Euler (0,typeRotation*90f,0));
	}

}
