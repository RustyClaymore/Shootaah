using UnityEngine;

public class BaseGameEntity
{
    private int id;
    private int entityType;

    private static int nextValidId = 0;

    private void SetID(int id)
    {
        if (id >= nextValidId)
        {
            this.id = id;
            nextValidId = this.id + 1;
        }
        else
        {
            Debug.LogWarning("Invalid ID");
            SetID(nextValidId);
        }
    }

    protected BaseGameEntity(int id)
    {
        SetID(id);
        entityType = (int)EntityType.defaultType;
    }

    protected BaseGameEntity(int id, int entityType)
    {
        SetID(id);
        this.entityType = entityType;
    }

    public virtual void Update() { }

    public virtual bool HandleMessage() { return false; }

    public static int GetNextValidID() { return nextValidId; }
    public static void ResetNextValidID() { nextValidId = 0; }
    
    public int GetID() { return id; }

    public int GetEntityType() { return entityType; }
    public void SetEntityType(int entType) { entityType = entType; }
}
