using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    public class ClosestPointOnBounds : FsmStateAction
    {
        public FsmGameObject triggerObject;
        public FsmGameObject hitObject;
        public FsmVector3 closestPoint;


        public override void OnEnter()
        {
            closestPoint.Value = hitObject.Value.GetComponent<Collider>().ClosestPointOnBounds(triggerObject.Value.transform.position);

        }
    }
}
