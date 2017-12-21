using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	public Target currentTarget;
	public Target nextTarget;
	// Use this for initialization
	void Start () {
//		Symbol initialTarget = GameManager.instance.usedSymbols [Random.Range (0,GameManager.instance.usedSymbols.Count)];
//		Symbol initialNextTarget = GameManager.instance.usedSymbols [Random.Range (0,GameManager.instance.usedSymbols.Count)];
//		UpdateTarget (initialTarget);
//		UpdateTarget (initialNextTarget);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateTarget(Symbol newNextTarget){//TODO insert animation of changing target
		currentTarget.TargetSymbol = nextTarget.TargetSymbol;
		nextTarget.TargetSymbol = newNextTarget;
	}
}
