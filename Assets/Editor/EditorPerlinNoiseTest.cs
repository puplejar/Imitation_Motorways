#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorPerlinNoiseTest
{
    [MenuItem("Tests/Run Perlin Noise Test")]
    public static void RunPerlinNoiseTest()
    {
        PerlinNoise noise = new PerlinNoise(1234);
        float result = noise.PerlinNoise2D(1.0f, 2.0f, 5.0f);

        if (result >= -1f && result <= 1f)
        {
            Debug.Log("✅ Perlin Noise 테스트 통과: " + result);
        }
        else
        {
            Debug.LogError("❌ Perlin Noise 테스트 실패: " + result);
        }
    }
}
#endif