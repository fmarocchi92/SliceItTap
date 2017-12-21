using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorController : MonoBehaviour {

	private int hitCount;
	GameManager gameManager;
	public int hitToSplit = 3;
	public SectorType type = SectorType.FULL;
	[HideInInspector] public Target target;
	public const float halfAngle=22.5f;
	public const float fullAngle=45f;
	public const float thirdAngle=15f;
	public const float quarterAngle=11.25f;

	void Awake () {
		target = GetComponentInChildren<Target> ();
	}

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
//		color = (SectorColor)Random.Range (0, 5);
//		switch (color) {
//		case SectorColor.BLUE:
//			GetComponent<SpriteRenderer> ().color = Color.blue;
//			break;
//		case SectorColor.WHITE:
//			GetComponent<SpriteRenderer> ().color = Color.white;
//			break;
//		case SectorColor.GREEN:
//			GetComponent<SpriteRenderer> ().color = Color.green;
//			break;
//		case SectorColor.RED:
//			GetComponent<SpriteRenderer> ().color = Color.red;
//			break;
//		case SectorColor.YELLOW:
//			GetComponent<SpriteRenderer> ().color = Color.yellow;
//			break;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager == null) {
			gameManager = GameManager.instance;
		}
	}

	public float Angle {
		get {
			float angle = transform.eulerAngles.z;
			switch (type) {
			case SectorType.FULL:
				return angle + 45f;
			case SectorType.HALF:
				return angle + 22.5f;
			case SectorType.QUARTER:
				return angle + 11.25f;
			case SectorType.THIRD:
				return angle + 15f;
			}
			return 0;
		}
	}

	public void Hit(){
		transform.localScale = transform.localScale * 1.02f;
		hitCount++;
		if (hitCount >= hitToSplit) {

			if (type != SectorType.QUARTER && type != SectorType.THIRD) {
				gameManager.Split (this);
			}
		}
	}
}


public enum SectorType{
	FULL,
	HALF,
	THIRD,
	QUARTER,
	COUNT
}

public enum SectorSymbol{
	SQUARE,
	CIRCLE,
	TRIANGLE,
	STAR,
	SPIRAL,
	PENTAGRAM,
	ASTERISK,
	COUNT
}

public enum SectorColor{
	WHITE,
	RED,
	YELLOW,
	BLUE,
	GREEN,
	COUNT
}