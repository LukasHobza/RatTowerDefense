using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour
{
    public static PathHolder pH;
    public Transform[] path;

    private void Awake()
    {
        pH = this;
    }
}
