using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{


    public ComputeShader compute;
    //public RenderTexture renderTexture;


    public ComputeBuffer PositionBuffer;

    public int resolution;

    Vector3[,] cubes;
    GameObject[,] cubesObjects;
    public void GenerateTerrain()
    {
        Debug.Log("generating");
        if(cubes == null || cubesObjects == null)
            MakeCubes();
        InitComputeShader();
    }
    public void MakeCubes()
    {
        cubes = new Vector3[resolution,resolution];
        cubesObjects = new GameObject[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {

                cubesObjects[x, y] = new GameObject();
                cubes[x, y] = cubesObjects[x, y].transform.position;
            }
        }
    }
    public void InitComputeShader()
    {
        PositionBuffer = new ComputeBuffer(resolution * resolution, sizeof(float) * 3);
        PositionBuffer.SetData(cubes);
        int kernel = compute.FindKernel("CSMain");
     
        compute.SetBuffer(kernel, "positionBuffer", PositionBuffer);
        compute.Dispatch(kernel, 8,8,1);

    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        /*
        if (renderTexture == null)
        {
            //seting up the texture
            renderTexture = new RenderTexture(256, 256, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        int kernel = compute.FindKernel("CSMain");
        compute.SetTexture(kernel, "Result", renderTexture);
        uint x;
        uint y;
        uint z;
        compute.GetKernelThreadGroupSizes(kernel, out x, out y, out z);
        compute.SetFloat("Width", renderTexture.width);
        compute.SetFloat("Height", renderTexture.height);
        compute.Dispatch(kernel, (int)(renderTexture.width / x), (int)(renderTexture.height / y), 1);
        Graphics.Blit(renderTexture,destination);
        {
            // dispatch () How many threadgroups ar used?

            // rendertextur.width/threadgroupsize.x = amount of threadgroups in x; 15/5 = 3 groups in x
            // rendertextur.height/threadgroupsize.y = amount of threadgroups in y; 15/5 = 3 groups in y
            // an image has no deapth -> amount of threadgroups in z = 1

            // <----------------->

            //  *****|*****|*****  ^
            //  *****|*****|*****  |
            //  *****|*****|*****  |
            //  -----------------  |
            //  *****|*****|*****  |
            //  *****|*****|*****  |
            //  *****|*****|*****  |
            //  -----------------  |
            //  *****|*****|*****  |
            //  *****|*****|*****  |
            //  *****|*****|*****  ^
        }
        */

    }
}