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
    public float waitTime;
    
    private float requestDelay;
    private float currentWaitTime;
    private bool hasRequestedOrder = false;
    private bool orderFailed = false;
    
    public UnityEvent onOrderFailed;
    
    void Start()
    {
        sr.sprite = customerSprite;
        requestDelay = Random.Range(2f, 4f);
        currentWaitTime = waitTime;
        bubble.SetActive(true);
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

    public void Initialize(Sprite cs, Order o, GameObject b, float w)
    {
        customerSprite = cs;
        order = o;
        bubble = b;
        waitTime = w;
        sr.sprite = customerSprite;
    }

    void RequestOrder()
    {
        hasRequestedOrder = true;
        order.gameObject.SetActive(true);
        bubble.SetActive(false);
    }

    void OrderFailed()
    {
        orderFailed = true;
        onOrderFailed?.Invoke();
        Debug.Log("Customer order failed!");
    }
}
