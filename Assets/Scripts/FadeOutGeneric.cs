using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutGeneric : MonoBehaviour
{

    SpriteRenderer rend;
    public GameObject imgInfoPlayer;
    public GameObject imgFundo;
    public GameObject ImgExp;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot31;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        StartCoroutine("fadeOut");
    }


    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -0.5f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.2f);
            imgInfoPlayer.SetActive(true);
            imgFundo.SetActive(true);
            ImgExp.SetActive(true);
            slot3.SetActive(true);
            slot4.SetActive(true);
            slot31.SetActive(true);
        }
    }


}
