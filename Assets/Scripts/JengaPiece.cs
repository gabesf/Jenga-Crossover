using System;
using System.Collections;
using System.Collections.Generic;
using Building;
using UnityEngine;

public abstract class JengaPiece : MonoBehaviour
{
    private Rigidbody _rb;
    private Renderer _renderer;

    public abstract string MaterialName { get; }
    
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _rb = GetComponentInChildren<Rigidbody>();
        Material material = Resources.Load<Material>(MaterialName);

        _renderer.material = material;
    }
    private void OnEnable()
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

public class GlassJengaPiece : JengaPiece
{
    
    

    public override string MaterialName => "Glass";
}

public class WoodJengaPiece : JengaPiece
{
    public override string MaterialName => "Wood";
 
}

public class StoneJengaPiece : JengaPiece
{
    public override string MaterialName => "Stone";
}
