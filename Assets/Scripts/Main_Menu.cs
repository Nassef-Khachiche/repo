using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource sourceMusic;
    public AudioSource sourceButton;

    public void PlayGame() 
    {
        StartCoroutine(PlaySound());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator PlaySound()
    {
        sourceMusic.Stop();
        sourceButton.Play();
        yield return new WaitForSeconds(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
