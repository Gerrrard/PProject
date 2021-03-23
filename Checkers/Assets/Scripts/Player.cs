using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score = 0;
    public int color = 0;      //1 - yellow, 2 - blue, 3 - red, 4 - green
    public bool isBot;

    public Camera Cam;

    public List<Piece> PieceList = new List<Piece>();
}
