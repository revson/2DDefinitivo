using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private _GameController _GameController;

	private SpriteRenderer spriteRenderer;
	public Sprite[] imagemObjeto;
	public bool open; 

	// Use this for initialization
	void Start () {
		//usar o find somente qaundo houver apenas um objeto com este nome
		_GameController = FindObjectOfType (typeof(_GameController)) as _GameController;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void interacao(){
		
		open = !open;

		switch (open) {
		case true:
			spriteRenderer.sprite = imagemObjeto [1];
			// verifica se o objeto carregou antes da interacao, se nao carrega.
			if (_GameController == null) {
				_GameController = FindObjectOfType (typeof(_GameController)) as _GameController;
			}
			_GameController.Teste += 1;
			break;

		case false:
			spriteRenderer.sprite = imagemObjeto[0];
			break;
		}



	}
}
