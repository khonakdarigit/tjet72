using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] Text text_Version;
    // Start is called before the first frame update
    void Start()
    {
        text_Version.text = $"v : {Application.version}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button_Run_CLick()
    {
        Sito.Instance.Run(true);
    }
    public void Button_Stop_CLick()
    {
        Sito.Instance.Run(false);
    }
    public void Button_Jump_CLick()
    {
        Sito.Instance.Jump();
    }
    public void Button_Reset_CLick()
    {
        SceneManager.LoadScene(0);
    }
    public void Button_Exit_CLick()
    {
        Application.Quit();
    }

}
