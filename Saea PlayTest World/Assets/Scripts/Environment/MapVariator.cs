using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapVariator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private RandomTileLists listSource;

    private List<List<RandomTile>> AllTiles;

    // Start is called before the first frame update
    void Start()
    {
        listSource = GameObject.Find("Tile Lists").GetComponent<RandomTileLists>();
        AllTiles = listSource.GetAll();

        tilemap.CompressBounds();

        Vector3Int bottomLeft = tilemap.origin;
        Vector3Int topRight = tilemap.origin + tilemap.size;
        int z = tilemap.origin.z;
        for (int x = bottomLeft.x; x < topRight.x; x++)
        {
            for (int y = bottomLeft.y; y < topRight.y; y++)
            {
                Vector3Int tile = new Vector3Int(x, y, z);
                TileBase currentTile = tilemap.GetTile(tile);
                foreach (List<RandomTile> tiles in AllTiles)
                {
                    bool containsTile = false;
                    foreach (RandomTile listTile in tiles)
                    {
                        if (listTile.tile == currentTile)
                        {
                            containsTile = true;
                        }
                    }
                    if (containsTile == true)
                    {
                        tilemap.SetTile(tile, RandomTile(tiles));
                    }
                }
            }
        }
    }

    private TileBase RandomTile(List<RandomTile> tiles)
    {
        List<RandomTile> rngList = new List<RandomTile>();
        foreach (RandomTile RandomTile in tiles)
        {
            for (int i = RandomTile.weight; i > 0; i--)
            {
                rngList.Add(RandomTile);
            }
        }
        int rng = Random.Range(1, rngList.Count);
        rng--;
        return rngList[rng].tile;
    }
}
