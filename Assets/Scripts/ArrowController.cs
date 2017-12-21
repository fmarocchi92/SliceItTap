using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	public float bpm;
	GameManager gameManager;

	float rotationSpeed;
	float lastAngle=0;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		rotationSpeed = bpm/60 * 360;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager == null) {
			gameManager = GameManager.instance;
		}
		transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
		bool input = Input.GetKeyDown (KeyCode.Space);
		if(input){
			gameManager.HitClosestSector (transform.eulerAngles.z);
		}
		if (lastAngle > transform.rotation.eulerAngles.z) {
			
		}
	}
}
