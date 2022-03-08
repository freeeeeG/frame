using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class EventEX : MonoBehaviour
{
    private Timer time = new Timer();

    public static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        time.Elapsed += point.action;
        time.Start();
        time.Interval = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class point
{
    internal static void action(object sender, ElapsedEventArgs e)
    {
        Debug.Log(EventEX.count++ % 32);
    }
}
