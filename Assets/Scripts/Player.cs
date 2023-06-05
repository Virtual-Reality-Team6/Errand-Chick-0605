using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string currentMapName;
    public float speed;
    public float jumpUp;
    float hAxis;
    float vAxis;

    bool shiftDown;
    bool spaceDown;
    bool interactionDown;
    bool memoDown;
    bool stampDown;
    bool menuDown;

    public int coin;
    public int score;

    bool isJump;
    bool isShop;
    bool seeMemo;
    bool seeStamp;
    bool seeMenu = false;

    public bool[] iscompletedErrand;
    public bool[] iscompletedStamp;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;

    public RectTransform memoUIGroup;
    public RectTransform stampUIGroup;
    public RectTransform menuUIGroup;

    public SceneLoader sceneLoader;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //PlayerPrefs.SetInt("MaxScore", 112500);
    }
    
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Interaction();
        Memo();
        Stamp();
        Menu();
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shiftDown = Input.GetButton("Run");
        spaceDown = Input.GetButton("Jump");
        interactionDown = Input.GetButtonDown("Interaction");
        memoDown = Input.GetButtonDown("Memo");
        stampDown = Input.GetButtonDown("Stamp");
        menuDown = Input.GetButtonDown("Menu");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * (shiftDown ? 2f : 1f) * Time.deltaTime;

        anim.SetBool("Walk", moveVec != Vector3.zero);
        anim.SetBool("Run", shiftDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (spaceDown && !isJump && !isShop){
            rigid.AddForce(Vector3.up * jumpUp,  ForceMode.Impulse);
            anim.SetBool("Jump", true);
            anim.SetTrigger("Jump");
            isJump = true;
        }
    }

    void Interaction()
    {
        if(interactionDown && nearObject != null){
            if(nearObject.tag == "NPC"){
                NPC npc = nearObject.GetComponent<NPC>();
                npc.Enter(this);
                isShop = true;
            }
        }
    }

    void Memo()
    {
        if(memoDown){
            if(seeMemo) {
                memoUIGroup.anchoredPosition = Vector3.down * 1000;
                seeMemo = false;
            }
            else {
                memoUIGroup.anchoredPosition = Vector3.zero;
                seeMemo = true;
            }
        }
    }

    public void Stamp()
    {
        if(stampDown){
            if(seeStamp) {
                stampUIGroup.anchoredPosition = Vector3.down * 1000;
                seeStamp = false;
            }
            else {
                stampUIGroup.anchoredPosition = new Vector3(718f, 0f, 0f);
                seeStamp = true;
            }
        }
    }

    public void Menu()
    {
        if(menuDown){
            if(seeMenu) {
                menuUIGroup.anchoredPosition = Vector3.left * 1500;
                seeMenu = false;
            }
            else {
                menuUIGroup.anchoredPosition = new Vector3(0f, 0f, 0f);
                seeMenu = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor"){
            isJump = false;
        }
        if(collision.gameObject.CompareTag("Portal")){
            if(sceneLoader == null){
                Debug.Log("sceneLoader가 null");
            }
            else{
                sceneLoader.LoadScene("Market");
            }
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "NPC")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "NPC"){
            NPC npc = nearObject.GetComponent<NPC>();
            npc.Exit();
            isShop = false;
            nearObject = null;
        }
    }
}
