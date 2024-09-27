using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour //je to fakt dulezite!
{
    public static PathHolder pH;
    public Transform[] path;

    private void Awake()
    {
        pH = this;
    }
}
