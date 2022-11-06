using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    private Animator animator;
    public GameObject monster;
    public GameObject auraVFX;
    private bool playerInRange = false;
    private bool auraOn = false;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!auraOn && playerInRange) {
        auraOn = true;
        Vector3 auraPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
        Instantiate(auraVFX, auraPos, Quaternion.identity);
      }
      
      if (count == 0) {
        count++;
        playerInRange = true;
        animator.SetBool("PlayerInRange", playerInRange);
        StartCoroutine(Spawn());
      }

      if(auraOn && !playerInRange) {
        auraOn = false;
        Destroy(auraVFX);
      }

    }

    IEnumerator Spawn()
    {
      yield return new WaitForSeconds(2f);
      Vector3 randomSpawnPosition = new Vector3(Random.Range(6, 10), 0.7f, Random.Range(-18, -20));
      Instantiate(monster, randomSpawnPosition, Quaternion.identity);
      yield return new WaitForSeconds(7f);
      playerInRange = false;
      animator.SetBool("PlayerInRange", playerInRange);
    }
}
