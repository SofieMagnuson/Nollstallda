using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
  public void StartOver()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
