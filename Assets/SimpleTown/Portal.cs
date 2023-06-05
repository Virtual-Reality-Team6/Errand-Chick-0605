using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
  public string transferMapName;
  private Player thePlayer;
  // Start is called before the first frame update
  void Start()
  {
    if (thePlayer == null)
    thePlayer = FindObjectOfType<Player>();
  }
  
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Player"))
    {
      thePlayer.currentMapName = transferMapName;
      SceneManager.LoadScene(transferMapName);
    }
  }
}