public class State<EntityType>
{

    public virtual void Enter(EntityType ent) { }

    public virtual void Execute(EntityType ent) { }

    public virtual void FixedExecute(EntityType ent) { }
    
    public virtual void Exit(EntityType ent) { }

    //public virtual bool OnMessage(EntityType ent) { return false; }

}