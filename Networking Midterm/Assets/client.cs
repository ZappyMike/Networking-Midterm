using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lecture 4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


public class client : MonoBehaviour
{

    public GameObject myCube;

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket client_socket;

    private float[] pos;
    private byte[] bpos;

    public Vector3 prevPos;

    public static void RunClient()
    {
        IPAddress ip = IPAddress.Parse("192.168.0.16");//192.168.2.144" 127.0.0.1);
        remoteEP = new IPEndPoint(ip, 11111);

        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    }

    // Start is called before the first frame update
    void Start()
    {
        myCube = GameObject.Find("Cube");

        RunClient();

        //lec 5
        pos = new float[] { myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z };
        bpos = new byte[pos.Length * 4];

        prevPos = myCube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());

        //Debug.Log("Sent X:" + myCube.transform.position.x);

        if (myCube.transform.position != prevPos)
        {
            pos = new float[] { myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z };

            Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);

            client_socket.SendTo(bpos, remoteEP);

            Debug.Log("sent");
        }

        prevPos = myCube.transform.position;

        Debug.Log(prevPos);
    }
}
