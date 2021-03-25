using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public int x, y;

    public void Appear()
    {
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
