using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public int x, y;
    public List<Piece> ToBeKilled;

    /*public void Appear()
    {
        this.transform.position = (Vector3.right * x) + (Vector3.forward * y) + new Vector3(-5f, 0f, -5f) + new Vector3(0.5f, 0.3f, 0.5f);
        //appearAnim
    }   //TO DO ANIM*/
    public void Disappear()
    {
        //destroyAnim
    }   //TO DO ANIM
    public void Selected()
    {
        //selectedAnim
    }   //TO DO ANIM
}
