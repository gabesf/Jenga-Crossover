using System;
using API;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.DataDisplay
{
    public class PieceSelector : MonoBehaviour
    {
        private Transform _highlight;
        private Transform _selection;
        private RaycastHit _raycastHit;
        private Camera _camera;

        public static Action<JengaPieceData> OnJengaPieceSelected;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            InfoBillboard.OnInfoBillboardClosed += HandleOnInfoBillboardClosed;
        }

        private void HandleOnInfoBillboardClosed()
        {
            DisableOutline();
        }

        private void Update()
        {
            UpdateHighlight();
            UpdateSelection();
        }

        private void UpdateHighlight()
        {
            if (_highlight != null)
            {
                _highlight.gameObject.GetComponent<Outline>().enabled = false;
                _highlight = null;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (IsGameObjectHit(ray))
            {
                _highlight = _raycastHit.transform;
                if (_highlight.CompareTag("Selectable") && _highlight != _selection)
                {
                    HighlightGameObject(_highlight.gameObject);
                }
                else
                {
                    _highlight = null;
                }
            }
        }

        private void UpdateSelection()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_highlight)
                {
                    SelectGameObject(_highlight.gameObject);
                }
                else
                {
                    DisableOutline();
                }
            }
        }

        private void HighlightGameObject(GameObject gameObject)
        {
            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Outline outline = gameObject.AddComponent<Outline>();
                outline.enabled = true;
                outline.OutlineColor = Color.black;
                outline.OutlineWidth = 7.0f;
            }
        }

        private void SelectGameObject(GameObject gameObject)
        {
            if (_selection != null)
            {
                _selection.gameObject.GetComponent<Outline>().enabled = false;
            }

            _selection = _raycastHit.transform;
            _selection.gameObject.GetComponent<Outline>().enabled = true;
            var pieceData = _selection.gameObject.GetComponentInParent<JengaPiece>();

            _highlight = null;

            if (pieceData != null)
            {
                OnJengaPieceSelected?.Invoke(pieceData.JengaPieceData);
            }
            else
            {
                throw new UnityException(
                    $"Unexpected Object ({_selection.gameObject.name}) not containing Jenga Piece Data");
            }
        }

        private bool IsGameObjectHit(Ray ray)
        {
            return !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit);
        }

        private void DisableOutline()
        {
            if (_selection)
            {
                _selection.gameObject.GetComponent<Outline>().enabled = false;
                _selection = null;
            }
        }
    }
}
