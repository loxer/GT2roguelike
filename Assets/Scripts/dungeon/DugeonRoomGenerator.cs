using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class DungeonTile : Tile
{   
    public Sprite[] dungeonTiles;

    public override void RefreshTile(Vector3Int pos, ITilemap tilemap){
        int z = 0;
        for(int y = -1; y <=1; y++){
            for(int x = -1; x <=1; x++){

                Vector3Int loc = new Vector3Int(pos.x + x, pos.y + y, pos.z + z);
                if(hasTile(tilemap, loc)){
                    tilemap.RefreshTile(loc);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int pos, ITilemap tilemap, ref TileData tiledata) {
        int mask = hasTile(tilemap, pos + new Vector3Int(0, 1, 0)) ? 1 : 0;
        mask = hasTile(tilemap, pos + new Vector3Int(0, 1, 0)) ? 2 : 0;
        mask = hasTile(tilemap, pos + new Vector3Int(0, 1, 0)) ? 4 : 0;
        mask = hasTile(tilemap, pos + new Vector3Int(0, 1, 0)) ? 8 : 0;
        int tileID = getID((byte)mask);
        if (tileID >= 0 && tileID < dungeonTiles.Length) {
            tiledata.sprite = dungeonTiles[tileID];
            tiledata.flags = TileFlags.LockTransform;
            if (tileID != 2 || tileID != 0)
            {
                tiledata.colliderType = ColliderType.None;
            } 
            else{
                tiledata.colliderType = ColliderType.Grid;
            }
        }
    }


    private int getID(byte mask){
        switch (mask){ 
            case 0: return 0; //walls @ L-eft T-op R-ight B-ottom
            case 6: return 1; //LT
            case 14: return 2; //T
            case 12: return 3; //RT
            case 7: return 4; //R
            case 13: return 5; //RB
            case 3: return 6; //LB
            case 11: return 7; //B
            case 9: return 8; //RB
            case 15: return 0; //all
        }
        return -1;
    }


    private bool hasTile(ITilemap tilemap, Vector3Int pos){
        return tilemap.GetTile(pos);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/DungeonGenerator")]
    public static void CreateDungeon()
    {
        string path = EditorUtility.SaveFilePanelInProject("dungeonTiles", "newDungeon","Asset", "raster");
        if(path == ""){
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DungeonTile>(), path);
    }
#endif
}