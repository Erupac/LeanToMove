using UnityEngine;
using System.Collections;

public abstract class PivotMove : MonoBehaviour {
    public GameObject pivotObject; // An ingame representation of the pivot.
    public float maxSpeed = 3f; // The maximum speed that can be achieved
    public float gain = 10f; // The sensitivity of the mechanic for speed
    public float threshold = 0.15f; // The threshold lean distance below which nothing happens
    public LayerMask mask;
    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;

    private Vector3 UP = new Vector3(0, 1, 0);

    public bool activated { get; set; }
    private Transform pivot;
    private Transform headPosition;
    private Transform cameraRig;
    
    void Awake()
    {
        activated = false;

        cameraRig = GameObject.Find("[CameraRig]").transform;
        headPosition = GameObject.Find("Camera (eye)").transform;

        if (pivotObject == null)
        {
            pivot = new GameObject().transform;
            pivot.position = Vector3.zero;

        }
        else
        {
            pivot = pivotObject.transform;
        }
        pivot.parent = cameraRig;

        leftController.TriggerClicked += toggleActivated;
        leftController.TriggerUnclicked += toggleActivated;
        rightController.TriggerClicked += toggleActivated;
        rightController.TriggerUnclicked += toggleActivated;
    }

    private void toggleActivated(object sender, ClickedEventArgs e)
    {
        if(this.activated)
        {
            this.activated = false;
        }
        else
        {
            this.pivot.position = headPosition.position;
            this.activated = true;
        }
    }

    /*
     * Updates the position of the camera with each time steap
     */
    void FixedUpdate()
    {
        if(activated)
        {
            Vector3 displacement = getDisplacement(headPosition.position);
            updateCameraPosition(displacement, headPosition.rotation.eulerAngles, cameraRig);
        }
        fixCameraElevation(headPosition.position, cameraRig);
    }

    /*
     * Mutates the CameraRig to be at a constant elevation above the landscape.
     * Args:
     * newPosition - The world position of the camera head.
     * cameraObject - The transform to move flush with the terrain.
     */
    private void fixCameraElevation(Vector3 headPosition, Transform cameraObject)
    {
        RaycastHit hitTo;
        bool hits = Physics.Raycast(headPosition, -UP, out hitTo, 10000, mask);
        if(hits)
        {
            Vector3 oldPos = cameraObject.position;
            oldPos.y = hitTo.point.y;
            cameraObject.position = oldPos;
        }
    }

    /*
     * Calculates the displacement of 'cameraPosition' from the current pivot.
     */
    private Vector3 getDisplacement(Vector3 cameraPosition)
    {
        Vector3 displacement = cameraPosition - pivot.position;

        // Only concerned with the x and y of displacement.
        displacement.y = 0;

        // Create a boundary at a distance of 'threshold' from the pivot where no motion is caused.
        if (displacement.magnitude > threshold)
        {
            displacement = (displacement - (displacement.normalized * threshold)) * gain * Time.fixedDeltaTime;

            // Limit the max speed that the player can go
            if(displacement.magnitude > maxSpeed * Time.fixedDeltaTime)
            {
                displacement = displacement.normalized * maxSpeed * Time.fixedDeltaTime;
            }
            return displacement;
        }
        else
        {
            return Vector3.zero;
        }

    }

    /*
     * Sets the pivot's position to 'newPivot'
     */
    public void setPivot(Vector3 newPivot)
    {
        pivot.position = newPivot;
    }

    /*
     * Updates the transform of the cameraRig according to the pivotDisplacement and the headLookDirection.
     */
    protected abstract void updateCameraPosition(Vector3 pivotDisplacement, Vector3 headLookDirection, Transform cameraRig);
}
