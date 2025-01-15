using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialRead : MonoBehaviour
{
    // シリアル通信用定義
    public string portName = "COM16"; //★
    public int baudRate    = 115200; //★
    private static SerialPort serialPort_;

    // シリアル通信で送られてくる加速度データをスターティックに
    public static int angleX;
    public static int angleY;
    private int angleXprev;
    private int angleYprev;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();
        angleX = 0;
        angleXprev = 0;
        angleY = 0;
        angleYprev = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // シリアル読み出し
        if (serialPort_ != null && serialPort_.IsOpen) {
            try {
                string message = serialPort_.ReadLine();
                //Debug.Log(message);

                bool errorFlag = false;
                
                string[] sArr =  message.Split(',');
                if (sArr.Length == 2) {
                    try{
                        angleX = int.Parse(sArr[0]);
                        if ((angleX < -511) || (angleX > 511)) {
                            errorFlag = true;
                        }
                    }
                    catch (System.Exception) {
                        errorFlag = true;
                    };

                    try {
                        angleY = int.Parse(sArr[1]);
                        if ((angleY < -511) || (angleY > 511)) {
                            errorFlag = true;
                        }
                    }
                    catch (System.Exception) {
                        errorFlag = true;
                    };
                    if (!errorFlag) {
                        string slog = sArr[0]+' '+sArr[1];
                        //Debug.Log(slog);
                    }
                    else {
                        angleX = angleXprev;
                        angleY = angleYprev;
                    }
                }
                
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }
}
