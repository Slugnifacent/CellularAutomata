using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameObject))]

public class TileUpdate : MonoBehaviour {
	static GameObject Prototype;
	public TileUpdate TopTile;
	public TileUpdate BottomTile;
	public TileUpdate LeftTile;
	public TileUpdate RightTile;
	
	public TileUpdate TopLeftTile;
	public TileUpdate TopRightTile;
	public TileUpdate BottomLeftTile;
	public TileUpdate BottomRightTile;
	
	
	
	public enum Neighbors{Top,Bottom,Left,Right,TopLeft, TopRight,BottomLeft,BottomRight};
	public bool On;
	bool NextState;
	ArrayList Rules;
	
	// Use this for initialization
	void Start () {
		Rules = new ArrayList();
		Rules.Add(new bool[]{false,false ,true,false,true});
		Rules.Add(new bool[]{false,false,true,false,true});
	}
	
	// Update is called once per frame
	void Update () {
		Color color = renderer.material.GetColor("_Color");
		if(On == false){ color.r = 0;}
		else {
			color.r = 1;
		}

		renderer.material.SetColor("_Color",color);
		foreach(bool[] rule in Rules){
			if(RuleCheck(rule[0],TopTile)){
				if(RuleCheck(rule[1],BottomTile)){
					if(RuleCheck(rule[2],LeftTile)){
						if(RuleCheck(rule[3],RightTile)){
							NextState = rule[4];
						}
					}
				}
			}			
		}
	}
	
	bool RuleCheck(bool Rule, TileUpdate Tile){
		if(Tile == null) return false;
		else return Rule == Tile.GetComponent<TileUpdate>().State();
	}
	
	public void ChangeState(){
		On = NextState;
	}
	
	public void ChangeState(bool Value){
		NextState = Value;
		On = Value;
	}
	
	public void AssignTile(Neighbors TileNeighbor, TileUpdate Tile){
		switch(TileNeighbor){
		case Neighbors.Top:
			TopTile = Tile;
			break;
			
		case Neighbors.Bottom:
			BottomTile = Tile;
			break;
			
		case Neighbors.Left:
			LeftTile = Tile;
			break;
			
		case Neighbors.Right:
			RightTile = Tile;
			break;
			
		case Neighbors.TopLeft:
			TopLeftTile = Tile;
			break;
			
		case Neighbors.TopRight:
			BottomRightTile = Tile;
			break;
			
		case Neighbors.BottomLeft:
			BottomLeftTile = Tile;
			break;
			
		case Neighbors.BottomRight:
			BottomRightTile = Tile;
			break;
		}
	}
	
	public static void AssignPrototype(GameObject Tile){
		if(Tile.tag != "Tile"){
			Debug.LogError("Prototype Tile was not initialized with a Tile");
		}else Prototype = (GameObject)Instantiate(Tile);
	}
	
	public bool State(){
		return On;
	}
}
