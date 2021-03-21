using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    int color;      //1 - yellow, 2 - blue, 3 - red, 4 - green
    //public bool isBot;
    public List<Piece> PieceList = new List<Piece>();
}
