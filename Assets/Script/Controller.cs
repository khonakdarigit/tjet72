using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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

}
