using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{

    public SpriteRenderer sr;

    public Sprite customerSprite;
    public Order order;
    public GameObject bubble;
    public ProgressBar progress;
    public float waitTime;
    
    private float requestDelay;
    private float currentWaitTime;
    private bool hasRequestedOrder = false;
    private bool orderFailed = false;
    
    void Start()
    {
        sr.sprite = customerSprite;
        requestDelay = Random.Range(2f, 4f);
        currentWaitTime = waitTime;
        bubble.SetActive(true);
        progress.gameObject.SetActive(true);
        StartCoroutine(RequestOrderAfterDelay());
    }

    void Update()
    {
        if (hasRequestedOrder && !orderFailed)
        {
            currentWaitTime -= Time.deltaTime;
            if (currentWaitTime <= 0)
            {
                OrderFailed();
            }
        }
        progress.UpdateProgress(currentWaitTime/waitTime);
    }

    IEnumerator RequestOrderAfterDelay()
    {
        yield return new WaitForSeconds(requestDelay);
        RequestOrder();
    }

    public void Initialize(Sprite cs, Order o, GameObject b, ProgressBar p, float w)
    {
        customerSprite = cs;
        order = o;
        bubble = b;
        waitTime = w;
        progress = p;
        sr.sprite = customerSprite;
    }

    void RequestOrder()
    {
        hasRequestedOrder = true;
        order.gameObject.SetActive(true);
        bubble.SetActive(false);
    }

    public int GetScore()
    {
        float perfect = RecipeController.instance.GetPerfectTime(order.color);
        
        float percentage = perfect / (waitTime - currentWaitTime);
        if (perfect > waitTime - currentWaitTime)
        {
            percentage = 1;
        }
        
        return (int)(percentage * 100);
    }

    void OrderFailed()
    {
        orderFailed = true;
        Time.timeScale = 0;
        UIController.instance.failScreen.SetActive(true);
        Debug.Log("Customer order failed!");
    }
}
