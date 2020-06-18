using System;
using System.Collections.Generic;
using UnityEngine;

public class _InputHandler : MonoBehaviour
{
    protected static _InputHandler s_Instance;
    public static _InputHandler Instance
    {
        get { return s_Instance; }
    }


    [SerializeField]
    int jumpBuffer = 18, shootBuffer = 10;

    int jumpCounter, shootCounter;


    public bool Shoot
    {
        get { return shootCounter > 0; }
    }
    public bool Jump
    {
        get { return jumpCounter > 0; }
    }


    public InputButton JumpButton = new InputButton(ButtonType.JumpButton, keyDict[ButtonType.JumpButton]);
    public InputButton ShootButton = new InputButton(ButtonType.ShootButton, keyDict[ButtonType.ShootButton]);
    public InputAxis HorizontalAxis = new InputAxis(
        new InputButton(ButtonType.RightButton, keyDict[ButtonType.RightButton]),
        new InputButton(ButtonType.LeftButton, keyDict[ButtonType.LeftButton])
     );


    [SerializeField]
    protected static readonly Dictionary<ButtonType, KeyCode> keyDict = new Dictionary<ButtonType, KeyCode>
    {
        {ButtonType.LeftButton,KeyCode.A},
        {ButtonType.RightButton,KeyCode.D},
        {ButtonType.JumpButton,KeyCode.J},
        {ButtonType.ShootButton,KeyCode.K},
    };


    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            throw new UnityException("There cannot be more than one PlayerInput script.  " +
                "The instances are " + s_Instance.name + " and " + name + ".");
    }

    public void Update()
    {
        JumpButton.Get();
        ShootButton.Get();
        HorizontalAxis.AxisUpdate();
        if (JumpButton.Down)
        {
            jumpCounter = jumpBuffer;
        }
        if (ShootButton.Held)
        {
            shootCounter = shootBuffer;
        }
    }


    public void FixedUpdate()
    {
        HorizontalAxis.AxisFiexedUpdate();

        if (jumpCounter > 0)
        {
            jumpCounter--;
        }

        if (shootCounter > 0)
        {
            shootCounter--;
        }

    }
}