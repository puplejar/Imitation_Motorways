using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    private int[] permutation = new int[512];
    private System.Random random;

    public PerlinNoise(int seed)
    {
        random = new System.Random(seed);
        int[] perm = new int[256];

        for (int i = 0; i < perm.Length; i++)
        {
            perm[i] = i;
        }
        // Fisher-Yates 셔플
        for (int i = 255; i >= 0; i--)
        {
            int j = random.Next(i + 1);
            (perm[i], perm[j]) = (perm[j], perm[i]);
        }

        for (int i = 0; i < permutation.Length; i++)
        {
            permutation[i] = perm[i % perm.Length];
        }
    }
    
    public float PerlinNoise2D(float x, float y,float noiseScale)
    {
        
        //frequency 
        float frequencyX = x / noiseScale;
        float frequencyY = y / noiseScale;
        
        int Xvertex = (int)Mathf.Floor(frequencyX) & 255;
        int Yvertex = (int)Mathf.Floor(frequencyY) & 255;
        
        float Xgrad = frequencyX - Xvertex;
        float Ygrad = frequencyY - Yvertex;
        
        float fadeLerpX = Fade(Xgrad);
        float fadeLerpY = Fade(Ygrad);

        int aa = permutation[Xvertex] + Yvertex;
        int ab = permutation[Xvertex] + ((Yvertex - 1 + 256) & 255);
        int ba = permutation[(Xvertex+ 255) & 255] + Yvertex;
        int bb = permutation[(Xvertex + 255) & 255] + ((Yvertex - 1 + 256) & 255);

        float lerfx1 = Lerp(Grad(permutation[aa], Xgrad, Ygrad), Grad(permutation[ab], Xgrad, Ygrad), fadeLerpX);
        float lerfx2 = Lerp(Grad(permutation[ba], Xgrad, Ygrad), Grad(permutation[bb], Xgrad, Ygrad), fadeLerpX);
        
        return Lerp(lerfx1, lerfx2,fadeLerpY);
    }

    public float Fade(float T)
    {
        return T * T * T * (T * (6 * T - 15) + 10);
    }

    private float Grad(int hash, float x, float y)
    {
        int h = hash & 3; // 해시 값을 4가지 경우 중 하나로 제한
        float u = h < 2 ? x : y;
        float v = h == 0 || h == 3 ? y : x;
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }

    private float Lerp(float A,float B,float T)
    {
        return A + (B - A) * T;
    }

}
