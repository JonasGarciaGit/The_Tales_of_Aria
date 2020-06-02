using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFade : MonoBehaviour
{

    public Canvas canvas;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;

        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut()
    {
        for(float f = 1f; f >= -0.5f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.2f);
            canvas.gameObject.SetActive(true);
        }
    }

}
