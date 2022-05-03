using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PiecePuzzleInteract : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private PiecePuzzleManager manager;
    [SerializeField]
    private PiecePuzzleInput input;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private ClearParticle clearParticle;

    public BoxCollider myCol;
    [SerializeField]
    private Outline outline;
    private bool clear;
    private bool playing;

    private void Awake()
    {
        myCol = GetComponent<BoxCollider>();
        clear = false;
        playing = false;
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        DisAblePuzzle();
    }

    private void DisAblePuzzle()
    {
        input.enabled = false;
        camera.SetActive(false);
        manager.enabled = false;

        clearParticle.ParticleOn();
        UIManager.Instance.OffPuzzleUI();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void AblePuzzle()
    {
        UIManager.Instance.OnPuzzleUI();
        camera.SetActive(true);
        manager.enabled = true;
        input.enabled = true;
        manager.clear.AddListener(DisAblePuzzle);
        outline.enabled = false;

        manager.PuzzleSetting();
        myCol.enabled = false;
    }

    public void Interact(GameObject taker)
    {
        if (clear || playing)
        {
            return;
        }
        else
        {
            AblePuzzle();

            playing = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
