using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorotineTest : MonoBehaviour
{
    public bool startCou = false;

    IEnumerator CoroutineExample()
    {
        yield return new WaitForSeconds(1);
        Debug.LogError("1");
        yield return new WaitForSeconds(1);
        Debug.LogError("2");
        yield return new WaitForSeconds(1);
        Debug.LogError("3");
        StartCoroutine("CoroutineExample");
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (startCou)
            {
                StartCoroutine("CoroutineExample");
            }
            else
            {
                StopCoroutine("CoroutineExample");
            }
            startCou = !startCou;
        }
    }
}
