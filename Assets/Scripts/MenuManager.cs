using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject stars;
    [SerializeField] GameObject backGround1;
    [SerializeField] GameObject backGround2;
    [SerializeField] GameObject Title;
    [SerializeField] Text pressStart;
    Animator shipAnim;
    Animator starsAnim;


    float progress;
    private float speed = 4f;
    private bool pressed = false;
    AsyncOperation asyncOp;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        StartCoroutine(LoadNewSceneAsync());
        shipAnim = ship.GetComponent<Animator>();
        starsAnim = stars.GetComponent<Animator>();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            pressStart.enabled = false;
            pressed = true;
            shipAnim.Play("ShipTransition");
            starsAnim.Play("StarsAnimation");
            Invoke("ActivateScene", 1.5f);
        }
        if (pressed)
        {
            Vector3 targetPosition1 = new Vector3(0f, 0f, 0f);
            Vector3 backgroundPosition1 = backGround1.transform.position;
            backGround1.transform.position = Vector3.MoveTowards(backgroundPosition1, targetPosition1, Time.deltaTime * speed);            
            
            Vector3 targetPosition2 = new Vector3(0f, 20f, 0f);
            Vector3 backgroundPosition2 = backGround2.transform.position;
            backGround2.transform.position = Vector3.MoveTowards(backgroundPosition2, targetPosition2, Time.deltaTime * speed);
        }
    }

    void ActivateScene()
    {
        asyncOp.allowSceneActivation = true;
    }

    IEnumerator LoadNewSceneAsync()
    {
        asyncOp = SceneManager.LoadSceneAsync(1);
        asyncOp.allowSceneActivation = false;
        while (!asyncOp.isDone)
        {
            // При allowSceneActiovation = false; 
            // максимальное значение async.progress - 0.9f
            progress = asyncOp.progress / 0.9f;
            yield return null;
        }
    }
}
