using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ButtonType
{
    LeftButton,
    RightButton,
    JumpButton,
    ShootButton,
}

public class InputButton
{
   protected ButtonType buttonType;
   protected KeyCode kecyCode;

    public bool Down { get; protected set; }
    public bool Held { get; protected set; }
    public bool Up { get; protected set; }
    public bool Enabled
    {
        get { return m_Enabled; }
    }

    [SerializeField]
    protected bool m_Enabled = true;
    protected bool m_GettingInput = true;

    //This is used to change the state of a button (Down, Up) only if at least a FixedUpdate happened between the previous Frame
    //and this one. Since movement are made in FixedUpdate, without that an input could be missed it get press/release between fixedupdate
    bool m_AfterFixedUpdateDown;
    bool m_AfterFixedUpdateHeld;
    bool m_AfterFixedUpdateUp;


    public InputButton(ButtonType buttonType,KeyCode keyCode)
    {
       this.buttonType=buttonType;
       this.kecyCode=keyCode; 
    }

    public void Get()
    {
        Down = Input.GetKeyDown(kecyCode);
        Held = Input.GetKey(kecyCode);
        Up = Input.GetKeyUp(kecyCode);

    }

}

