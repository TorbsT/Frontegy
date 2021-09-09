using System;
using System.Collections.Generic;
using UnityEngine;
//using TMPro; please dont use here

[System.Serializable]
public class Card : SelChy  // changed to class since i didn't know why it should be struct
{  // TODO Currently only supports handCard, not upgrades
    public override Player owner { get => _state.owner; }
    public int roleId { get => blueprint.roleId; }
    public Role role { get => blueprint.role; }
    private SummonCardBP blueprint { get => _state.blueprint; }
    public bool used { get => _state.used; }

    public CardState state { get => _state; }
    private CardState _state;

    public Card(CardState state)
    {
        _state = state;
        stage();
        initMats();
    }

    public override bool canSecondarySelectOn(SelChy selChy)
    {
        if (selChy is Tile tile)
        {
            if (tile.state.ownerId == state.ownerId) return true;
        }
        return false;
    }
    public override void secondarySelectOn(SelChy selChy)
    {
        Tile tile = (Tile)selChy;
        if (tile != null)
        {

            SelMan.Instance.select(null);
            blueprint.cast(this, tile, CastType.summon);
            _state.used = true;
            transive.setParent(null, true);

            // Do the animation. Temporary storing necessary
            Vector3 source = transive.pos3p.get(false).v3;
            float mass = getCardPhy().rb.mass;
            CardPhy p = getCardPhy();
            CardPool.Instance.ragdollify(this);

            
            Vector3 target = tile.surfaceTransform.position;
            float angle = 10;

            //Vector3 force = calcBallisticVelocityVector(source, target, angle);
            Vector3 force = CalculateTrajectoryVelocity(source, target, 0.3f);
            if (float.IsNaN(force.x)) force.x = 0f;
            if (float.IsNaN(force.y)) force.y = 0f;
            if (float.IsNaN(force.z)) force.z = 0f;
            Debug.Log("Force needed is " + force + " * "+mass);
            p.rb.velocity = force;

        }
    }

    public SummonCardBP getBlueprint() { if (blueprint == null) Debug.LogError("IllegalStateException"); return blueprint; }
    public bool canCastOn(Tile tile)
    {
        return tile.owner == owner;
    }
    public CardPhy getCardPhy()
    {
        return CardPool.Instance.getHost(this);
    }

    public override void initMats()
    {
        setMat("frontFG", blueprint.frontFGMatPlace);
        setMat("backFG", "backFG");
        setMat("selectable", "initial");
        setCol("FG", getPlayerMatPlace());
    }
    protected override string getInitialSelMatPlace()
    {
        return "initial";
    }
    public override Phy getPhy()
    {
        return getCardPhy();
    }
    public override void stage()
    {
        CardPool.Instance.stage(this);
    }
    public override void unstage()
    {
        CardPool.Instance.unstage(this);
    }
    public override void hover()
    {
        base.hover();
        
    }


    Vector3 calcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }
    Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
    {
        float vx = (target.x - origin.x) / t;
        float vz = (target.z - origin.z) / t;
        float vy = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
        return new Vector3(vx, vy, vz);
    }
}
