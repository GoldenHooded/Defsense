using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRateStablish : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
