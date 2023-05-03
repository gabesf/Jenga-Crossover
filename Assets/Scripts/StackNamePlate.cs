using UnityEngine;
using TMPro;
namespace Building
{
    public class StackNamePlate : MonoBehaviour
    {
        public TextMeshPro nameplateLabel;
        public void Initialize(StackNameplateInstantiationData stackNameplateInstantiationData)
        {
            nameplateLabel.text = stackNameplateInstantiationData.Name;
            transform.localPosition = stackNameplateInstantiationData.LocalPosition;
            transform.localRotation = Quaternion.Euler(stackNameplateInstantiationData.LocalRotation);
            
        }
    }
}