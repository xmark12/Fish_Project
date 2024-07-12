using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjectsMine = new List<GameObject>();
    private List<GameObject> pooledObjectsHook = new List<GameObject>();
    private List<GameObject> pooledObjectsNet = new List<GameObject>();

    private int amountToPool = 10;

    [SerializeField] private GameObject mineObject;
    [SerializeField] private GameObject hookObject;
    [SerializeField] private GameObject netObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(mineObject);
            obj.SetActive(false);
            pooledObjectsMine.Add(obj);
        }

        for (int j = 0; j < amountToPool; j++)
        {
            GameObject obj = Instantiate(hookObject);
            obj.SetActive(false);
            pooledObjectsHook.Add(obj);
        }

        for (int k = 0; k < amountToPool; k++)
        {
            GameObject obj = Instantiate(netObject);
            obj.SetActive(false);
            pooledObjectsNet.Add(obj);
        }
    }

    public GameObject GetPooledObjectMine()
    {
        for (int i = 0; i < pooledObjectsMine.Count; i++)
        {
            if (!pooledObjectsMine[i].activeInHierarchy)
            {
                return pooledObjectsMine[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObjectHook()
    {

        for (int i = 0; i < pooledObjectsHook.Count; i++)
        {
            if (!pooledObjectsHook[i].activeInHierarchy)
            {
                return pooledObjectsHook[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObjectNet()
    {

        for (int i = 0; i < pooledObjectsNet.Count; i++)
        {
            if (!pooledObjectsNet[i].activeInHierarchy)
            {
                return pooledObjectsNet[i];
            }
        }

        return null;
    }
}
