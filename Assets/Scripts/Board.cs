using System;
using System.Collections;
using System.Collections.Generic;
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
        if(startTile != null && endTile != null)
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
}
