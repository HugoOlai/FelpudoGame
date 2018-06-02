using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Principal : MonoBehaviour {
    //desenvolvimento de um jogo na unity
	public GameObject jogador;
	public GameObject Bate;
	public GameObject idle;

	public GameObject barril;
	public GameObject vilaoE;
	public GameObject vilaoD;

	public AudioClip BateAudio;
	public AudioClip MorreAudio;

	public GameObject Barra;

	public Text pontos;
	int i;
	public Text direita;
	public Text esquerda;

	bool LadoPersonagem;
	private List<GameObject> ListaBlocos;
	float escala;

	bool Fim = false;

	// Use this for initialization
	void Start () {
		escala = transform.localScale.x;
		Bate.SetActive (false);
		ListaBlocos= new List<GameObject>();
		Inicio ();


	}
	
	// Update is called once per frame
	void Update () {
		if(!Fim){
			if(Input.GetButtonDown("Fire1"))
			{
				if (Input.mousePosition.x > Screen.width / 2) {
					bateEsquerda ();
					direita.text = " ";
				} else {
					bateDireita ();
					esquerda.text = " ";
				}

				ListaBlocos.RemoveAt (0);
				Repositorio ();
				Confere();
				Barra.SendMessage ("Inicio");
				GetComponent<AudioSource> ().PlayOneShot (BateAudio);
			}
		}

	}

	void bateEsquerda(){
		Bate.SetActive (true);
		idle.SetActive (false);
		LadoPersonagem = true;
		jogador.transform.position = new Vector2(1.1f,jogador.transform.position.y);
		jogador.transform.localScale = new Vector2(-escala, 1);
		Invoke ("Idle", 0.2f);
		ListaBlocos [0].SendMessage ("bateEsquerda");

	}

	void bateDireita(){
		Bate.SetActive (true);
		idle.SetActive (false);
		LadoPersonagem = false;
		jogador.transform.position = new Vector2(-1.1f,0f);
		jogador.transform.localScale = new Vector2(escala,jogador.transform.localScale.y);
		Invoke ("Idle", 0.2f);
		ListaBlocos [0].SendMessage ("bateDireita");
	}

	void Idle()
	{
		Bate.SetActive (false);
		idle.SetActive (true);
	}

	GameObject CriaBarril(Vector2 posicao)
	{
		GameObject NovoBarril;

		if (Random.value > 0.5f || ListaBlocos.Count <3 ) {
			NovoBarril = Instantiate (barril);
		
		} else {
			if (Random.value > 0.5f) {
				NovoBarril = Instantiate (vilaoE);

			} else {

				NovoBarril = Instantiate (vilaoD);
			}

		}

		NovoBarril.transform.position = posicao;
		return NovoBarril;

	}

	void Inicio()
	{
		for (int i = 0; i <= 7; i++) {

			GameObject Ba = CriaBarril(new Vector2(0,-2.1f+(i*0.99f)));
			ListaBlocos.Add (Ba);
		}
	}

	void Repositorio()
	{
		GameObject Ba = CriaBarril(new Vector2(0,-2.1f+(8*0.99f)));
		ListaBlocos.Add (Ba);
		for (int i = 0; i <= 7; i++) {

			ListaBlocos [i].transform.position = new Vector2 (ListaBlocos[i].transform.position.x, ListaBlocos[i].transform.position.y - 0.99f);

		}
	}

	void Confere()
	{
		if (ListaBlocos [0].tag == "inimigo") {
			if (ListaBlocos [0].name == "inimigoEsq(Clone)" && LadoPersonagem == true || ListaBlocos [0].name == "inimigoDir(Clone)" && LadoPersonagem == false) {
				print ("acertei");
				i++;
				pontos.text = "Score: " + i.ToString ();
				Barra.SendMessage ("almentaBarra");
			} else {
				print ("errei");
				FimJogo ();
				GetComponent<AudioSource> ().PlayOneShot (MorreAudio);
			}
		}
	}

	void FimJogo(){
		Fim = true;
		if (LadoPersonagem) {
			Bate.GetComponent<SpriteRenderer> ().color = Color.red;
			idle.GetComponent<SpriteRenderer> ().color = Color.red;
			jogador.GetComponent<Rigidbody2D> ().isKinematic = false;
			jogador.GetComponent<Rigidbody2D> ().velocity = new Vector2 (9f, 1);
			jogador.GetComponent<Rigidbody2D> ().gravityScale = 1.3f;
			jogador.GetComponent<Rigidbody2D> ().AddTorque (-100);
			Invoke ("CarregaCena", 2);
		} else {
			Bate.GetComponent<SpriteRenderer> ().color = Color.red;
			idle.GetComponent<SpriteRenderer> ().color = Color.red;
			jogador.GetComponent<Rigidbody2D> ().isKinematic = false;
			jogador.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-9f, 1);
			jogador.GetComponent<Rigidbody2D> ().gravityScale = 1.3f;
			jogador.GetComponent<Rigidbody2D> ().AddTorque (100);
			Invoke ("CarregaCena", 2);
		}
	}

	void CarregaCena()
	{
		Application.LoadLevel ("1");
	}
}
