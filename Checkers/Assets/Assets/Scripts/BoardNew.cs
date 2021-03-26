using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNew : MonoBehaviour
{
    private Vector3 PieceOffset = new Vector3(0.5f, 0.3f, 0.5f);
    private Vector3 BoardOffset = new Vector3(-5f, 0f, -5f);

    public GameObject sphere;
    public GameObject yellowPiece;  //may be changed to materials switch
    public GameObject greenPiece;
    public GameObject bluePiece;
    public GameObject redPiece;

    public GameObject PlayPref;
    public GameObject YellowP;
    public GameObject GreenP;
    public GameObject RedP;
    public GameObject BlueP;

    public Player Green = null;
    public Player Yellow = null;
    public Player Red = null;
    public Player Blue = null;

    public Piece[,] pieces = new Piece[10, 10];

    private List<Sphere> LisSpheres = new List<Sphere>();

    private Piece SelPiece = null;

    public List<Piece[,]> AllStepsTemp;
    public List<List<Piece[,]>> AllSteps;
    public bool[,] Blocks = new bool[10, 10];

    public int[,,,] stepsBot;

    int stepID;

    private Queue<Player> PlayerQueue = new Queue<Player>();

    private Player Turn = null;

    private void SetBlock(bool[,] bl)
    {
        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                if(((x >= 0) && (x <= 2) && (y + 1 >= 0 && y + 1 <= 2 || y + 1 >= 7 && y + 1 <= 9) || (x >= 7) && (x <= 9) && (y + 1 >= 0 && y + 1 <= 2 || y + 1 >= 7 && y + 1 <= 9)))
                {
                    bl[x, y] = false;
                }
                else
                {
                    bl[x, y] = true;
                }
            }
        }
    }   //READY

    public void PossibleMoves(bool killed, int x, int y)
    {
        int col = pieces[x, y].color;
        int lv = pieces[x, y].level;

        for(int i = 1; i <= lv; i++)
        {
            if (lv == 1)
            {
                if (col == 1 || col == 3)
                {
                    if ((x + 1) < 10 && Blocks[x + 1, y])
                    {
                        if (pieces[x + 1, y] == null && !killed)
                        {
                            Sphere S = new Sphere();
                            S.x = x + 1;
                            S.y = y;
                            LisSpheres.Add(S);
                        }
                        else if (pieces[x + 1, y].color != pieces[x, y].color && x + 2 < 10 && Blocks[x + 2, y] && pieces[x + 2, y] == null)
                        {
                            Sphere S = new Sphere();
                            S.x = x + 2;
                            S.y = y;
                            S.ToBeKilled.Add(pieces[x + 1, y]);
                            LisSpheres.Add(S);
                            PossibleMoves(true, x + 2, y);
                        }
                    }
                    if ((x - 1) > 0 && Blocks[x - 1, y])
                    {
                        if (pieces[x - 1, y] == null && !killed)
                        {
                            Sphere S = new Sphere();
                            S.x = x - 1;
                            S.y = y;
                            LisSpheres.Add(S);
                        }
                        else if (pieces[x - 1, y].color != pieces[x, y].color && x - 2 > 0 && Blocks[x - 2, y] && pieces[x - 2, y] == null)
                        {
                            Sphere S = new Sphere();
                            S.x = x - 2;
                            S.y = y;
                            S.ToBeKilled.Add(pieces[x - 1, y]);
                            LisSpheres.Add(S);
                            PossibleMoves(true, x - 2, y);
                        }
                    }
                    //check 2 & 4 sides
                    if (col == 1)
                    {
                        if ((y + 1) < 10 && Blocks[x, y + 1])
                        {
                            if (pieces[x, y + 1] == null && !killed)
                            {
                                Sphere S = new Sphere();
                                S.x = x;
                                S.y = y + 1;
                                LisSpheres.Add(S);
                            }
                            else if (pieces[x, y + 1].color != pieces[x, y].color && y + 2 < 10 && Blocks[x, y + 2] && pieces[x, y + 2] == null)
                            {
                                Sphere S = new Sphere();
                                S.x = x;
                                S.y = y + 2;
                                S.ToBeKilled.Add(pieces[x, y + 1]);
                                LisSpheres.Add(S);
                                PossibleMoves(true, x, y + 2);
                            }
                        }
                    }   //check 3 side
                    else
                    {
                        if ((y - 1) > 0 && Blocks[x, y - 1])
                        {
                            if (pieces[x, y - 1] == null && !killed)
                            {
                                Sphere S = new Sphere();
                                S.x = x;
                                S.y = y - 1;
                                LisSpheres.Add(S);
                            }
                            else if (pieces[x, y - 1].color != pieces[x, y].color && y - 2 > 0 && Blocks[x, y - 2] && pieces[x, y - 2] == null)
                            {
                                Sphere S = new Sphere();
                                S.x = x;
                                S.y = y - 2;
                                S.ToBeKilled.Add(pieces[x, y - 1]);
                                LisSpheres.Add(S);
                                PossibleMoves(true, x, y - 2);
                            }
                        }
                    }   //check 1 side
                }
                else if (col == 2 || col == 4)
                {
                    if ((y + 1) < 10 && Blocks[x, y + 1])
                    {
                        if (pieces[x, y + 1] == null && !killed)
                        {
                            Sphere S = new Sphere();
                            S.x = x;
                            S.y = y + 1;
                            LisSpheres.Add(S);
                        }
                        else if (pieces[x, y + 1].color != pieces[x, y].color && y + 2 < 10 && Blocks[x, y + 2] && pieces[x, y + 2] == null)
                        {
                            Sphere S = new Sphere();
                            S.x = x;
                            S.y = y + 2;
                            S.ToBeKilled.Add(pieces[x, y + 1]);
                            LisSpheres.Add(S);
                            PossibleMoves(true, x, y + 2);
                        }
                    }
                    if ((y - 1) > 0 && Blocks[x, y - 1])
                    {
                        if (pieces[x, y - 1] == null && !killed)
                        {
                            Sphere S = new Sphere();
                            S.x = x;
                            S.y = y - 1;
                            LisSpheres.Add(S);
                        }
                        else if (pieces[x, y - 1].color != pieces[x, y].color && y - 2 > 0 && Blocks[x, y - 2] && pieces[x, y - 2] == null)
                        {
                            Sphere S = new Sphere();
                            S.x = x;
                            S.y = y - 2;
                            S.ToBeKilled.Add(pieces[x, y - 1]);
                            LisSpheres.Add(S);
                            PossibleMoves(true, x, y - 2);
                        }
                    }
                    //check 1 & 3 sides
                    if (col == 2)
                    {
                        if ((x + 1) < 10 && Blocks[x + 1, y])
                        {
                            if (pieces[x + 1, y] == null && !killed)
                            {
                                Sphere S = new Sphere();
                                S.x = x + 1;
                                S.y = y;
                                LisSpheres.Add(S);
                            }
                            else if (pieces[x + 1, y].color != pieces[x, y].color && x + 2 < 10 && Blocks[x + 2, y] && pieces[x + 2, y] == null)
                            {
                                Sphere S = new Sphere();
                                S.x = x + 2;
                                S.y = y;
                                S.ToBeKilled.Add(pieces[x + 1, y]);
                                LisSpheres.Add(S);
                                PossibleMoves(true, x + 2, y);
                            }
                        }
                    }   //check 4 side
                    else
                    {
                        if ((x - 1) > 0 && Blocks[x - 1, y])
                        {
                            if (pieces[x - 1, y] == null && !killed)
                            {
                                Sphere S = new Sphere();
                                S.x = x - 1;
                                S.y = y;
                                LisSpheres.Add(S);
                            }
                            else if (pieces[x - 1, y].color != pieces[x, y].color && x - 2 > 0 && Blocks[x - 2, y] && pieces[x - 2, y] == null)
                            {
                                Sphere S = new Sphere();
                                S.x = x - 2;
                                S.y = y;
                                S.ToBeKilled.Add(pieces[x - 1, y]);
                                LisSpheres.Add(S);
                                PossibleMoves(true, x - 2, y);
                            }
                        }
                    }   //check 2 side
                }
            }
            else
            {
                if ((x + i) < 10 && Blocks[x + i, y])
                {
                    if (pieces[x + i, y] == null && !killed)
                    {
                        Sphere S = new Sphere();
                        S.x = x + i;
                        S.y = y;
                        LisSpheres.Add(S);
                    }
                    else if (pieces[x + i, y].color != pieces[x, y].color && x + i + 1 < 10 && Blocks[x + i + 1, y] && pieces[x + i + 1, y] == null)
                    {
                        Sphere S = new Sphere();
                        S.x = x + i + 1;
                        S.y = y;
                        S.ToBeKilled.Add(pieces[x + i, y]);
                        LisSpheres.Add(S);
                        PossibleMoves(true, x + i + 1, y);
                    }
                }
                if ((x - i) > 0 && Blocks[x - i, y])
                {
                    if (pieces[x - i, y] == null && !killed)
                    {
                        Sphere S = new Sphere();
                        S.x = x - i;
                        S.y = y;
                        LisSpheres.Add(S);
                    }
                    else if (pieces[x - i, y].color != pieces[x, y].color && x - i - 1 > 0 && Blocks[x - i - 1, y] && pieces[x - i - 1, y] == null)
                    {
                        Sphere S = new Sphere();
                        S.x = x - i - 1;
                        S.y = y;
                        S.ToBeKilled.Add(pieces[x - i, y]);
                        LisSpheres.Add(S);
                        PossibleMoves(true, x - i - 1, y);
                    }
                }
                if ((y + i) < 10 && Blocks[x, y + i])
                {
                    if (pieces[x, y + i] == null && !killed)
                    {
                        Sphere S = new Sphere();
                        S.x = x;
                        S.y = y + i;
                        LisSpheres.Add(S);
                    }
                    else if (pieces[x, y + i].color != pieces[x, y].color && y + i + 1 < 10 && Blocks[x, y + i + 1] && pieces[x, y + i + 1] == null)
                    {
                        Sphere S = new Sphere();
                        S.x = x;
                        S.y = y + i + 1;
                        S.ToBeKilled.Add(pieces[x, y + i]);
                        LisSpheres.Add(S);
                        PossibleMoves(true, x, y + i + 1);
                    }
                }
                if ((y - i) > 0 && Blocks[x, y - i])
                {
                    if (pieces[x, y - i] == null && !killed)
                    {
                        Sphere S = new Sphere();
                        S.x = x;
                        S.y = y - i;
                        LisSpheres.Add(S);
                    }
                    else if (pieces[x, y - i].color != pieces[x, y].color && y - i - 1 > 0 && Blocks[x, y - i - 1] && pieces[x, y - i - 1] == null)
                    {
                        Sphere S = new Sphere();
                        S.x = x;
                        S.y = y - i - 1;
                        S.ToBeKilled.Add(pieces[x, y - i]);
                        LisSpheres.Add(S);
                        PossibleMoves(true, x, y - i - 1);
                    }
                }
            }
        }
    }   //REFACTORING NEEDED

    public void MakeMove(Player pl, int step)
    {

    }   //TO DO

    private void EndTurn(Player pl)
    {

    }   //TO DO

    private void EndGame()
    {

    }   //TO DO LATER

    private int[,,,] AllPossibleSteps(Player pl)
    {
        return null;
    }   //TO DO LATER

    private float[,,,] ConvertToFloatArr(int[,,,] stp)
    {
        return null;
    }   //TO DO LATER

    private void InterBotFunction(Player pl)
    {
        AllPossibleSteps(pl);
        ConvertToFloatArr(stepsBot);
        // manipulations with stepID
    }   //TO DO LATER

    private int SpheresIntoId(Sphere SP)
    {
        int id = 0;
        return id;
    }   //TO DO

    private void ClickManager(Player turn)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = turn.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Piece PieC;
                    Sphere SphC;
                    GameObject Caught = hit.transform.gameObject;
                    if (Caught.TryGetComponent<Piece>(out PieC))
                    {
                        if (SelPiece == PieC)
                        {
                            SelPiece = null;
                            HideSpheres(turn, null);
                        }
                        else if (turn.PieceList.Contains(PieC))
                        {
                            HideSpheres(turn, null);
                            SelPiece = PieC;
                            ShowSpheres(turn);
                        }
                        else if (SelPiece == null)
                        {
                            SelPiece = PieC;
                            ShowSpheres(turn);
                        }
                    }
                    else if (Caught.TryGetComponent<Sphere>(out SphC))
                    {
                        int Id = SpheresIntoId(SphC);
                        HideSpheres(turn, SphC);
                        MakeMove(turn, Id);
                        SelPiece = null;
                        EndTurn(turn);
                    }
                }
            }
        }
        else if (turn.isBot)
        {
            InterBotFunction(turn);
            MakeMove(turn, stepID);
            EndTurn(turn);
        }
    }   //READY, BUT CHECK FOR GAME END

    public void HideSpheres(Player pl, Sphere S)
    {
        if (S != null)  //not selection
        {
            stepID = SpheresIntoId(S);
            S.Selected();
            LisSpheres.Remove(S);
        }

        foreach (Sphere Sp in LisSpheres)
        {
            Sp.Disappear();
            LisSpheres.Remove(Sp);
        }
    }   //READY

    public void ShowSpheres(Player pl)
    {
        int i = 0;
        foreach (Sphere Sp in LisSpheres)
        {
            GameObject SpH = Instantiate(sphere) as GameObject;
            LisSpheres[i] = SpH.GetComponent<Sphere>();
            LisSpheres[i].Appear();
            i++;
        }
    }   //READY

    public void GeneratePiece(Player play, GameObject GoPiece, int x, int y, bool Playable)   //READY
    {
        if (Playable)
        {
            GameObject GoP = Instantiate(GoPiece) as GameObject;
            Piece Pie = GoP.GetComponent<Piece>();
            pieces[x, y] = Pie;
            Pie.x = x;
            Pie.y = y;

            Pie.transform.SetParent(play.transform);
            play.PieceList.Add(Pie);

            MovePiece(Pie, x, y);
        }
        else
        {
            pieces[x, y].GenNonPlayablePiece(pieces, GoPiece);
        }
    }

    public void MovePiece(Piece P, int x, int y)  //READY
    {
        pieces[(int)P.transform.position.x, (int)P.transform.position.y] = null;
        pieces[x, y] = P;
        P.x = x;
        P.y = y;
        P.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BoardOffset + PieceOffset + P.offset;
    }

    private void GeneratePieceSet(Player play)  //READY
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
                        GeneratePiece(play, yellowPiece, x, z, true);
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
                        GeneratePiece(play, bluePiece, x, z, true);
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
                        GeneratePiece(play, redPiece, x, z, true);
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
                        GeneratePiece(play, greenPiece, x, z, true);
                    }
                }
                break;
        }
    } 

    public void ChangeCam(int color)
    {
        Debug.Log(1);
        switch (color)
        {
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
    }   //READY

    private void TempChangeCam()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeCam(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
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
    }   //READY

    void Start()
    {
        SetBlock(Blocks);
        GeneratePieceSet(Green);
        GeneratePieceSet(Red);
        GeneratePieceSet(Yellow);
        GeneratePieceSet(Blue);
        Turn = Yellow;
    }

    void Update()
    {
        TempChangeCam();
        ClickManager(Turn);
    }
}