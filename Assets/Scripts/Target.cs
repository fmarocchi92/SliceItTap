using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public Sprite asteriskSprite;
	public Sprite circleSprite;
	public Sprite pentagramSprite;
	public Sprite spiralSprite;
	public Sprite squareSprite;
	public Sprite starSprite;
	public Sprite triangleSprite;

	Symbol symbol;
	public Symbol TargetSymbol {
		get {
			return symbol;
		}
		set {
			if (value != null) {
				switch (value.symbol) {
				case SectorSymbol.ASTERISK:
					spriteRenderer.sprite = asteriskSprite;
					break;
				case SectorSymbol.CIRCLE:
					spriteRenderer.sprite = circleSprite;
					break;
				case SectorSymbol.PENTAGRAM:
					spriteRenderer.sprite = pentagramSprite;
					break;
				case SectorSymbol.SPIRAL:
					spriteRenderer.sprite = spiralSprite;
					break;
				case SectorSymbol.SQUARE:
					spriteRenderer.sprite = squareSprite;
					break;
				case SectorSymbol.STAR:
					spriteRenderer.sprite = starSprite;
					break;
				case SectorSymbol.TRIANGLE:
					spriteRenderer.sprite = triangleSprite;
					break;
				}
				switch (value.color) {
				case SectorColor.BLUE:
					spriteRenderer.color = Color.blue;
					break;
				case SectorColor.WHITE:
					spriteRenderer.color = Color.white;
					break;
				case SectorColor.GREEN:
					spriteRenderer.color = Color.green;
					break;
				case SectorColor.RED:
					spriteRenderer.color = Color.red;
					break;
				case SectorColor.YELLOW:
					spriteRenderer.color = Color.yellow;
					break;
				}
			}
			symbol = value;
		}
	}

//	SectorSymbol targetSymbol;
//
//	public SectorSymbol TargetSymbol { 
//		get {
//			return targetSymbol;
//		}
//		set { 
//			switch (value) {
//			case SectorSymbol.ASTERISK:
//				spriteRenderer.sprite = asteriskSprite;
//				break;
//			case SectorSymbol.CIRCLE:
//				spriteRenderer.sprite = circleSprite;
//				break;
//			case SectorSymbol.PENTAGRAM:
//				spriteRenderer.sprite = pentagramSprite;
//				break;
//			case SectorSymbol.SPIRAL:
//				spriteRenderer.sprite = spiralSprite;
//				break;
//			case SectorSymbol.SQUARE:
//				spriteRenderer.sprite = squareSprite;
//				break;
//			case SectorSymbol.STAR:
//				spriteRenderer.sprite = starSprite;
//				break;
//			case SectorSymbol.TRIANGLE:
//				spriteRenderer.sprite = triangleSprite;
//				break;
//			}
//			targetSymbol = value;
//		} 
//	}
//
//	SectorColor targetColor;
//	public SectorColor TargetColor { 
//		get {
//			return targetColor;
//		}
//		set {
//			switch (value) {
//			case SectorColor.BLUE:
//				spriteRenderer.color = Color.blue;
//				break;
//			case SectorColor.WHITE:
//				spriteRenderer.color = Color.white;
//				break;
//			case SectorColor.GREEN:
//				spriteRenderer.color = Color.green;
//				break;
//			case SectorColor.RED:
//				spriteRenderer.color = Color.red;
//				break;
//			case SectorColor.YELLOW:
//				spriteRenderer.color = Color.yellow;
//				break;
//			}
//			targetColor = value;
//		} 
//	}

	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool Equals(object other){
		Target s = other as Target;
		return s.symbol.Equals (symbol);
	}
}

public class Symbol{
	public SectorColor color;
	public SectorSymbol symbol;

	public override bool Equals(object other){
		Symbol s = other as Symbol;
		return s!=null && s.color.Equals (color) && s.symbol.Equals (symbol);
	}

	public override int GetHashCode(){
		return (int)color * 10000 + (int)symbol;
	}
}
