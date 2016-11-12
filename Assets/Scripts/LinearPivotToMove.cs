using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPivotToMove : PivotMove
{
    protected override void updateCameraPosition(Vector3 pivotDisplacement, Vector3 headLookDirection, Transform cameraRig)
    {
        cameraRig.position += pivotDisplacement;
    }
}
