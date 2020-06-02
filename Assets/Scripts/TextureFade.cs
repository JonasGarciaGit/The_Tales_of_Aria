using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextureFade : MonoBehaviour
{
    public Canvas canvas;
    SpriteRenderer rend;
    private bool nextSceneIsDone;
    public GameObject controllerWriter;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        i = 0;
        StartCoroutine("fadeOut");
    }

    private void Update()
    {
        if(canvas != null && controllerWriter != null)
        {
            nextSceneIsDone = controllerWriter.GetComponent<TypeWriter>().corroutineIsDone;


            if (nextSceneIsDone == true && i == 0)
            {
                i = 1;
                StartCoroutine("LoadNextScene");
            }
        }
       
    }

    IEnumerator FadeIn()
    {
        for(float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
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

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine("FadeIn");
        canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Cena_inicial");
    }

}
