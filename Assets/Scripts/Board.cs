using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject tileObject; // espacios vacios de la cuadricula
    [SerializeField] private float cameraSizeOffset;
    [SerializeField] private float cameraVerticalOffset;

    [SerializeField] private GameObject[] availablePieces;

    //Arreglos de dos dimensiones
    public Tile[,] Tiles;
    public Piece[,] Pieces;

    Tile startTile;
    Tile endTile;

    private void Start()
    {
        Tiles = new Tile[width, height];
        Pieces = new Piece[width, height];
        SetupBoard();
        PositionBoard();
        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPieces = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
                var o = Instantiate(selectedPieces, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[x, y] = o.GetComponent<Piece>(); //guardo una referencia al comoponebte tile que acabo de crear
                Pieces[x,y] ?.Setup(x, y, this);
            }
        }
    }

    private void PositionBoard()
    {
        float newPositionX = (float)width / 2f;
        float newPositionY = (float)height / 2f;
        Camera.main.transform.position = new Vector3(newPositionX - 0.5f, newPositionY - 0.5f + cameraVerticalOffset, -10f);
        float horizontal = width + 1;
        float vertical = (height / 2) + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffset : vertical + cameraSizeOffset;
    }

    private void SetupBoard()
    {        
        for(int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                var o = Instantiate(tileObject, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Tiles[x, y] = o.GetComponent<Tile>(); //guardo una referencia al comoponebte tile que acabo de crear
                Tiles[x, y]?.Setup(x, y, this);
            }
        }
    }

    public void TileDown(Tile tile_)
    {
        startTile = tile_;
    }

    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }

    public void TileUp(Tile tile_)
    {
        if(startTile != null && endTile != null && IsCloseTo(startTile, endTile))
        {
            SwapTiles();
        }
        startTile = null;
        endTile = null;
    }

    private void SwapTiles()
    {
        var StartPiece = Pieces[startTile.x, startTile.y];
        var EndPiece = Pieces[endTile.x, endTile.y];

        StartPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x, startTile.y);

        Pieces[startTile.x, startTile.y] = EndPiece;
        Pieces[endTile.x, endTile.y] = StartPiece;
    }

    private bool IsCloseTo(Tile start, Tile end)
    {
        if(Math.Abs((start.x-end.x)) ==1 && start.y == end.y) //mismo eje horizontal
        {
            return true;
        }
        if(Math.Abs((start.y - end.y)) == 1 && start.x == end.x ) //mismo eje vertical
        {
            return true;
        }
        return false;
    }

    private List<Piece> GetMatchByDirection(int xpos, int ypos, Vector2 direction, int minPieces = 3) 
    {
        List<Piece> matches = new List<Piece>();
        Piece startPiece = Pieces[xpos, ypos];
        matches.Add(startPiece);

        int nextX;
        int nextY;
        int maxValue = width > height ? width : height;

        for(int i=1; i<maxValue; i++)
        {
            nextX = xpos + (int)direction.x * i;
            nextY = ypos + (int)direction.y * i;

            if(nextX >= 0 && nextX<width && nextY>=0 && nextY<height)
            {
                var nextPiece = Pieces[nextX, nextY];
                if (nextPiece != null && nextPiece.pieceType == startPiece.pieceType)
                    matches.Add(nextPiece);
                else
                {
                    break;
                }
            }           
        }
        if (matches.Count >= minPieces)
            return matches;
        return null;
    }

    private List<Piece> GetMatchByPiece(int xpos, int ypos, int minPieces = 3)
    {
        var upMatchs = GetMatchByDirection(xpos, ypos, new Vector2(0, 1), 2);
        var downMatchs = GetMatchByDirection(xpos, ypos, new Vector2(0, -1), 2);
        var rightMatchs = GetMatchByDirection(xpos, ypos, new Vector2(1, 0), 2);
        var leftMatchs = GetMatchByDirection(xpos, ypos, new Vector2(-1, 0), 2);

        if (upMatchs == null) upMatchs = new List<Piece>();
        if (downMatchs == null) downMatchs = new List<Piece>();
        if (rightMatchs == null) rightMatchs = new List<Piece>();
        if (leftMatchs == null) leftMatchs = new List<Piece>();

        var verticalMatches = upMatchs.Union(downMatchs).ToList();
        var horizontalMatches = leftMatchs.Union(rightMatchs).ToList();

        var foundMatches = new List<Piece>();
        if (verticalMatches.Count >= minPieces)
            foundMatches.Union(verticalMatches).ToList();
        if (horizontalMatches.Count >= minPieces)
            foundMatches.Union(horizontalMatches).ToList();

        return foundMatches;
    }
}
