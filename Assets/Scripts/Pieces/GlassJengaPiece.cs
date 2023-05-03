using System;
using Building;
using UnityEngine;

public class GlassJengaPiece : JengaPiece
{
    protected override string MaterialName => "Glass";
    protected override string PieceLabel => "To Explore";
    protected override Color LabelColor => new Color(0.6f, 0.6f, 0.6f);

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
        pieceModel.SetActive(false);
    }
}