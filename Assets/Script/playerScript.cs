using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	private Animator playerAnimator;
	private Rigidbody2D playerRb;

	public Transform groundCheck; // objeto responsavel por indicar se o personagem esta sobre a superficie
	public LayerMask whatIsGround; //indica o que e superficie para o teste do grounded

	public float speed; // velocidade de movimento do personagem
	public float jumpForce; //velocidade aplicada para gerar o pulo do personagem
	private float h, v; // variaveis que representam o x e y do movimento

	public bool Grounded; //indica se o personagem esta pisando em alguma superficie
	public bool lookLeft; //indica se o personagem esta virado para esquerda
	public bool attacking; //indica se o personagem esta executando um ataque
	public int idAnimation; //indica o id da animacao

	public Collider2D standing, crounching; // colisor em pe e colisor abaixado

	public Transform hand;
	private Vector3 dir = Vector3.right;
	public LayerMask interacao;

	// Use this for initialization
	void Start () {

		playerRb = GetComponent<Rigidbody2D> (); // inicializa o componente via script
		playerAnimator = GetComponent<Animator> (); // inicializa o componente via script

	}

	void FixedUpdate(){ // taxa de atualizacao fixa de 0.02, onde fica os comandos relacionados a fisica

		Grounded = Physics2D.OverlapCircle (groundCheck.position,0.02f, whatIsGround);
		playerRb.velocity = new Vector2 (h * speed, playerRb.velocity.y);

		interagir ();

	}
	
	// Update is called once per frame
	void Update () { //taxa de atualizacao de update e mais rapido

		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		if(h > 0 && lookLeft == true && attacking == false){ 
			//andando para direita e olhando para esquerda
			flip();
			
		}else if(h < 0 && lookLeft == false && attacking == false){
			//andando para esquerda e olhando para direita
			flip();
		}


		if (v < 0) {
			
			idAnimation = 2;
			if(Grounded == true){
				h = 0;
			}

		}else if (h != 0) {
			idAnimation = 1;
		} else {
			idAnimation = 0;
		}

		if (Input.GetButtonDown ("Fire1") && v >= 0 && attacking == false) {
			playerAnimator.SetTrigger ("atack");
		}

		if(Input.GetButtonDown("Jump") && Grounded == true && attacking == false){
			playerRb.AddForce (new Vector2(0, jumpForce));
		}

		if(attacking == true && Grounded == true){
			h = 0;
		}

		// altera o tamanho do box collider quando em pe ou abaixado
		if (v < 0 && Grounded == true) {
			crounching.enabled = true;
			standing.enabled = false;
		} else if(v >= 0 && Grounded == true){
			crounching.enabled = false;
			standing.enabled = true;
		}else if(v != 0  && Grounded == true){
			crounching.enabled = false;
			standing.enabled = true;
		}


		playerAnimator.SetBool ("grounded", Grounded);
		playerAnimator.SetInteger ("idAnimation", idAnimation);
		playerAnimator.SetFloat ("speedY", playerRb.velocity.y);


	}

	void flip(){
		lookLeft = !lookLeft; //inverte o valor da variavel boleana
		float x = transform.localScale.x;
		x *= -1; //multiplicado por -1 inverte o sinal do scale x
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		dir.x = x;
	}

	void atack(int atk){

		switch(atk){

		case 0:
			attacking = false;
			break;
		case 1:
			attacking = true;
			break;
		}

	}

	void interagir(){
		RaycastHit2D hit = Physics2D.Raycast (hand.position, dir, 0.1f, interacao);
		Debug.DrawRay (hand.position, dir * 0.1f, Color.red);

		if(hit == true){

			print (hit.collider.gameObject.name);
		}
	}

}
