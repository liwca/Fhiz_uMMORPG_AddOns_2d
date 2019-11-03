using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIFPS : MonoBehaviour
{
    public Text fpsText;

    public float goodThreshold = 90f;
    public float okayThreshold = 60f;

    public Color goodColor = Color.green;
    public Color okayColor = Color.yellow;
    public Color badColor = Color.red;

    protected int fps;

    private void Update()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);

        // change color based on status
        if (fps >= goodThreshold)
            fpsText.color = goodColor;
        else if (fps >= okayThreshold)
            fpsText.color = okayColor;
        else
            fpsText.color = badColor;

        // show latency in milliseconds
        fpsText.text = fps.ToString() + "fps";
    }
}