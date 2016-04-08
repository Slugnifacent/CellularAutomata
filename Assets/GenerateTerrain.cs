using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {
	
	public int Width;
	public int Height;
	TileUpdate[,] Tiles;
	public GameObject DefaultTile;
	float seconds = 2;
	
	// Use this for initialization
	void Start () {
		if(DefaultTile == null){
			Debug.LogError("Defualt Tile is Null");
		}
		if(DefaultTile.tag != "Tile"){
			Debug.LogError("DefualeTile was not initialized with a Tile");
		}
		TileUpdate.AssignPrototype(DefaultTile);
		Tiles = new TileUpdate[Width,Height];
		Vector3 TileDimensions = DefaultTile.transform.localScale;
		for(int index = 0; index < Width; index++){
			for(int endex = 0; endex < Height; endex++){
				GameObject tile = (GameObject)Instantiate(DefaultTile);
				tile.transform.Translate(new Vector3(index*TileDimensions.x,0,endex*TileDimensions.z));
				Color color = tile.renderer.material.GetColor("_Color");
				tile.renderer.material.SetColor("_Color",color);		
				Tiles[index , endex] = tile.GetComponent<TileUpdate>();
				if((index*endex)%32==0){
					Tiles[index , endex].ChangeState(true);
				}
			}
		}
		for(int index = 0; index < Width; index++){
			for(int endex = 0; endex < Height; endex++){
				SafeNeighborAdd(Tiles[index , endex],TileUpdate.Neighbors.Top   , index, endex-1);
				SafeNeighborAdd(Tiles[index , endex],TileUpdate.Neighbors.Bottom, index, endex+1);
				SafeNeighborAdd(Tiles[index , endex],TileUpdate.Neighbors.Left  , index-1, endex);
				SafeNeighborAdd(Tiles[index , endex],TileUpdate.Neighbors.Right , index+1, endex);
				
			}
		}
	}
	
	void SafeNeighborAdd( TileUpdate Tile, TileUpdate.Neighbors Neighbor,int index, int endex){
		if(index >= 0 && index < Width && endex >= 0 && endex < Height)
		{
			Tile.AssignTile(Neighbor, Tiles[index,endex]);
		}
		else {
			Tile.AssignTile(Neighbor, null);
		}
	}
	
	
	
	void LateUpdate(){
		seconds -= Time.deltaTime;
		if(seconds <= 0){
			for(int index = 0; index < Width; index++){
				for(int endex = 0; endex < Height; endex++){
					Tiles[index , endex].ChangeState();
				}
			}
			seconds = .1f;
		}
	}
}
