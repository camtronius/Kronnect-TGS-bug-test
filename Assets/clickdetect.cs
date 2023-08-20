using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGS;

public class clickdetect : MonoBehaviour
{
    // Start is called before the first frame update
    TerrainGridSystem tgs;

    void Start()
    {
        tgs = TerrainGridSystem.instance;
        testHexlocs();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetMouseButtonUp(0))
        {

            int row = tgs.CellGetRow(tgs.cellLastClickedIndex);
            int col = tgs.CellGetColumn(tgs.cellLastClickedIndex);
            Debug.Log("Last clicked cell- row: " + row + " col: " + col);

        }
    }

    public void testHexlocs()
    {
        int cellIndex = tgs.CellGetIndex(5, 5, false);
        int neighborIndexBot = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.Bottom);
        int neighborIndexBotL = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.BottomLeft);
        int neighborIndexBotR = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.BottomRight);
        int neighborIndexTop = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.Top);
        int neighborIndexTopL = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.TopLeft);
        int neighborIndexTopR = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.TopRight);
        int neighborIndexL = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.Left);
        int neighborIndexR = tgs.CellGetNeighbour(cellIndex, CELL_SIDE.Right);

        //tgs.CellColorTemp(neighborIndexTop, Color.green, 99999);
        tgs.CellColorTemp(neighborIndexTopL, Color.red, 99999);
        tgs.CellColorTemp(neighborIndexTopR, Color.blue, 99999);
        tgs.CellColorTemp(neighborIndexL, Color.black, 99999);
        tgs.CellColorTemp(neighborIndexR, Color.gray, 99999);
        tgs.CellColorTemp(neighborIndexBot, Color.cyan, 99999);
        tgs.CellColorTemp(neighborIndexBotL, Color.magenta, 99999);
        tgs.CellColorTemp(neighborIndexBotR, Color.yellow, 99999);

        foreach (TGS.Cell i in tgs.CellGetNeighbours(cellIndex))
        {
            Debug.Log(i.row + " " + i.column);
            tgs.CellFlash(i.index, Color.yellow);

        }
    }
}
