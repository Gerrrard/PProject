using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps
{
    float[,,] OneSteps;
    public int count;
}

public class Sphere : MonoBehaviour
{

}

public class Board : MonoBehaviour
{
    public GameObject sphere;
    public GameObject yellowPiece;
    public GameObject greenPiece;
    public GameObject bluePiece;
    public GameObject redPiece;

    public Piece[,] pieces = new Piece[10,10];
    public Sphere[,] spheres = new Sphere[10, 10];
    
    private Piece SelPiece = null;

    private Vector3 BoardOffset = new Vector3(-5f, 0f, -5f);
    private Vector3 PieceOffset = new Vector3(0.5f, 0.3f, 0.5f);

    private Vector2 MouseOver;
    private Vector2 StartDrag;
    private Vector2 EndDrag;

    public Player Green;
    public Player Yellow;
    public Player Red;
    public Player Blue;
    private Queue<Player> PlayerQueue = new Queue<Player>();

    int stepID;

    public List<Steps> AllSteps;



    private void GenerateBoard()
    {
        for (int x = 3; x <= 6; x++)
        {
            for (int z = 1; z <= 2; z++)
            {
                GeneratePiece(Yellow, yellowPiece, x, z);
            }
        }
        for (int x = 3; x <= 6; x++)
        {
            for (int z = 7; z <= 8; z++)
            {
                GeneratePiece(Red, redPiece, x, z);
            }
        }
        for (int z = 3; z <= 6; z++)
        {
            for (int x = 1; x <= 2; x++)
            {
                GeneratePiece(Blue, bluePiece, x, z);
            }
        }
        for (int z = 3; z <= 6; z++)
        {
            for (int x = 7; x <= 8; x++)
            {
                GeneratePiece(Green, greenPiece, x, z);
            }
        }
    }
    private void GeneratePiece(Player play, GameObject p, int x, int y)
    {
        GameObject Go = Instantiate(p) as GameObject;
        Piece P = Go.GetComponent<Piece>();
        pieces[x, y] = P;
        play.PieceList.Add(P);
        MovePiece(P, x, y);
        Go.transform.SetParent(transform);
    }
    private void MovePiece(Piece pie, int x, int y)
    {
        pie.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BoardOffset + PieceOffset;
    }

    private void MouseOverUpdate()
    {
        RaycastHit Hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 25.0f, LayerMask.GetMask("Board")))
        {
            MouseOver.x = (int)(Hit.point.x - BoardOffset.x);
            MouseOver.y = (int)(Hit.point.z - BoardOffset.z);
        }
        //if not collided with board
        else
        {
            MouseOver.x = -1;
            MouseOver.y = -1;
        }
    }

    private void SelectPiece(int x, int y)
    {
        //if out of bounds
        if (x < 0 || x > 9 || y < 0 || y > 9)
        {
            return;
        }

        Piece Sel = pieces[x, y];

        if(Sel != null)
        {
            SelPiece = Sel;
            SelPiece.transform.position += new Vector3(0.0f, 1f, 0.0f);
            StartDrag = MouseOver;
            //ShowPossibleStep(Sel);
        }
    }

    private void TryMove(int x1, int y1, int x2, int y2)
    {
        StartDrag = new Vector2(x1, y1);
        EndDrag = new Vector2(x2, y2);

        SelPiece = pieces[x1, y1];

        //if out of bounds
        if (x2 < 0 || y2 < 0 || x2 > 9 || y2 > 9)
        {
            if(SelPiece != null)
            {
                MovePiece(SelPiece, x1, y1);
            }

            StartDrag = Vector2.zero;
            SelPiece = null;
            return;
        }

        if(SelPiece != null)
        {
            if(EndDrag == StartDrag)
            {
                MovePiece(SelPiece, x1, y1);
                StartDrag = Vector2.zero;
                SelPiece = null;
                return;
            }

            if (SelPiece.ValidMove(pieces, x1, y1, x2, y2))
            {
                if (Mathf.Abs(x2 - x1) == 2 || Mathf.Abs(y2 - y1) == 2)
                {
                    Piece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null)
                    {
                        pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                        Destroy(p.gameObject);
                    }
                }
            pieces[x2, y2] = SelPiece;
            pieces[x1, y1] = null;

            MovePiece(SelPiece, x2, y2);
            }
            
            
            //Endturn();
        }
    }

    /*private void EndTurn()
    {

    }*/
    private void Start()
    {
        GenerateBoard();
    }
    private void Update()
    {
        MouseOverUpdate();
        int x = (int)MouseOver.x;
        int y = (int)MouseOver.y;

        if (Input.GetMouseButtonDown(0))
        {
            SelectPiece(x, y);
        }

        if (Input.GetMouseButtonUp(0) && SelPiece != null)
        {
            /*if(spheres[x,y] != null)
            {
                MovePiece(SelPiece, x, y);
                StartDrag = Vector2.zero;
                SelPiece = null;
            }*/
            MovePiece(SelPiece, x, y);
        }
    }
}
