using UnityEngine;

namespace Building
{
    public static class StackNamePlateBuilder
    {
        public static void CreateNameplate(StackNameplateInstantiationData stackNameplateInstantiationData,
            GameObject stackNameplatePrefab,
            Transform stackTransform)
        {
            Transform namePlate = Object.Instantiate(stackNameplatePrefab, stackTransform).transform;
            namePlate.GetComponent<StackNamePlate>().Initialize(stackNameplateInstantiationData);
        }
    }
}