using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    void Update()
    {
        SoundManager.instance.PlaySound("BackGround");      // BackGround Àç»ý
    }
}
