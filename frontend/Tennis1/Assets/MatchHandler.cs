using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public UnityEngine.UI.Text Ip;
    public UnityEngine.UI.Text Port;
    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.Slider Number;

    public GameObject ConnectObject;
    public Tennis1.Common.Adsorption.PreparerAdsorber PreparerAdsorber;
    public Agent Agent;

    
    
    public void ChangeAgentStatus(bool connect)
    {
        
        ConnectObject.SetActive(!connect);
    }
    public void Connect()
    {
        Agent.Connect(Ip.text, int.Parse(Port.text));
    }

    public void SignUp()
    {
        Tennis1.Common.Registration reg = default;
        reg.Name = Name.text;
        reg.PlayerNumber = (int)Number.value;
        PreparerAdsorber.SignUp(reg);
    }

    public void ToField()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Login").completed += (oper) => {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Field", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        };
        
        

    }


}
