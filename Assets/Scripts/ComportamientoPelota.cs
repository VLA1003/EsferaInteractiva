using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoPelota : MonoBehaviour
{
    bool clickDone = false;
    public void clickEnPelota()
    {
        if (clickDone == false)
        {
            clickDone = true;
        }
        else
        {
            clickDone = false;
        }

    }
}
