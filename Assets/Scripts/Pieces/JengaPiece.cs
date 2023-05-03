using System;
using System.Collections;
using System.Collections.Generic;
using Building;
using TMPro;
using UnityEngine;

public abstract class JengaPiece : MonoBehaviour
{
    protected Rigidbody _rb;
    private Renderer _renderer;

    protected abstract string MaterialName { get; }
    protected abstract string PieceLabel { get; }
    protected abstract Color LabelColor { get; }

    public JengaPieceData JengaPieceData { get; set; }
    protected virtual void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _rb = GetComponentInChildren<Rigidbody>();
        Material material = Resources.Load<Material>(MaterialName);
        _renderer.material = material;
        var textMesh = GetComponentInChildren<TextMeshPro>();
        textMesh.text = PieceLabel;
        textMesh.color = LabelColor;
        _rb.gameObject.tag = "Selectable";
    }
    protected virtual void OnEnable()
    {
        GameManager.OnEnablePhysics += HandleOnEnablePhysics;
    }

    private void OnDisable()
    {
        GameManager.OnEnablePhysics -= HandleOnEnablePhysics;
    }

    private void HandleOnEnablePhysics()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;
    }
}