using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool sank;

    private void Start()
    {
        sank = false;
    }

    public void SetSank()
    {
        sank = true;
    }
}
