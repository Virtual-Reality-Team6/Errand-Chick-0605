using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("보고 싶다. 이렇게 말하니까 더 보고 싶다. 야속한 시간 우리~");
        }
    }
}
