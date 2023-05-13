using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject authors;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start_game()
    {
        SceneManager.LoadScene(1);
    }
    public void about()
    {
        authors.SetActive(true);
    }
    public void exit()
    {
        Application.Quit();
    }
    public void quit_about()
    {
        authors.SetActive(false);
    }
}
