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

    public float OctavePerlin(float x, float y, int octaves, float persistence,float lacunarity)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0; // Used for normalizing result to 0.0 - 1.0

            for (int i = 0; i < octaves; i++)
            {
                total += PerlinNoise2D(x * frequency, y * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= persistence;
                frequency *= lacunarity;
            }
        
        return total / maxValue;
    }

    public float PerlinNoise2D(float x, float y)
    {

        int Xvertex = (int)x & 255;
        int Yvertex = (int)y & 255;
        
        float Xgrad = x - (int)x;
        float Ygrad = y - (int)y;
        
        float fadeLerpX = Fade(Xgrad);
        float fadeLerpY = Fade(Ygrad);

        int X1 = (Xvertex + 1) & 255;
        int Y1 = (Yvertex + 1) & 255;

        int aa = permutation[permutation[Xvertex] + Yvertex];
        int ab = permutation[permutation[Xvertex] + Y1];
        int ba = permutation[permutation[X1] + Yvertex];
        int bb = permutation[permutation[X1] + Y1];
        
        float aagrad = Grad(aa, Xgrad, Ygrad);
        float abgrad = Grad(ab, Xgrad, Ygrad - 1);
        float bagrad = Grad(ba, Xgrad - 1, Ygrad);
        float bbgrad = Grad(bb, Xgrad - 1, Ygrad - 1);
        
        float lerfx1 = Lerp(aagrad, bagrad, fadeLerpX);
        float lerfx2 = Lerp(abgrad, bbgrad, fadeLerpX);
        
        float lerfy1 = Lerp(lerfx1, lerfx2, fadeLerpY);
        
        return lerfy1;
    }

    public float Fade(float T)
    {
        return T * T * T * (T * (6 * T - 15) + 10);
    }

    private float Grad(int hash, float x, float y)
    {
        // int h = hash & 3; // 해시 값을 4가지 경우 중 하나로 제한
        // float u = h < 2 ? x : y;
        // float v = h == 0 || h == 3 ? y : x;
        // return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        switch (hash & 7) // 8가지 방향 사용
        {
            case 0: return  x + y;
            case 1: return -x + y;
            case 2: return  x - y;
            case 3: return -x - y;
            case 4: return  x;
            case 5: return -x;
            case 6: return  y;
            case 7: return -y;
            default: return 0; // 절대 실행되지 않음
        }
    }

    private float Lerp(float A,float B,float T)
    {
        return A + (B - A) * T;
    }

}
