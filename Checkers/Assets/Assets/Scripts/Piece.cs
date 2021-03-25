using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int color = 0;   //1 - yellow, 2 - blue, 3 - red, 4 - green
    public bool hasRed = false, hasYellow = false, hasGreen = false, hasBlue = false;
    public int level = 1;
    public bool isPlayable = true;

    public Vector3 offset = new Vector3(0f,0f,0f);
    private Vector3 PO = new Vector3(0.5f, 0.3f, 0.5f);
    private Vector3 BO = new Vector3(-5f, 0f, -5f);

    public int x, y;
    
    public List<Piece> child = new List<Piece>();

    public GameObject yPiece;  //may be changed to materials switch or to change in future (!)
    public GameObject gPiece;
    public GameObject bPiece;
    public GameObject rPiece;

    public void ChangeLevel(Piece[,] PM, Piece P)
    {
        int col = 0;

        if (P.y > 2 && P.y < 7)
        {
            if(P.x == 0)
            {
                col = 2;
                GenNonPlayablePiece(PM, bPiece);
                this.hasBlue = true;
            }
            else if(P.x == 9)
            {
                col = 4;
                GenNonPlayablePiece(PM, gPiece);
                this.hasGreen = true;
            }
        }
        else if (P.x > 2 && P.x < 7)
        {
            if(P.y == 9)
            {
                col = 3;
                GenNonPlayablePiece(PM, rPiece);
                this.hasRed = true;
            }
            else if (P.y == 0)
            {
                col = 1;
                GenNonPlayablePiece(PM, yPiece);
                this.hasYellow = true;
            }
        }

        if (col != 0)
        {
            /*animation of appearing
             */
            this.level = 1 + (this.hasRed ? 1 : 0) + (this.hasYellow ? 1 : 0) + (this.hasGreen ? 1 : 0) + (this.hasBlue ? 1 : 0);
            this.offset = this.level * Vector3.up * 0.25f;
        }
    }   //READY

    public void GenNonPlayablePiece(Piece[,] PM, GameObject GoPie)
    {
        GameObject GoP = Instantiate(GoPie) as GameObject;
        Destroy(GoP.GetComponent<BoxCollider>());
        Piece Pie = GoP.GetComponent<Piece>();
        Pie.isPlayable = false;

        BoxCollider BCP = this.transform.GetComponent<BoxCollider>();   //check collider later
        BCP.size = new Vector3(BCP.size.x, BCP.size.y, BCP.size.z / this.level * (this.level + 1));
        BCP.center = new Vector3(BCP.center.x, BCP.center.y, BCP.center.z - 0.0025f);

        Pie.transform.position = (Vector3.right * this.x) + (Vector3.forward * this.y) + BO + PO + Vector3.up * (-0.3f);
        
        Pie.transform.SetParent(PM[this.x, this.y].transform);
        PM[this.x, this.y].child.Add(Pie);
    }   //READY

    public void GetPoints(Player pl)
    {
        pl.score += (this.level + 1);
    }   //READY

    public void KillPiece(Player pl)
    {
        foreach (Piece P in this.child)
        {
            this.child.Remove(P);
            P.DeathAnim();
        }
        this.GetPoints(pl);
        this.DeathAnim();
    }   //READY

    private void DeathAnim()
    {
        //deathAnim
        Destroy(this);
    }   //TO DO

    public void MoveAnim(int x1, int y1, int x2, int y2)
    {

    }   //TO DO

    public void SelectPieceAnim()
    {

    }   //TO DO

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
