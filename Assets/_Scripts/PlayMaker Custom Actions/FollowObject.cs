// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	public class FollowObject : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		public FsmGameObject targetObject;



		// Cache for performance
		private GameObject cachedObject;
		private Transform myTransform;

		private GameObject cachedTarget;
		private Transform targetTransform;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleLateUpdate = true;
		}

		public override void OnLateUpdate()
		{
			if (targetObject.Value == null)
			{
				return;
			}

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			if (cachedObject != go)
			{
				cachedObject = go;
				myTransform = go.transform;
			}

			if (cachedTarget != targetObject.Value)
			{
				cachedTarget = targetObject.Value;
				targetTransform = cachedTarget.transform;
			}
		
			myTransform.position = targetTransform.position;

		}

	}
}