using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipManage : MonoBehaviour
{
    #region Singleton

    public enum MeshBlendShape { Torso, Arms, Legs };
    public equipment[] defaultEquipment;

    public static equipManage instance;
    public SkinnedMeshRenderer targetMesh;

    SkinnedMeshRenderer[] currentMeshes;

    void Awake()
    {
        instance = this;
    }

    #endregion

    equipment[] currentEquipment;   // Items we currently have equipped

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(equipment newItem, equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    inventoryManager inventory;    // Reference to our inventory

    void Start()
    {
       // inventory = inventoryManager.instance;     // Get a reference to our inventory

        // Initialize currentEquipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaults();
    }

    // Equip a new item
    public void Equip(equipment newItem)
    {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    // Unequip an item with a particular index
    public equipment Unequip(int slotIndex)
    {
        equipment oldItem = null;
        // Only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // Add the item to the inventory
            oldItem = currentEquipment[slotIndex];
     //       inventory.Add(oldItem);

            SetBlendShapeWeight(oldItem, 0);
            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            // Remove the item from the equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaults();
    }

    void AttachToMesh(equipment item, int slotIndex)
    {

        SkinnedMeshRenderer newMesh = Instantiate(item.mesh) as SkinnedMeshRenderer;
        newMesh.transform.parent = targetMesh.transform.parent;

        newMesh.rootBone = targetMesh.rootBone;
        newMesh.bones = targetMesh.bones;

        currentMeshes[slotIndex] = newMesh;


        SetBlendShapeWeight(item, 100);

    }

    void SetBlendShapeWeight(equipment item, int weight)
    {
        foreach (MeshBlendShape blendshape in item.coveredMeshRegions)
        {
            int shapeIndex = (int)blendshape;
            targetMesh.SetBlendShapeWeight(shapeIndex, weight);
        }
    }

    void EquipDefaults()
    {
        foreach (equipment e in defaultEquipment)
        {
            Equip(e);
        }
    }

    void Update()
    {
        // Unequip all items if we press U
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
