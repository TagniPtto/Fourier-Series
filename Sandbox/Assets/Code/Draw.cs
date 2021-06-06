using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera cam;
    public List<Vector2> DrawingPositions;
    // Start is called before the first frame update
    public bool CanDraw = true;
    public float Scale = 1;
    public LineRenderer lr;

    public float maxSpaceBetweenPoints = 0.1f;
    public Vector3 cursoroffset;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
        lr.startColor = Color.black;
        lr.endColor = Color.black;

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        DrawingPositions = new List<Vector2>();
        
    }
    public void Reset()
    {
        DrawingPositions.Clear();
        lr.positionCount = DrawingPositions.Count;
        CanDraw = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Reset();
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
            //py goes from 0.8 - 1.167 or something
            p.y -= 0.985f;


            p *= 5 * Scale;
            p -= cursoroffset;
            DrawingPositions.Add(p);
            lr.positionCount = DrawingPositions.Count;

            lr.SetPosition(0, DrawingPositions[DrawingPositions.Count - 1]);
        }
        if (CanDraw && (Input.GetKey(KeyCode.Mouse0)))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
            //py goes from 0.8 - 1.167 or something
            p.y -= 0.985f;


            p *= 5 * Scale;
            p -= cursoroffset;
            //Debug.Log(p.x+ "," + p.y + "," + p.z);

            float x = p.x - DrawingPositions[DrawingPositions.Count - 1].x;
            float y = p.y - DrawingPositions[DrawingPositions.Count - 1].y; 
            float dist = Mathf.Sqrt(y*y +(x*x));
            if (dist >=maxSpaceBetweenPoints)
            {
                DrawingPositions.Add(p);
                lr.positionCount = DrawingPositions.Count;

                lr.SetPosition(DrawingPositions.Count - 1, DrawingPositions[DrawingPositions.Count - 1]);
            }
           
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CanDraw = false;
        }

    }
}
