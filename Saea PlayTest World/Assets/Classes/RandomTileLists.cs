using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileLists : MonoBehaviour
{
    [Header("Moss")]
    [SerializeField] public List<RandomTile> moss_vertical;
    [SerializeField] public List<RandomTile> moss_horizontal;
    [SerializeField] public List<RandomTile> moss_cornerNE;
    [SerializeField] public List<RandomTile> moss_cornerSE;
    [SerializeField] public List<RandomTile> moss_cornerSW;
    [SerializeField] public List<RandomTile> moss_cornerNW;
    [SerializeField] public List<RandomTile> moss_diagonalSN;
    [SerializeField] public List<RandomTile> moss_diagonalNS;

    [Header("Breakable tiles")]
    [SerializeField] public List<RandomTile> breakable_ceiling;
    [SerializeField] public List<RandomTile> breakable_rightWall;
    [SerializeField] public List<RandomTile> breakable_floor;
    [SerializeField] public List<RandomTile> breakable_leftWall;

    public List<List<RandomTile>> GetAll()
    {
        List<List<RandomTile>> AllLists = new List<List<RandomTile>> {
            moss_vertical,
            moss_horizontal,
            moss_cornerNE,
            moss_cornerSE,
            moss_cornerSW,
            moss_cornerNW,
            moss_diagonalSN,
            moss_diagonalNS,
            breakable_ceiling,
            breakable_rightWall,
            breakable_floor,
            breakable_leftWall
        };
        
        /*AllLists.Add(moss_vertical);
        AllLists.Add(moss_horizontal);
        AllLists.Add(moss_cornerNE);
        AllLists.Add(moss_cornerSE);
        AllLists.Add(moss_cornerSW);
        AllLists.Add(moss_cornerNW);
        AllLists.Add(moss_diagonalSN);
        AllLists.Add(moss_diagonalNS);

        AllLists.Add(breakable_floor);
        AllLists.Add(breakable_ceiling);
        AllLists.Add(breakable_leftWall);
        AllLists.Add(breakable_rightWall);*/

        return AllLists;
    }
}
