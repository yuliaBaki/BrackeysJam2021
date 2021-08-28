using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CloneManager : MonoBehaviour
{
    public static CloneManager Instance { get; private set; }
    private Queue<Queue<Vector3>> positions = new Queue<Queue<Vector3>>();
    private List<GameObject> clones = new List<GameObject>();
    [SerializeField] private int amountOfClones = 20;
    [SerializeField] private GameObject clone;
    private void Awake()
    {
        if (!Instance) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    
    public void AddClonePositions(Queue<Vector3> cloneData)
    {
        var r = new Queue<Vector3>();
        while (cloneData.Count > 0)
        {
            Vector3 test = cloneData.Dequeue();
            r.Enqueue(test);
        }
        positions.Enqueue(r);

        DeleteClones();
        //if (positions.Count > amountOfClones) positions.RemoveAt(0);
        foreach (var q in positions)
        {
            //the duplicate is because the clone dequeues 
            Vector3[] stupid = new Vector3[q.Count];
            q.CopyTo(stupid, 0);
            var t = new Queue<Vector3>(stupid);
            var c = Instantiate(clone, t.Peek(), Quaternion.identity).GetComponent<CloneMovement>();
            if (c) {
                c.SetPositions(t);
            } 
            clones.Add(c.gameObject);
        }
    }
    private void DeleteClones()
    {
        clones.ForEach(x => Destroy(x));
        clones.Clear();
    }
   
}
