using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllNpc : MonoBehaviour
{
    public Transform npc;
    public SpriteRenderer npcRenderer;
    public Transform[] pontos;
    public float npcSpeed;
    public int idTarget;
    public Animator npcAnimator;

    // Start is called before the first frame update
    void Start()
    {
        npcRenderer = npc.gameObject.GetComponent<SpriteRenderer>();
        npc.position = pontos[0].position;
        idTarget = 1;
        npcAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(npc != null && npcAnimator.GetBool("idle") == false)
        {
            npc.position = Vector3.MoveTowards(npc.position, pontos[idTarget].position, npcSpeed * Time.deltaTime);
            if(idTarget == 0)
            {
                npcRenderer.flipX = true;
            }
            else
            {
                npcRenderer.flipX = false;
            }
            if(npc.position == pontos[idTarget].position)
            {
                StartCoroutine("idlePerSeconds");
                
                idTarget += 1;
                if (idTarget == pontos.Length)
                {
                    idTarget = 0;
                }
            }
        }
    }

    IEnumerator idlePerSeconds()
    {
        npcAnimator.SetBool("idle", true);
        yield return new WaitForSeconds(5.0f);
        npcAnimator.SetBool("idle", false);
        yield return new WaitForSeconds(0.1f);
    }
}
