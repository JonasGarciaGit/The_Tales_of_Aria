using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{

    public Text textWriter;
    public float delayWriter = 0.1f;
    public string escrevaFrase;
    public bool corroutineIsDone;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("habilitarTexto");
    }

    IEnumerator mostrarTexto(string textType)
    {
        textWriter.text = "";
        for(int letter = 0; letter < textType.Length; letter++)
        {
            textWriter.text = textWriter.text + textType[letter];

            yield return new WaitForSeconds(delayWriter); 
        }
        corroutineIsDone = true;
    }

    IEnumerator habilitarTexto()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine("mostrarTexto", escrevaFrase);
    }

}
