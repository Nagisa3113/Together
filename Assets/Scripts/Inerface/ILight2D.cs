using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILight2D : IInteractive
{
    void Lighten();
    void UnLighten();
}
