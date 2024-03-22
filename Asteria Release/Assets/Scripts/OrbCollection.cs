using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCollection : MonoBehaviour
{
    public float collectionDistance = 5f;
    public float fillSpeed = 5f;
    public Image fillBar;

    private int orbsCollected = 0;
    private int totalOrbs;
    private bool isCollecting = false;
    private Transform player;

    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Find total number of orbs in the scene
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");
        totalOrbs = orbs.Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isCollecting)
        {
            CollectOrb();
        }
    }

void CollectOrb()
{
    GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");

    foreach (GameObject orb in orbs)
    {
        float distance = Vector3.Distance(player.position, orb.transform.position);

        if (distance <= collectionDistance)
        {
            Destroy(orb);
            orbsCollected++;
            
            // Null check for fillBar
            if (fillBar != null)
            {
                // Access fillAmount property
                fillBar.fillAmount += 0.1f;
            }
            else
            {
                Debug.LogError("fillBar is not assigned.");
            }
        }
    }
}





IEnumerator FillBarOverTime(float startValue, float targetValue, float duration)
{
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        fillBar.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the fill bar reaches the exact target value
    fillBar.fillAmount = targetValue;
}



}
