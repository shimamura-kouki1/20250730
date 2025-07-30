using UnityEngine;

public class Hello : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        GetComponent<ScriptA>().Hello();
    }
}
