using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    WAIT, GAMEPLAY, DIE
}


public enum itemType
{
    COIN, REDCOIN, STAIR
}

public enum stageType
{
    A,B,C,D,E
}

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    [HideInInspector]
    public GameState currentState;




    [Header("Text")]
    public Text scoreTXT;
    public Text distanceTXT;

    private int score;
    private int  distance;

    [Header("Player Conf")]
    public Transform player;
    public Transform posicaoColetavel;
    public Transform powerUpsensor;

    private Rigidbody rb;
    private Animator anim;
   
    public float movimentSpeed;
    public float forceJump =10f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isWalk;
    private Vector3 moviment;


    [Header("Config pontosParada")]
    public int idpontoDestino = 1;
    public Transform[] pontosParada;
    private Vector3 posicaoDestino;
    public float velocidadeMudancaCaminho;


    [Header("Estados dos Blocos")]
    private int blocos;  //blocos instânciados 
    public int startBlocos;//blocos iniciais
    public float tamanhoFase;
    public Transform estadoPosicao; //posição que estão os blocos instânciados
   // public GameObject[] estadosBlocos;  //blocos instanciados

    public GameObject[] blocoA;
    public GameObject[] blocoB;
    public GameObject ultimoBloco;
   


    public bool isPoweUp;

    public float percPowerUp;

    private float H;

    private bool verificarApertou; 
    
    private bool verificarApertouEsquerda ;


    [Header("Touch Android")]
    public Touch toque;

   // public GameObject Tamanhocena;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        blocos = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = player.gameObject.GetComponent<Rigidbody>();
        anim = player.gameObject.GetComponent<Animator>();

        posicaoDestino = pontosParada[idpontoDestino].position;

        powerUpsensor.gameObject.SetActive(false);

      

        
        for(int i = 0; i < startBlocos; i ++)
        {
            NovosBlocos();


        }



        if(fadeinOut._instance != null)
        {
            fadeinOut._instance.Fase();
            StartCoroutine("StartStage");
        }
        else
        {
            currentState = GameState.GAMEPLAY;
            anim.SetBool("isWalk" , true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateAnimator();



        if (currentState != GameState.GAMEPLAY) { return; }

        /* if (Input.GetKeyDown(KeyCode.RightArrow))
         {
           InputX(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputX(-1);
        }*//*

        H = Input.GetAxisRaw("Horizontal");





        if (H==1 && verificarApertou == false)
        {
            verificarApertou = true;
            
            InputX(1);
        }


        if (H == 0 && verificarApertou == true)
        {
            verificarApertou = false;

           
        }


        if (H==-1 && verificarApertou == false)
        {
            verificarApertou = true;

            InputX(-1);
        }

        if (H == 0 && verificarApertou == true)
        {
           
            verificarApertou = false;


        }

        if (Input.GetButtonDown("Pulo"))
        {
            Jump();
        }
        */
      //  tamanhoFase = Screen.width;

       // if (Input.touchCount > 0)
       // {
     //    //   toque = Input.GetTouch(0);
         //
          //  if(toque.phase == TouchPhase.Began)
          //  {
           //     if(toque.position.x > (( Screen.width / 2)+2f))
           //     {
           //         InputX(1);
            //    }else if (toque.position.x <( (Screen.width / 2) - 2f))
            //    {
                  
            //InputX(-1);
             //   }
                
        //    }

           // if(toque.phase == TouchPhase.Began)
            //{
             //   if((toque.position.y <( Screen.height / 3) ))
              //  {
              //      if ((toque.position.x > ((Screen.width / 2) -1f) && toque.position.x < (Screen.width / 2) + 1f))
               //     {
                  //      Jump();
                //    }

                //   
               // }
           // }
        //}

        posicaoDestino = new Vector3(pontosParada[idpontoDestino].position.x, player.position.y, player.position.z);

        player.position = Vector3.MoveTowards(player.position, posicaoDestino, velocidadeMudancaCaminho * Time.deltaTime);

        powerUpsensor.position = player.position;



        moviment = new Vector3(0f, rb.velocity.y, movimentSpeed);
        rb.velocity = moviment;

        distance = Mathf.RoundToInt(Vector3.Distance(player.position, estadoPosicao.position));


        if(distance > 100)
        {
            movimentSpeed = 20;
        }

        moviment = new Vector3(0f, rb.velocity.y, movimentSpeed);
        rb.velocity = moviment;
        UpDateHUD();

    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, whatIsGround);
    }

    #region 
    IEnumerator StartStage()
    {
        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);
        currentState = GameState.GAMEPLAY;
        anim.SetBool("isWalk", true);
    }

    public void Jump()
    {
        if (isGrounded == false) { return; }
        audioManage._instance.PlayFX(audioManage._instance.FXJump);
        rb.AddForce(Vector3.up * forceJump);
    }


    public void InputX(int value)
    {
        idpontoDestino += value;
        if(idpontoDestino >= pontosParada.Length)
        {
            idpontoDestino = pontosParada.Length - 1;
        }else if(idpontoDestino < 0)
        {
            idpontoDestino = 0;
        }

        
    }

    void UpdateAnimator()
    {
        anim.SetBool("isGrounded", isGrounded);
       
    }

    public void NovosBlocos()
    {
        GameObject  novoBlocos = null;
        int idbloco = 0;
        
        stageBlocos b = ultimoBloco.GetComponent<stageBlocos>();

        switch (b.type)
        {
            case stageType.A:
                idbloco = Random.Range(0, blocoA.Length);
                novoBlocos = blocoA[idbloco];

                break;
            case stageType.B:
                idbloco = Random.Range(0, blocoB.Length);
                novoBlocos = blocoB[idbloco];
                break;

        }

        ultimoBloco = novoBlocos;
        
        //int idBloco = Random.Range(0, estadosBlocos.Length);

        GameObject temp = Instantiate(novoBlocos, estadoPosicao.position + Vector3.forward * (blocos * tamanhoFase), Quaternion.identity, estadoPosicao);
        blocos++;
    }



    public void morrer()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("distance", distance);

        audioManage._instance.PlayMusic(audioManage._instance.lostLife, false);


        
        if(score > PlayerPrefs.GetInt("hiScore"))
        {
            PlayerPrefs.SetInt("hiScore", score);
        }

        if (distance > PlayerPrefs.GetInt("hiDistance"))
        {
            PlayerPrefs.SetInt("hiDistance", distance);
        }

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        currentState = GameState.DIE;
        anim.SetBool("isDie", true);
        anim.SetTrigger("Die");


        StartCoroutine("Morredo");


    }


    IEnumerator Morredo()
    {
        yield return new WaitForSeconds(3f);
        fadeinOut._instance.Fase();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => fadeinOut._instance.isFadeComplete);
        fadeinOut._instance.GoScene("GameOver");

    }

    public void GetItem(itemType type)
    {
        switch (type)
        {
            case itemType.COIN:

               
                if(audioManage._instance != null)
                {
                    audioManage._instance.PlayFX(audioManage._instance.FXCoin);
                }
                score += 100;


                break;

            case itemType.REDCOIN:

                
                if (audioManage._instance != null)
                {
                    audioManage._instance.PlayFX(audioManage._instance.FXCoin);
                }
                score += 500;
                break;


            case itemType.STAIR:


                if (audioManage._instance != null)
                {
                    audioManage._instance.PlayFX(audioManage._instance.FXCoin);
                    audioManage._instance.PlayMusic(audioManage._instance.powerUp, false);
                }
                isPoweUp = true;
                StartCoroutine("SuperStair");
                score += 500;
                break;
        }
    }
    void UpDateHUD()
    {
        scoreTXT.text = score.ToString("N0");
        distanceTXT.text = distance.ToString("N0") + " m";
    }
    IEnumerator SuperStair()
    {
        powerUpsensor.gameObject.SetActive(true);
        yield return new WaitUntil(() => !audioManage._instance.Music.isPlaying);
        isPoweUp = false;
        powerUpsensor.gameObject.SetActive(false);
        audioManage._instance.PlayMusic(audioManage._instance.musicaFase, true);
    }

    #endregion



}
