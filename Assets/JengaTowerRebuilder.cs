using System;
using System.Collections;
using System.Collections.Generic;
using Building;
using UnityEngine;

public class IsStackFinished
{
    public IsStackFinished()
    {
        IsFinished = false;
    }
    public bool IsFinished;
}
public class PoseRestore
{
    
    public Quaternion Rotation;
    public Vector3 Position;
    public readonly Transform Transform;
    public readonly Rigidbody Rb;
    public readonly JengaPiece JengaPiece;
    
    
    public PoseRestore(Transform transform)
    {
        Rb = transform.GetComponentInChildren<Rigidbody>();
        JengaPiece = transform.GetComponent<JengaPiece>();

        if (JengaPiece == null)
        {
            Debug.Log($"Could not find Jenga piece in {transform.name}");
        }

        Transform = Rb.transform;
        Rotation = Transform.rotation;
        Position = Transform.position;
    }

    public void RestoreIsKinematic()
    {
        Rb.isKinematic = false;
    }
}


public interface IRebuildStack
{
    void RebuildStack(List<List<PoseRestore>> stacks);
}
public abstract class RebuildData: IRebuildStack
{




    public abstract void RebuildStack(List<List<PoseRestore>> stacks);

    protected void RestoreAllIsKinematic(List<PoseRestore> stack)
    {
        foreach (var piece in stack)
        {
            piece.RestoreIsKinematic();
        }
    }
    
    protected void RestoreGlassPiece(PoseRestore poseRestore)
    {
        if (poseRestore.JengaPiece.GetType() == typeof(GlassJengaPiece))
        {
            poseRestore.Transform.gameObject.SetActive(true);
        }
    }
}

public class InstantaneousRebuild: RebuildData
{
    public override void RebuildStack(List<List<PoseRestore>> stacks)
    {
        Debug.Log("Instantaneous");
        foreach (var stack in stacks)
        {
            RebuiltStack(stack);
        }
    }
    
    private void RebuiltStack(List<PoseRestore> stack)
    {
        foreach (var piece in stack)
        {
            RestorePose(piece);
        }

        RestoreAllIsKinematic(stack);
    }
    
    public void RestorePose(PoseRestore poseRestore)
    {
        poseRestore.Rb.isKinematic = true;
        poseRestore.Transform.position = poseRestore.Position;
        poseRestore.Transform.rotation = poseRestore.Rotation;

        RestoreGlassPiece(poseRestore);

    }

    
}

public class CoroutineHelper : MonoBehaviour
{
    
}
public class DynamicRebuild: RebuildData
{
    public DynamicRebuild(float timeBetweenAnimations, float timeToReturn) : base()
    {
        TimeBetweenAnimations = timeBetweenAnimations;
        TimeToReturn = timeToReturn;
    }

    public float TimeBetweenAnimations;
    public float TimeToReturn;


    public override void RebuildStack(List<List<PoseRestore>> stacks)
    {
        GameObject gameObject = new GameObject();
        MonoBehaviour behaviour = gameObject.AddComponent<CoroutineHelper>();

        behaviour.StartCoroutine(RebuildStacksCoroutine(behaviour, stacks));


    }

    private List<IsStackFinished> _stacksFinishedAnimating;

    private IEnumerator RebuildStacksCoroutine(MonoBehaviour behaviour, List<List<PoseRestore>> stacks)
    {
        _stacksFinishedAnimating = new List<IsStackFinished>();
        //bool allFinished = false;
        for (var index = 0; index < stacks.Count; index++)
        {
            var stack = stacks[index];
            _stacksFinishedAnimating.Add(new IsStackFinished());
            yield return behaviour.StartCoroutine(RebuiltStack(stack, _stacksFinishedAnimating[index]));
        }

        yield return new WaitUntil(() => AllStacksFinished() == true);
        yield return new WaitForSeconds(0.25f);
        
        foreach (var stack in stacks)
        {
            RestoreAllIsKinematic(stack);
            
        }
        foreach (var stack in stacks)
        {
            foreach (var piece in stack)
            {
                RestoreGlassPiece(piece);
            }
        }

        

        

        //yield return new WaitUntil(() => allFinished == true);
    }

    private bool AllStacksFinished()
    {
        foreach (var isStackFinished in _stacksFinishedAnimating)
        {
            if (isStackFinished.IsFinished == false) return false;
        }

        return true;
    }

    private IEnumerator RebuiltStack(List<PoseRestore> stack,  IsStackFinished isStackFinished)
    {
        bool rotationFinished = false;
        bool movementFinished = false;
        foreach (var piece in stack)
        {
            //piece.rb
            piece.Rb.isKinematic = true;
            //Debug.Log($"Restoring {piece.Transform.name}");
            //piece.RestorePose();
            GameObject pieceGameObject = piece.Transform.gameObject;
            LeanTween.move(pieceGameObject, piece.Position, TimeToReturn)
                .setEaseInOutQuad()
                .setOnComplete(() => rotationFinished = true);
            LeanTween.rotate(pieceGameObject, piece.Rotation.eulerAngles, TimeToReturn)
                .setEaseInOutQuad()
                .setOnComplete(() => movementFinished = true);
            //yield return new WaitForSeconds()
            //piece.Transform.position = piece.Position;
            //piece.Transform.rotation = piece.Rotation;
        }
        
        yield return new WaitUntil(() => rotationFinished == movementFinished == true);


        isStackFinished.IsFinished = true;


    }
}
public class JengaTowerRebuilder : MonoBehaviour
{
    private bool _arePositionStored;
    
    private List<List<PoseRestore>> _piecesPosesFromStacks;
    private void OnEnable()
    {
        _piecesPosesFromStacks = new List<List<PoseRestore>>();
        GameManager.OnStacksBuilt += HandleOnStacksBuild;
        GameManager.OnRequestToStackRebuild += HandleOnStacksRebuilt;
    }

    private void HandleOnStacksRebuilt(RebuildData rebuildData)
    {
        Debug.Log("$Rebuilding Stacks!");

        rebuildData.RebuildStack(_piecesPosesFromStacks);
        
    }

    

    

    private void HandleOnStacksBuild(List<Transform> stacks)
    {
        foreach (var stack in stacks)
        {
            _piecesPosesFromStacks.Add(GetAllPosesFromStack(stack));
        }
    }

    private List<PoseRestore> GetAllPosesFromStack(Transform stack)
    {
        var allPosesFromStack = new List<PoseRestore>();
        Rigidbody[] rigidbodies = stack.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            var t = rb.transform.parent;
            //Debug.Log($"Fetching {rb.transform.parent.name}");
            allPosesFromStack.Add(new PoseRestore(t));
        }

        return allPosesFromStack;
    }
}
