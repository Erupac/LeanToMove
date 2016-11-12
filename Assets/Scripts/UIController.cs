using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Slider leanGain;
    public Slider leanThresh;
    public Slider maxSpeed;
    public LinearPivotToMove moveScript;
    public Text leanGainValue;
    public Text leanThreshValue;
    public Text maxSpeedValue;

	// Use this for initialization
	void Start () {
        leanGain.value = moveScript.gain;
        leanThresh.value = moveScript.threshold;
        maxSpeed.value = moveScript.maxSpeed;
        maxSpeedValue.text = maxSpeed.value.ToString();
        leanThreshValue.text = leanThresh.value.ToString();
        leanGainValue.text = leanGain.value.ToString();

    }

    public void setMaxSpeed()
    {
        moveScript.maxSpeed = maxSpeed.value;
        maxSpeedValue.text = maxSpeed.value.ToString();
    }

    public void setLeanThresh()
    {
        moveScript.threshold = leanThresh.value;
        leanThreshValue.text = leanThresh.value.ToString();
    }

    public void setLeanGain()
    {
        moveScript.gain = leanGain.value;
        leanGainValue.text = leanGain.value.ToString();
    }
}
