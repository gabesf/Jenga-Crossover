using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSelector : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private Camera _camera;

    public static Action<JengaPieceData> OnJengaPieceSelected;
    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        // Highlight
        
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
            
        }
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Clicked");
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }

                
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                var pieceData = selection.gameObject.GetComponent<JengaPiece>();

                
                
                highlight = null;
                
                if (pieceData != null)
                {
                    OnJengaPieceSelected?.Invoke(pieceData.JengaPieceData);
                }
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }
}
