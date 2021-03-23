using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Steps
{
    float[,,] OneSteps;
    public int count;
}

public class BoardNew : MonoBehaviour
{
    public GameObject sphere;
    public GameObject yellowPiece;
    public GameObject greenPiece;
    public GameObject bluePiece;
    public GameObject redPiece;

    public GameObject YellowP;
    public GameObject GreenP;
    public GameObject RedP;
    public GameObject BlueP;

    public Piece[,] pieces = new Piece[10, 10];
    public Sphere[,] spheres = new Sphere[10, 10];
    private List<Sphere> LisSpheres = new List<Sphere>();

    private Vector3 PieceOffset = new Vector3(0.5f, 0.3f, 0.5f);
    private Vector3 BoardOffset = new Vector3(-5f, 0f, -5f);

    private Piece SelPiece = null;

    public List<Steps> AllSteps;
    int stepID;

    private Vector2 MouseOver;
    private Vector2 StartPoint;
    private Vector2 EndPoint;

    public Player Green = null;
    public Player Yellow = null;
    public Player Red = null;
    public Player Blue = null;

    private Queue<Player> PlayerQueue = new Queue<Player>();

    private Player Turn;

    public void HideSpheres(Player pl, Piece P)
    {

    }

    public void ShowSpheres(Player pl, Piece P)
    {

    }

    public void MakeMove(Player pl, int step)
    {

    }

    public void EndTurn(Player pl)
    {

    }

    public int SpheresIntoId(Sphere SP)
    {
        int id = 0;
        return id;
    }

    private void ClickManager(Player Turn){
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Piece PieC;
                    Sphere SphC;
                    GameObject Caught = hit.transform.gameObject;
                    if(Caught.TryGetComponent<Piece>(out PieC))
                    {
                        if(SelPiece == PieC)
                        {
                            SelPiece = null;
                            HideSpheres(Turn, PieC);
                            //return;
                        }else if (Turn.PieceList.Contains(PieC))
                        {
                            HideSpheres(Turn, PieC);
                            SelPiece = PieC;
                            ShowSpheres(Turn, PieC);
                            //return;
                        }else if(SelPiece == null)
                        {
                            SelPiece = PieC;
                            ShowSpheres(Turn, PieC);
                            //return;
                        }
                    }else if (Caught.TryGetComponent<Sphere>(out SphC))
                    {
                        int Id = SpheresIntoId(SphC);
                        HideSpheres(Turn, PieC);
                        MakeMove(Turn, Id);
                        SelPiece = null;
                        EndTurn(Turn);
                    }
                }
            }
        }else if (Turn.isBot)
        {

        }
    }

    private void GeneratePiece(Player play, GameObject GoPiece, int x, int y)   //Generate, add GoPiece to play's List<piece> and move to x,y
    {
        GameObject GoP = Instantiate(GoPiece) as GameObject;
        Piece Pie = GoP.GetComponent<Piece>();
        pieces[x, y] = Pie;
        Pie.x = x;
        Pie.y = y;

        Pie.transform.SetParent(play.transform);
        play.PieceList.Add(Pie);

        MovePiece(Pie, pieces, x, y);
    }

    private void MovePiece(Piece P, Piece[,] PM, int x, int y)  //Change position of P and array PM
    {
        PM[(int)P.transform.position.x, (int)P.transform.position.y] = null;
        PM[x, y] = P;
        P.x = x;
        P.y = y;
        P.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BoardOffset + PieceOffset;
    }

    private void GeneratePieceSet(Player play)  //Generate set for play
    {
        Vector3 Posit = new Vector3(0f, 0f, 0f);
        Quaternion Rot = Quaternion.Euler(0, 0, 0);
        PlayerQueue.Enqueue(play);
        switch (play.color)
        {
            case 1:
                Posit = new Vector3(0f, 6f, -6f);
                Rot = Quaternion.Euler(55, 0, 0);
                YellowP = Instantiate(YellowP, Posit, Rot) as GameObject;
                play = Yellow = YellowP.GetComponent<Player>();
                play.Cam = YellowP.GetComponent<Camera>();
                play.PieceList.Clear();
                for (int x = 3; x <= 6; x++)
                {
                    for (int z = 1; z <= 2; z++)
                    {
                        GeneratePiece(play, yellowPiece, x, z);
                    }
                }
                break;
            case 2:
                Posit = new Vector3(-6f, 6f, 0f);
                Rot = Quaternion.Euler(55, 90, 0);
                BlueP = Instantiate(BlueP, Posit, Rot) as GameObject;
                play = Blue = BlueP.GetComponent<Player>();
                play.Cam = BlueP.GetComponent<Camera>();
                play.PieceList.Clear();
                for (int z = 3; z <= 6; z++)
                {
                    for (int x = 1; x <= 2; x++)
                    {
                        GeneratePiece(play, bluePiece, x, z);
                    }
                }
                break;
            case 3:
                Posit = new Vector3(0f, 6f, 6f);
                Rot = Quaternion.Euler(55, 180, 0);
                RedP = Instantiate(RedP, Posit, Rot) as GameObject;
                play = Red = RedP.GetComponent<Player>();
                play.Cam = RedP.GetComponent<Camera>();
                play.PieceList.Clear();
                for (int x = 3; x <= 6; x++)
                {
                    for (int z = 7; z <= 8; z++)
                    {
                        GeneratePiece(play, redPiece, x, z);
                    }
                }
                break;
            case 4:
                Posit = new Vector3(6f, 6f, 0f);
                Rot = Quaternion.Euler(55, -90, 0);
                GreenP = Instantiate(GreenP, Posit, Rot) as GameObject;
                play = Green = GreenP.GetComponent<Player>();
                play.Cam = GreenP.GetComponent<Camera>();
                play.PieceList.Clear();
                for (int z = 3; z <= 6; z++)
                {
                    for (int x = 7; x <= 8; x++)
                    {
                        GeneratePiece(play, greenPiece, x, z);
                    }
                }
                break;
        }
    }

    public void ChangeCam(int color) {
        switch(color){
            case 1:
                Green.Cam.enabled = false;
                Blue.Cam.enabled = false;
                Red.Cam.enabled = false;
                Yellow.Cam.enabled = true;
                break;
            case 2:
                Yellow.Cam.enabled = false;
                Green.Cam.enabled = false;
                Red.Cam.enabled = false;
                Blue.Cam.enabled = true;
                break;
            case 3:
                Yellow.Cam.enabled = false;
                Green.Cam.enabled = false;
                Blue.Cam.enabled = false;
                Red.Cam.enabled = true;
                break;
            case 4:
                Yellow.Cam.enabled = false;
                Blue.Cam.enabled = false;
                Red.Cam.enabled = false;
                Green.Cam.enabled = true;
                break;
        }
    }
    private void TempChangeCam() {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeCam(1);
        } else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeCam(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeCam(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeCam(4);
        }
    }
    void Start()
    {
        GeneratePieceSet(Green);
        GeneratePieceSet(Red);
        GeneratePieceSet(Yellow);
        GeneratePieceSet(Blue);
    }

    void Update()
    {
        TempChangeCam();
        ClickManager(Turn);
    }
}
