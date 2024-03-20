using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boid : MonoBehaviour
{
    public List<boid> others;
    public List<boidEater> boidEaters;
    public Vector2 direction;
    public float speed;
    public Color color;
    public SpriteRenderer spr;

    
    public Slider speedSlider;
    // Start is called before the first frame update
    void Start()
    {
        GameObject [] temp = GameObject.FindGameObjectsWithTag("boid");
        for(int i = 0; i < temp.Length; i++){
            others.Add(temp[i].GetComponent<boid>());
        }  
        color = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));
        spr.color = color; 
        temp = GameObject.FindGameObjectsWithTag("boidEater");
        for(int i = 0; i < temp.Length; i++){
            boidEaters.Add(temp[i].GetComponent<boidEater>());
        }  

    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "box"){
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateSliderValues();
        MoveToCenter();
        AlignWithOthers();
	    AvoidOtherBoids();
        checkForPredator();
        //rotate
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        //move
        transform.Translate(direction * (speed * Time.deltaTime));
    }
    public float fearDistance;
    public Slider fearSlider;
    
    public float speedMax;
    public float speedMin;
    void checkForPredator(){
        for(int i = 0; i < boidEaters.Count; i++){
            float distance = Vector2.Distance(boidEaters[i].transform.position, transform.position);
            if(distance <= fearDistance){
                speed = speedMax;
            }else{
                speed = speedMin;
            }
        }

    }
    public float moveToCenterStrength = 0.005f;
    public float localBoidsDistance;
    public Slider centerStrengthSlider;
    public Slider localBoidsDistanceSlider;

    void MoveToCenter(){
        Vector2 center = transform.position;
        int count = 0;
        for(int i = 0; i < others.Count; i++){
            float distance = Vector2.Distance(others[i].transform.position, transform.position);
            if(distance <=localBoidsDistance){
                center += (Vector2)others[i].transform.position;
                count++;
            }
        }
        if(count == 0){
            return;
        }
        Vector2 positionAverage = center/count;
        positionAverage = positionAverage.normalized;
        Vector2 faceDirection = (positionAverage - (Vector2) transform.position).normalized;

        float timeStrength = moveToCenterStrength * Time.deltaTime;
        direction = direction+timeStrength*faceDirection/(timeStrength+1);
        direction = direction.normalized;
    }
    public float avoidOtherStrength;
    public float collisionAvoidCheckDistance;

    public Slider avoidOtherStrengthSlider;
    public Slider collisionAvoidCheckDistanceSlider;

    void AvoidOtherBoids(){
        Vector2 awayVector = Vector2.zero;
        for(int i = 0; i < others.Count; i++){
            float distance = Vector2.Distance(others[i].transform.position, transform.position);
            if(distance <=collisionAvoidCheckDistance){
                awayVector += (Vector2)(others[i].transform.position-transform.position);
            }
            awayVector = awayVector.normalized;

            direction=direction+avoidOtherStrength*awayVector/(avoidOtherStrength +1);
	        direction = direction.normalized;
        }
    }
    public float alignWithOthersStrength;
    public float alignmentCheckDistance;
    public Slider alignWithOthersStrengthSlider;
    public Slider alignmentCheckDistanceSlider;

    void AlignWithOthers(){
        Vector2 directionSum = Vector2.zero;
        int count = 0;

        for(int i = 0; i < others.Count; i++){
            float distance = Vector2.Distance(others[i].transform.position, transform.position);
            if(distance <=alignmentCheckDistance){
                directionSum += others[i].direction;
                count++;
            }
        }

        Vector2 aveDir = directionSum/count;
        aveDir = aveDir.normalized;

        float timeStrength = alignmentCheckDistance * Time.deltaTime;
        direction = direction+timeStrength*aveDir/(timeStrength+1);
        direction = direction.normalized;
    }

    void updateSliderValues(){
        speed = speedSlider.value;
        moveToCenterStrength = centerStrengthSlider.value;
        avoidOtherStrength = avoidOtherStrengthSlider.value;
        localBoidsDistance = localBoidsDistanceSlider.value;
        alignmentCheckDistance = alignmentCheckDistanceSlider.value;
        alignWithOthersStrength =alignWithOthersStrengthSlider.value;
        collisionAvoidCheckDistance = collisionAvoidCheckDistanceSlider.value;
    }
}
