using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{

    public SpriteRenderer sr;

    public Sprite customerSprite;
    public Order order;
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

    public void Initialize(Sprite cs, Order o, float w)
    {
        customerSprite = cs;
        order = o;
        waitTime = w;
        sr.sprite = customerSprite;
    }

    void RequestOrder()
    {
        hasRequestedOrder = true;
        order.gameObject.SetActive(true);
    }

    void OrderFailed()
    {
        orderFailed = true;
        onOrderFailed?.Invoke();
        Debug.Log("Customer order failed!");
    }
}
