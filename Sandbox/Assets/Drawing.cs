using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lr;
    public Fourier fourierscript;
    int count = 0;
    private List<Vector3> positions;
    void Start()
    {
        if(lr == null)
        {
            lr = gameObject.GetComponent<LineRenderer>();
        }
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        count = 0;
        lr.startColor = Color.blue;
        lr.endColor = Color.blue;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = count + 1;
        if (fourierscript.OrderCircles[fourierscript.OrderCircles.Length - 1] == null) return;
        lr.SetPosition(count, new Vector3(fourierscript.OrderCircles[fourierscript.OrderCircles.Length-1].Top.x, fourierscript.OrderCircles[fourierscript.OrderCircles.Length-1].Top.y, 0.1f));
        count++;

        if (count >= (int)((1/(fourierscript.scalar*0.01)))) count = 0;
    }
}
