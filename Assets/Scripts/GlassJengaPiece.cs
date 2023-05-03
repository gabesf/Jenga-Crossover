using System;
using Building;
using UnityEngine;

public class GlassJengaPiece : JengaPiece
{
    public override string MaterialName => "Glass";

    private GameObject pieceModel;
    
    protected override void Awake()
    {
        base.Awake();
        pieceModel = _rb.gameObject;
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.OnTestStacks += HandleOnTestStacks;
    }

    private void HandleOnTestStacks()
    {
        
        //_rb.isKinematic = true;
        //_rb.useGravity = false;
        pieceModel.SetActive(false);
        
        Debug.Log($"Turning off");
    }
}