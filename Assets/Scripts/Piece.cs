using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    [SerializeField] public int x, y;
    [SerializeField] private Board boardScript;

    public enum Type
    {
        elephant,
        giraffe,
        hippo,
        monkey,
        panda,
        parrot,
        penguin,
        pig,
        rabbit,
        snake
    };

    public Type pieceType;

    public void Setup(int x_, int y_, Board board_)
    {
        x = x_;
        y = y_;
        boardScript = board_;

        //efecto para mejorar cuando aparecen las piezas
        transform.localScale = Vector3.one * 0.35f;
        transform.DOScale(Vector3.one, 0.35f);
    }

    public void Move(int desX, int desY)
    {
        transform.DOMove(new Vector3(desX, desY, -5), 0.25f).SetEase(Ease.InOutCubic).onComplete = () =>
        {
            x = desX;
            y = desY;
        };
    }

    public void Remove(bool animated)
    {
        if(animated)
        {           
            transform.DORotate(new Vector3(0, 0, -120f), 0.12f);
            transform.DOScale(Vector3.one * 1.2f, 0.005f).onComplete = () =>
            {
                transform.DOScale(Vector3.zero, 0.1f).onComplete = () =>
                {
                    Destroy(gameObject);
                };
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(0, 0);
    }
}
