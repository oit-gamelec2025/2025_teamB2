using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        StartCoroutine(WaitAndChangeScene());
    }

    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("start");
        SceneManager.LoadScene("description");
	
    }
}
