using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int x, y;
    [SerializeField] private Board boardScript;

    public void Setup(int x_, int y_, Board board_)
    {
        x = x_;
        y = y_;
        boardScript = board_;
    }

    public void OnMouseDown()
    {
        boardScript.TileDown(this);
    }

    public void OnMouseEnter()
    {
        boardScript.TileOver(this);
    }

    public void OnMouseUp()
    {
        boardScript.TileUp(this);
    }
}
