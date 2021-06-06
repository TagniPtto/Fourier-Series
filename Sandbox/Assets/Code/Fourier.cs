using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fourier : MonoBehaviour
{
    public int CircleCount = 2;
    public ComplexNumber[] coefficients;
    public Circle[] OrderCircles;
    public LineRenderer CycleRenderer;

    public Draw drawscr;



    public float DrawingScale = 2;
    public float dt_Integration = 0.001f;
    public float scalar = 0.1f;
    public float time;
    public void FoSe() {

        coefficients = new ComplexNumber[CircleCount * 2 + 1];

    }
    public void Generate()
    {
        OrderCircles = new Circle[coefficients.Length];
        time = 0;

        if(CycleRenderer == null)
            CycleRenderer = this.gameObject.GetComponent<LineRenderer>();
        CycleRenderer.positionCount = OrderCircles.Length + 1;
        CycleRenderer.startColor = Color.blue;
        CycleRenderer.endColor = Color.blue;
        CycleRenderer.startWidth = 0.1f;
        CycleRenderer.endWidth = 0.1f;


        int NumbOfCirc = (int)((OrderCircles.Length - 1) / 2);
        OrderCircles[0] = new Circle(0, CalcSquareCOefficients(0));
        for (int i = 1; i< NumbOfCirc; i++)
        {

            OrderCircles[2*i] = new Circle(i, CalcSquareCOefficients(i));
            OrderCircles[2*i-1] = new Circle(-i, CalcSquareCOefficients(-i));       
        }

    }

    public void Update()
    {
        time += 0.01f * scalar;
        
        for (int i = 1; i < OrderCircles.Length; i++)
        {
            OrderCircles[i - 1].Rotate(time);
            if ((OrderCircles[i] == null) || (OrderCircles[i - 1] == null))
            {
                Debug.Log("Yaa shit het is hiere weer");
                return;
            }
            OrderCircles[i].Center = OrderCircles[i - 1].Top;
            CycleRenderer.SetPosition(i - 1, OrderCircles[i - 1].Center);

        }
        OrderCircles[OrderCircles.Length-1].Rotate(time);
        CycleRenderer.SetPosition(CycleRenderer.positionCount - 2, OrderCircles[OrderCircles.Length-1].Center);
        CycleRenderer.SetPosition(CycleRenderer.positionCount-1, OrderCircles[OrderCircles.Length-1].Top);
        if (time >= 1)
        {
            time = 0;
        }
    }
    private ComplexNumber CalcCUBECOefficients(int frequency)
    {
        float im = 0;
        float re = 0;
        float speed = frequency * 2 * Mathf.PI;

        float a = 1;
        float b = 1;

        //1+ti
        for(float t = -0.125f; t <0.125; t+= dt_Integration)
        {
            re += (a * Mathf.Cos(speed * t)) + (8*t * Mathf.Sin(speed * t));
            im += (8*t * Mathf.Cos(speed * t)) - (a * Mathf.Sin(speed * t));
        }
        //t+i
        for (float t = 0.125f; t < 0.375; t += dt_Integration)
        {
            re += ((-8) * (t - 0.25f) * Mathf.Cos(speed * t)) + (b * Mathf.Sin(speed * t));
            im += (b * Mathf.Cos(speed * t)) - ((-8) * (t - 0.25f) * Mathf.Sin(speed * t));
        }
        //-1+it
        for (float t = 0.375f; t < 0.625; t += dt_Integration)
        {
            re += (-a * Mathf.Cos(speed * t)) + ((-8) * (t - 0.5f) * Mathf.Sin(speed * t));
            im += ((-8) * (t - 0.5f) * Mathf.Cos(speed * t)) - (-a * Mathf.Sin(speed * t));
        }
        //t-i
        for (float t = 0.625f; t < 0.875; t += dt_Integration)
        {
            re += ((8) * (t - 0.75f) * Mathf.Cos(speed * t)) + (-b * Mathf.Sin(speed * t));
            im += (-b * Mathf.Cos(speed * t)) - ((8) * (t - 0.75f) * Mathf.Sin(speed * t));
        }
        re *= DrawingScale;
        im *= DrawingScale;
        return new ComplexNumber(re, im);
    }
    private ComplexNumber CalcSquareCOefficients(int frequency)
    {
        float im = 0;
        float re = 0;
        float speed = -frequency * 2 * Mathf.PI;
        re += (Mathf.Sin((float)(speed * 0.25)) - Mathf.Sin((float)(speed * -0.25))) / (speed); 
        im += (Mathf.Cos((float)(speed * 0.25)) - Mathf.Cos((float)(speed * -0.25))) / (-speed);

        im += (Mathf.Sin((float)(speed * 0.5)) - Mathf.Sin((float)(speed * 0))) / (speed);
        re += (Mathf.Cos((float)(speed * 0.5)) - Mathf.Cos((float)(speed * 0))) / (speed);

        re += (Mathf.Sin((float)(speed * 0.75)) - Mathf.Sin((float)(speed * 0.25))) / (-speed);
        im += (Mathf.Cos((float)(speed * 0.75)) - Mathf.Cos((float)(speed * 0.25))) / (speed);

        im += (Mathf.Sin((float)(speed * 1)) - Mathf.Sin((float)(speed * 0.5))) / (-speed);
        re += (Mathf.Cos((float)(speed * 1)) - Mathf.Cos((float)(speed * 0.5))) / (-speed);

        return new ComplexNumber(re,im);
    }

    private ComplexNumber CalcCUSTOMCOefficients(int frequenty)
    {
        float im = 0;
        float re = 0;
        float dtSTep = 1f/drawscr.DrawingPositions.Count;

        float speed = 2f * Mathf.PI * frequenty;
        for (float t = 0; t < (1f-dtSTep); t += dtSTep)
        {
            int index = Mathf.RoundToInt(t / dtSTep);

                //Debug.Log(index + "," + t + ", " + drawscr.DrawingPositions.Count);
           
            re += ((drawscr.lr.GetPosition(index).x *Mathf.Cos(speed * (t))) + (drawscr.lr.GetPosition(index).y * Mathf.Sin(speed * t))) * dtSTep;
            im += ((drawscr.lr.GetPosition(index).y *Mathf.Cos(speed * t)) - (drawscr.lr.GetPosition(index).x * Mathf.Cos(speed * t))) *dtSTep;
         
        }

        return new ComplexNumber(re,im);
    }
}

[System.Serializable]
public class ComplexNumber 
{
    public float im;
    public float re;
    public ComplexNumber(float real, float imaginary)
    {
        im = imaginary;
        re = real;
    }
};

[System.Serializable]
public class Circle
{
    public float Freq;

    //coefficient
    public float Magn;
    public float StartAngle;

    public float angle;

    public Vector2 Top;
    public Vector2 Center;
    public void Rotate(float t)
    {
        angle = (StartAngle) +(2 * Mathf.PI * Freq * t);
        Top.x = Mathf.Cos(angle) *Magn + Center.x;
        Top.y = Mathf.Sin(angle) *Magn + Center.y;
    }
    public Circle(float Frequenty,ComplexNumber coef)
    {
        Freq = Frequenty;
        Magn = Mathf.Sqrt((coef.re*coef.re)+ (coef.im * coef.im));
        if (Magn != 0) StartAngle = Mathf.Acos(coef.re / Magn);
        else StartAngle = 0;
        Top.x = Mathf.Cos(StartAngle) * Magn;
        Top.y = Mathf.Sin(StartAngle) * Magn;
    }
}