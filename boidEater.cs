using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidEater : MonoBehaviour
{
    public List<boid> others;
    public Vector2 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject [] temp = GameObject.FindGameObjectsWithTag("boid");
        for(int i = 0; i < temp.Length; i++){
            others.Add(temp[i].GetComponent<boid>());
        }  
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBoid();
        transform.Translate(direction * (speed * Time.deltaTime));
        Debug.DrawRay(transform.position, direction, Color.white);
    }
    void FindClosestBoid(){
        boid closestboid = others[0];
        float closestposition = Vector2.Distance(others[0].transform.position, transform.position);
        foreach(boid b in others){
            float distance = Vector2.Distance(b.transform.position, transform.position);
            Color col = b.color;
            //Debug.Log("Boid "+Vector2.Distance(b.transform.position,transform.position));
            if(distance < closestposition){
                closestboid = b;
                closestposition = distance;
            }
            //Debug.DrawRay(transform.position, b.transform.position - transform.position, b.color);
        }
        direction = closestboid.transform.position - transform.position;
        Debug.Log("Final Boid "+Vector2.Distance(closestboid.transform.position,transform.position));
    }

}
