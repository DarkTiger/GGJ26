using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menu, credits, rules;
    [SerializeField] GameObject play, closecredits;
    [SerializeField] float timerSetChangeScene;
    GameObject myEventSystem;
    bool starting;

    float timer;


    private void Awake()
    {
        myEventSystem = GameObject.Find("EventSystem");
    }

    private void Update()
    {
        if (starting)
        {
            timer += Time.deltaTime;
            if (timer > timerSetChangeScene)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void Closed()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(closecredits);
    }
    public void ClosedCredits()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(play);
    }

    public void Play()
    {
        menu.SetActive(false);
        rules.SetActive(true);
        starting = true;
    }
}
