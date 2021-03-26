using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Vector3 PO = new Vector3(0.5f, 0.3f, 0.5f);
    private Vector3 BO = new Vector3(-5f, 0f, -5f);

    public int x, y;
    public List<Piece> ToBeKilled;

    public void Appear()
    {
        this.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BO + PO;
        //appearAnim
    }   //TO DO ANIM
    public void Disappear()
    {
        //destroyAnim
        Destroy(this);
    }   //TO DO ANIM
    public void Selected()
    {
        //selectedAnim
        Destroy(this);
    }   //TO DO ANIM
}
