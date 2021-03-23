using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int color = 0;   //1 - yellow, 2 - blue, 3 - red, 4 - green
    public bool hasRed = false, hasYellow = false, hasGreen = false, hasBlue = false;
    public int level = 0;
    public Vector3 offset = new Vector3(0f,0f,0f);
    public bool isPlayable = true;
    public int x, y;

    /*public bool ValidMove(Piece[,] board, int x1,int y1,int x2,int y2)
    {
        int deltaMoveX, deltaMoveY;

        if(board[x2,y2] != null)
        {
            return false;
        }

        deltaMoveX = x2 - x1;
        deltaMoveY = y2 - y1;

        if(isRed || isYellow)
        {
            if(Mathf.Abs(deltaMoveX) == 1 || isRed && (deltaMoveY == -1) || isYellow && (deltaMoveY == 1))
            {
                return true;
            }
            else if(Mathf.Abs(deltaMoveX) == 2 || isRed && (deltaMoveY == -2) || isYellow && (deltaMoveY == 2))
            {
                Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                if(p != null && (isRed && !p.isRed || isYellow && !p.isYellow))
                {
                    return true;
                }
            }
        }

        if(isBlue || isGreen)
        {
            if(Mathf.Abs(deltaMoveY) == 1 || isBlue && (deltaMoveX == 1) || isGreen && (deltaMoveX == -1))
            {
                return true;
            }
            else if(Mathf.Abs(deltaMoveY) == 2 || isBlue && (deltaMoveX == 2) || isGreen && (deltaMoveX == -2))
            {
                Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                if (p != null && (isBlue && !p.isBlue || isGreen && !p.isGreen))
                {
                    return true;
                }
            }
        }

        return false;
    }*/
}
