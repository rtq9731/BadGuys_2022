using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleElement : MonoBehaviour
{
    public event Action<Vector3> OnRotationChanged = null;

}
