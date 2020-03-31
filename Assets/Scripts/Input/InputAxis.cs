using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputAxis
{

    [Header("缓冲帧")]
    public float bufferCounter;
    public float speedBuffer = 15;
    public float slowBuffer = 8;

    bool isStop;
    int forward;

    [Header("加速曲线")]
    public AnimationCurve moveCurve, stopCurve;

    public InputButton posButton, negButton;

    public float Value { get; protected set; }
    public bool ReceivingInput { get; protected set; }
    public bool Enabled { get { return m_Enabled; } }

    protected bool m_Enabled = true;
    protected bool m_GettingInput = true;


    public InputAxis(InputButton posButton, InputButton negButton)
    {
        this.posButton = posButton;
        this.negButton = negButton;
    }

    public void AxisUpdate()
    {
        posButton.Get();
        negButton.Get();

        if (posButton.Held && negButton.Held)
        {
            bufferCounter = 0;
            Value = 0;
            return;
        }

        Value = isStop ? stopCurve.Evaluate(bufferCounter / slowBuffer) :
            moveCurve.Evaluate(bufferCounter / speedBuffer);

        Value *= forward;

    }

    public void AxisFiexedUpdate()
    {
        if (!posButton.Held && !negButton.Held)
        {
            isStop = true;
            if (bufferCounter >= slowBuffer)
            {
                bufferCounter = slowBuffer;
            }
            if (bufferCounter > 0)
                bufferCounter--;
        }
        else
        {
            isStop = false;

            if (bufferCounter < speedBuffer)
            {
                bufferCounter++;
            }
            if(posButton.Held) forward=1;
            else if (negButton.Held) forward=-1;
        }


    }
}

