using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject arrow;
	public List<SectorController> sectors;

	public GameObject halfPrefab;
	public GameObject thirdPrefab;
	public GameObject quarterPrefab;
	public TargetController targetController;

	[HideInInspector] public List<Symbol> usedSymbols = new List<Symbol>();
	[HideInInspector] public static GameManager instance;
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (instance);
			instance = this;
		}
	}

	void Start () {
		foreach (SectorController sector in sectors) {
			AssignSymbol (sector);
		}
		targetController.UpdateTarget (usedSymbols [Random.Range (0, usedSymbols.Count)]);
		targetController.UpdateTarget (usedSymbols [Random.Range (0, usedSymbols.Count)]);
	}

	void AssignSymbol( SectorController sector){
		Symbol newSymbol = new Symbol ();
		newSymbol.color = (SectorColor)Random.Range (0, (float)SectorColor.COUNT);
		newSymbol.symbol = (SectorSymbol)Random.Range (0, (float)SectorSymbol.COUNT);
		while (usedSymbols.Contains (newSymbol)) {
			newSymbol.color = (SectorColor)Random.Range (0, (float)SectorColor.COUNT);
			newSymbol.symbol = (SectorSymbol)Random.Range (0, (float)SectorSymbol.COUNT);
		}
		sector.target.TargetSymbol = newSymbol;
		usedSymbols.Add (newSymbol);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public SectorController GetClosestSector(float zAngle){
		float minAngle = float.MaxValue;
		SectorController closestSector = null;
		foreach (SectorController sector in sectors) {
			if (Mathf.Abs (sector.Angle - zAngle) < minAngle) {
				minAngle = Mathf.Abs (sector.Angle - zAngle);
				closestSector = sector;
			}
		}
		return closestSector;
	}

	public void HitClosestSector(float zAngle){
		float minAngle = float.MaxValue;
		SectorController closestSector = null;
		foreach (SectorController sector in sectors) {
			if (Mathf.Abs (sector.Angle - zAngle) < minAngle) {
				minAngle = Mathf.Abs (sector.Angle - zAngle);
				closestSector = sector;
			}
		}
		if (closestSector.target.Equals(targetController.currentTarget)) {
			closestSector.Hit ();
			Symbol nextTarget = usedSymbols [Random.Range (0, usedSymbols.Count)];
			while(nextTarget.Equals(closestSector.target.TargetSymbol)){
				nextTarget = usedSymbols [Random.Range (0, usedSymbols.Count)];
			}
			targetController.UpdateTarget (nextTarget);
		} else {
			HitError ();
		}
	}

	public void HitError (){

	}

	public void Split(SectorController sector){
		sectors.Remove (sector);
		switch (sector.type) {
		case SectorType.FULL:
			if (Random.value > 0.5) {
//				SpawnThird (sector.Angle);
				SpawnSector (3,thirdPrefab,sector.Angle,SectorController.fullAngle,SectorController.thirdAngle);
			} else {
//				SpawnHalf (sector.Angle);
				SpawnSector (2,halfPrefab,sector.Angle,SectorController.fullAngle,SectorController.halfAngle);
			}
			break;
		case SectorType.HALF:
//			SpawnQuarter (sector.Angle);
			SpawnSector (2,quarterPrefab,sector.Angle,SectorController.halfAngle,SectorController.quarterAngle);
			break;
		case SectorType.QUARTER:
			break;
		case SectorType.THIRD:
//			SpawnQuarter (sector.Angle);
			break;
		}
//		Symbol toRemove = new Symbol { color = sector.target.TargetColor, symbol = sector.target.TargetSymbol };
		usedSymbols.Remove (sector.target.TargetSymbol);
		GameObject.Destroy (sector.gameObject);
	}

	void SpawnHalf(float zAngle){
		GameObject firstHalf = GameObject.Instantiate (halfPrefab);
		firstHalf.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle-SectorController.fullAngle)));
		GameObject secondHalf = GameObject.Instantiate (halfPrefab);
		secondHalf.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle)));
		sectors.Add (firstHalf.GetComponent<SectorController> ());
		sectors.Add (secondHalf.GetComponent<SectorController> ());
	}

	void SpawnThird(float zAngle){
		GameObject firstThird = GameObject.Instantiate (thirdPrefab);
		firstThird.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle-SectorController.fullAngle)));
		GameObject secondThird = GameObject.Instantiate (thirdPrefab);
		secondThird.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle-SectorController.fullAngle+2*SectorController.thirdAngle)));
		GameObject thirdThird = GameObject.Instantiate (thirdPrefab);
		thirdThird.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle-SectorController.fullAngle+4*SectorController.thirdAngle)));
		sectors.Add (firstThird.GetComponent<SectorController> ());
		sectors.Add (secondThird.GetComponent<SectorController> ());
		sectors.Add (thirdThird.GetComponent<SectorController> ());
	}

	void SpawnQuarter(float zAngle){
		GameObject firstQuarter = GameObject.Instantiate (quarterPrefab);
		firstQuarter.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle-SectorController.halfAngle)));
		GameObject secondQuarter = GameObject.Instantiate (quarterPrefab);
		secondQuarter.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,zAngle)));
		sectors.Add (firstQuarter.GetComponent<SectorController> ());
		sectors.Add (secondQuarter.GetComponent<SectorController> ());
	}

	void SpawnSector(int sectorsCount, GameObject sectorPrefab, float zAngle, float sectorAngle, float newAngle){
		float angle = zAngle - sectorAngle;
		for (int i = 0; i < sectorsCount; i++) {
			GameObject sector = GameObject.Instantiate (sectorPrefab);
			sector.transform.SetPositionAndRotation(Vector3.zero,Quaternion.Euler(new Vector3(0,0,angle)));
			angle += 2*newAngle;
			sectors.Add (sector.GetComponent<SectorController> ());
			AssignSymbol (sector.GetComponent<SectorController>());
		}
	}
}
