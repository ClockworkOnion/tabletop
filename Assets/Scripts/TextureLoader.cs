using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLoader : MonoBehaviour
{
    public MeshRenderer renderer;
    public MeshRenderer rendererBack;

    public void LoadFromImage(string filename) { 
        byte[] textureData = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + filename);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(textureData);
        PrepareRenderer(renderer, texture);
        PrepareRenderer(rendererBack, texture);
    }

    public void PrepareRenderer(MeshRenderer rend, Texture2D texture) { 
        rend.material.mainTexture = texture;
        rend.material.EnableKeyword("_ALPHABLEND_ON");
        rend.material.EnableKeyword("_ALPHATEST_ON");
        rend.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        rend.material.SetInt("_Mode", 3); // 3 corresponds to Transparent mode
        rend.material.SetFloat("_Glossiness", 0f);
    }
}
