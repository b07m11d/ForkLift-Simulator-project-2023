//將要用到的函示庫引入
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

public class ArduinoPort : MonoBehaviour
{
    public static ArduinoPort instance;
    public  int pot1;
    public  int pot2;

    //設定基本參數
    private string portName = "COM3";

    int setBaudRate = 115200;
    Parity parity = Parity.None;
    int dataBits = 8;
    StopBits stopBits = StopBits.One;

    SerialPort serialPort = null;

    public int isClosePort = 1;

    public string PortName { get => portName; set => portName = value; }

    // open the port 
    void Start()
    {
        instance = this;
        //string portName_1 = portName;        
        OpenPort();
    }

    void Update()
    {
        ReadData();        
    }


    public void OpenPort()
    {
        serialPort = new SerialPort(portName, setBaudRate, parity, dataBits, stopBits);

        isClosePort = 0;
        // check whether port is open 
        try
        {
            serialPort.Open();
            Debug.Log("Open Port Success!");
        }
        catch (Exception e)
        {
            //Debug.Log(Exception e);
            Debug.Log(e.Message);

        }
    }

    public void ClosePort()
    {
        isClosePort = 1;
        try
        {
            serialPort.Close();
            Debug.Log("Close Port Success!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    void OnApplicationQuit()
    {
        serialPort.Close();
    }
    public void ReadData()
    {
        if (serialPort.IsOpen)
        {

            string a = serialPort.ReadLine();
            string[] values = a.Split('@');

            /*if (values.Length == 2)
            {
                if (values[0] == "1")
                    pot1 = 1;
                else if (values[0] == "-1")
                    pot1 = -1;
                else if (values[0] == "0")
                    pot1 = 0;

                if (values[1] == "1")
                    pot2 = 1;
                else if (values[1] == "-1")
                    pot2 = -1;
                else if (values[1] == "0")
                    pot2 = 0;
                //Debug.Log("pot1:" + int.Parse(values[0]));
                //Debug.Log("pot2:" + int.Parse(values[1]));
                //pot1 = Convert.ToInt32(values[0]);
                //pot2 = int.Parse(values[1]);
                //Debug.Log("Pot 1 = " + pot1+"；Pot 2 = " + pot2);
            }*/

            if (values.Length == 2)
            {
                pot1 = int.Parse(values[0]);
                pot2 = int.Parse(values[1]);
                Debug.Log("Pot 1 = " + pot1 + "；Pot 2 = " + pot2);
            }

        }
    }

}



