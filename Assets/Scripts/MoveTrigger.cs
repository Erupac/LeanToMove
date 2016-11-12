using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour {

    public GameObject head;
    public GameObject pivotObject;
    Transform headPosition;
    Transform pivotPosition;
    PivotMove pivotMove;
    SteamVR_TrackedController controller;

	// Use this for initialization
	void Start () {
        pivotPosition = pivotObject.transform;
        headPosition = head.transform;
        pivotMove = head.GetComponent<PivotMove>();

        controller = this.GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += UpdatePivotPosition;
        controller.TriggerClicked += ActivatePivotMove;
        controller.TriggerUnclicked += DeactivatePivotMove;
	}

    public void UpdatePivotPosition(object sender, ClickedEventArgs e)
    {
        Vector3 newPos = headPosition.position;
        newPos.y = 0;
        pivotPosition.position = newPos;
    }

    public void ActivatePivotMove(object sender, ClickedEventArgs e)
    {
        pivotMove.activated = true;
    }

    public void DeactivatePivotMove(object sender, ClickedEventArgs e)
    {
        pivotMove.activated = false;
    }

}
