using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{

    public SpriteRenderer sr;

    public Sprite customerSprite;
    public Sprite orderSprite;
    public float waitTime;
    
    private float requestDelay;
    private float currentWaitTime;
    private bool hasRequestedOrder = false;
    private bool orderFailed = false;
    
    public UnityEvent onOrderFailed;
    
    void Start()
    {
        sr.sprite = customerSprite;
        requestDelay = Random.Range(0f, 2f);
        currentWaitTime = waitTime;
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
    }

    IEnumerator RequestOrderAfterDelay()
    {
        yield return new WaitForSeconds(requestDelay);
        RequestOrder();
    }

    void RequestOrder()
    {
        hasRequestedOrder = true;
        Debug.Log("Customer requested order!");
    }

    void OrderFailed()
    {
        orderFailed = true;
        onOrderFailed?.Invoke();
        Debug.Log("Customer order failed!");
    }

    public void Initialize(Sprite cs, Sprite os, float w)
    {
        customerSprite = cs;
        orderSprite = os;
        waitTime = w;
        sr.sprite = customerSprite;
    }
}
