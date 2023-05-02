using System;
using System.Collections;
using System.Collections.Generic;
using Building;
using UnityEngine;

public abstract class JengaPiece : MonoBehaviour
{
    public Rigidbody rb;
    
    private Renderer _renderer;

    public abstract string MaterialName { get; }
    
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        Material material = Resources.Load<Material>(MaterialName);

        if (material == null)
        {
            
        } 
        
        Debug.Log("Got Material");

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
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}

public class GlassJengaPiece : JengaPiece
{
    
    

    public override string MaterialName => "Glass";
}

public class WoodJengaPiece : JengaPiece
{
    public override string MaterialName => "Glass";
 
}

public class StoneJengaPiece : JengaPiece
{
    public override string MaterialName => "Glass";
}
