using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BoardController : MonoBehaviour
{
    private float angleX;
    private float angleZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angleX = 0.0f;
        angleZ = 0.0f;
        // ボードオブジェクトを回転する
        transform.rotation = Quaternion.Euler( angleX, 0f, angleZ);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angleX = -1.0f * SerialRead.angleY / 11.4f;
        angleZ = -1.0f * SerialRead.angleX / 11.4f;

        // ボードオブジェクトを回転する
        transform.rotation = Quaternion.Euler( angleX, 0f, angleZ);
    }
    
}
