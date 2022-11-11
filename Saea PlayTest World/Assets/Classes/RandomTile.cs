using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct RandomTile
{
    [SerializeField] public TileBase tile;
    [SerializeField] public int weight;

    public RandomTile(TileBase tile, int weight)
    {
        this.tile = tile;
        this.weight = weight;
    }
}
