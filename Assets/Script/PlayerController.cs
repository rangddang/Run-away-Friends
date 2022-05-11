using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks//, IPunObservable
{
    public PhotonView PV;

    Rigidbody rigid;
    Renderer renderer;
    //private GameObject mChest;
    //public RectTransform m_ChestBar;
    public GameObject m_HP;
    public Image m_HPBar;
    public Text NickName;
    private Collider chest;
    public GameObject trap;
    public GameObject spider;
    public GameObject rope;

    public float speed;
    float playerSpeed;
    float maxSpeed;
    public float sp;
    public int spMax;
    public float hp;
    public int hpMax;
    public int item;
    bool chestBool;
    float chestMax;
    float chestMin;
    bool Transparent;
    bool itemBool;
    int curse;//저주

    public Vector3 moveVec;
    public Vector3 dir;

    private void Awake()
    {
        if (PV.IsMine)
        {
            rigid = GetComponent<Rigidbody>();
            //mChest = GameObject.Find("UICanvas/ChestBar");
            renderer = gameObject.GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        NickName.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        m_HPBar.color = PV.IsMine ? Color.green : Color.red;
        if (PV.IsMine)
        {
            //item = 7;
            maxSpeed = speed;
            playerSpeed = speed;
            spMax = 60;
            sp = 0;
            hpMax = 50;
            hp = hpMax;
            chestBool = false;
            chestMax = 2;
            //Chest.SetActive(false);
            Transparent = false;
            itemBool = false;
            curse = 0;
            
        }
    }


    void Update()
    {
        if (PV.IsMine)
        {
            MovePlayer();
            Item();
        }
    }

    void MovePlayer()
    {
        //HPBar
        Vector3 hpDir = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0)) - m_HP.transform.position;//화질 안맞으면 깨짐
        m_HP.transform.Translate(hpDir);
        m_HPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (hp / hpMax) * 120);
        //NickName
        Vector3 nameDir = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0)) - NickName.transform.position;
        NickName.transform.Translate(nameDir);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        moveVec = new Vector3(x, 0, y).normalized;
        if (x != 0 || y != 0)
        {
            dir = new Vector3(x, 0, y);
            
        }
            


        //Run
        if (Input.GetKey(KeyCode.LeftShift) && sp > 0 && (x != 0 || y != 0))
        {
            playerSpeed = speed * 1.7f;
            sp -= 15 * Time.deltaTime;
            if (sp < 0) sp = 0;
        }
        else playerSpeed = speed * 1;

        RaycastHit ray;

        //Move
        if (Physics.Raycast(transform.position + moveVec * 0.3f, Vector3.down, out ray, 6))
        {
            transform.Translate(moveVec * playerSpeed * Time.deltaTime);
        }
            
        //Debug.DrawRay(transform.position + moveVec * 0.3f, Vector3.down * 6, Color.blue, 0.3f);


    }

    void Item()
    {
        float reSpawnSec;
        if (chestBool == true && chestMin < chestMax)
        {
            chestMin += 1f * Time.deltaTime;
            //mChest.transform.position = Camera.main.WorldToScreenPoint(chest.transform.position + new Vector3(0, 1f, 0));
            //m_ChestBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (chestMin / chestMax) * 80);
        }
        else if (chestMin >= chestMax)
        {
            if (chest.gameObject.tag == "Chest")
                item = Random.Range(1, 7 + 1);
            else if (chest.gameObject.tag == "CurseChest")
                curse = Random.Range(1, 1 + 1);
            chestMin = 0;
            chestBool = false;
            //mChest.SetActive(false);
            reSpawnSec = chestMax * 20f;
            StartCoroutine(Respawn(chest, reSpawnSec));
        }

        if (item > 0 && Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("아이템" + item + "번 사용");
            switch (item)
            {
                case 1://Stun trap
                    itemBool = false;
                    GameObject instance = Instantiate(trap, transform.position - new Vector3(0, 1f, 0), Quaternion.Euler(0, 45, 0));
                    break;
                case 2://Spider web
                    itemBool = false;
                    GameObject instance1 = Instantiate(spider, transform.position - new Vector3(0, 1f, 0), Quaternion.Euler(0, 45, 0));
                    break;
                case 3://Skateboard
                    StartCoroutine(Fast());
                    break;
                case 4://SpAdd
                    sp += 50;
                    if (sp > 60) sp = 60;
                    break;
                case 5://HpAdd
                    hp += 10;
                    if (hp > 50) hp = 50;
                    break;
                case 6://Invisible
                    StartCoroutine(Invisible());
                    break;
                case 7://Rope
                    itemBool = false;
                    GameObject instance2 = Instantiate(rope, transform.position, Quaternion.Euler(new Vector3(90, (dir.x) * 90, 0)));
                    break;


            }
            item = 0;

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine)
        {
            float reSpawnSec;
            if (other.gameObject.tag == "GameManager")
            {
                transform.position = new Vector3(0, 2, 0);
            }
            if (other.gameObject.tag == "SP" && sp < spMax)
            {
                reSpawnSec = 20f;
                StartCoroutine(Respawn(other, reSpawnSec));
                sp += 6;
                if (sp > spMax) sp = spMax;
            }
            if (other.gameObject.tag == "Chest")
            {
                //mChest.SetActive(true);
                chestMin = 0;
                chestMax = 2;
                chest = other;
                chestBool = true;
            }
            if (other.gameObject.tag == "CurseChest")
            {
                //mChest.SetActive(true);
                chestMin = 0;
                chestMax = 3.5f;
                chest = other;
                chestBool = true;
            }
            if (other.gameObject.tag == "Trap")
            {
                if (itemBool == true)
                {
                    itemBool = false;
                    Destroy(other.gameObject);
                    StartCoroutine(Stun());
                }
                else
                {
                    itemBool = true;
                }
            }
            if (other.gameObject.tag == "SpiderWeb")
            {
                if (itemBool == true)
                {
                    itemBool = false;
                    Destroy(other.gameObject);
                    StartCoroutine(Slow());
                }
                else
                {
                    itemBool = true;
                }
            }
            if (other.gameObject.tag == "Rope")
            {
                if (itemBool == true)
                {
                    itemBool = false;
                    Destroy(other.gameObject);
                    StartCoroutine(Slow());
                }
                else
                {
                    StartCoroutine(RopeMove(other, dir));
                    itemBool = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.gameObject.tag == "Chest")
            {
                chestMin = 0;
                chestBool = false;
                //mChest.SetActive(false);
            }
            if (other.gameObject.tag == "CurseChest")
            {
                chestMin = 0;
                chestBool = false;
                //mChest.SetActive(false);
            }
            if (other.gameObject.tag == "Bush")
            {
                if (Transparent == false)
                    renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.gameObject.tag == "Bush")
            {
                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
            }
        }
    }

    IEnumerator Stun()
    {
        speed = 0f;
        playerSpeed = speed * 1;
        yield return new WaitForSeconds(2f);
        speed = maxSpeed;
        playerSpeed = speed * 1;
    }

    IEnumerator Slow()
    {
        speed = maxSpeed * 0.5f;
        playerSpeed = speed * 1;
        yield return new WaitForSeconds(3f);
        speed = maxSpeed;
        playerSpeed = speed * 1;
    }

    IEnumerator Fast()
    {
        speed = maxSpeed * 1.25f;
        playerSpeed = speed * 1;
        yield return new WaitForSeconds(3f);
        speed = maxSpeed;
        playerSpeed = speed * 1;
    }
    IEnumerator Invisible()
    {
        Transparent = true;
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
        yield return new WaitForSeconds(5f);
        Transparent = false;
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
    }

    IEnumerator Respawn(Collider other, float reSpawnSec)
    {
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(reSpawnSec);
        other.gameObject.SetActive(true);
    }

    IEnumerator RopeMove(Collider other, Vector3 d)
    {
        d = d.normalized;
        for (int i = 0; i < 300; i++)
        {
            other.gameObject.transform.position += d * 0.5f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(other.gameObject);
    }
}
