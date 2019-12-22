using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialToLogin : MonoBehaviour
{
      
    // Update is called once per frame
    public void ToLogin()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Login", UnityEngine.SceneManagement.LoadSceneMode.Additive).completed += InitialToLogin_completed;
    }

    private void InitialToLogin_completed(AsyncOperation obj)
    {
        GameObject.Destroy(gameObject);
    }
}
