// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("This Action helps to select unwanted tags for trigger events. " +
			 "It interacts with all tags except the tags you select.  " +
			 "NOTE: Collisions are processed after other actions, so this action should be ordered last in the list.")]
	public class TriggerEventIgnoreTags: FsmStateAction
	{
		[Tooltip("The GameObject to detect trigger events on.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The type of trigger event to detect.")]
		public TriggerType trigger;


		[UIHint(UIHint.TagMenu)]
		[Tooltip("Unwanted tags.")]
		public string[] ignoreTags;

		[Tooltip("Event to send if the trigger event is detected.")]
		public FsmEvent sendEvent;


		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		// cached proxy component for callbacks
		private PlayMakerProxyBase cachedProxy;

		public override void Reset()
		{
			gameObject = null;
			trigger = TriggerType.OnTriggerEnter;
			sendEvent = null;
			storeCollider = null;
		}

		public override void OnPreprocess()
		{
			if (gameObject == null) gameObject = new FsmOwnerDefault();
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				switch (trigger)
				{
					case TriggerType.OnTriggerEnter:
						Fsm.HandleTriggerEnter = true;
						break;
					case TriggerType.OnTriggerStay:
						Fsm.HandleTriggerStay = true;
						break;
					case TriggerType.OnTriggerExit:
						Fsm.HandleTriggerExit = true;
						break;
				}
			}
			else
			{
				// Add proxy components now if we can
				GetProxyComponent();
			}
		}

		public override void OnEnter()
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				return;

			if (cachedProxy == null)
				GetProxyComponent();

			AddCallback();

			gameObject.GameObject.OnChange += UpdateCallback;
		}

		public override void OnExit()
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				return;

			RemoveCallback();

			gameObject.GameObject.OnChange -= UpdateCallback;
		}

		private void UpdateCallback()
		{
			RemoveCallback();
			GetProxyComponent();
			AddCallback();
		}

		private void GetProxyComponent()
		{
			cachedProxy = null;
			var source = gameObject.GameObject.Value;
			if (source == null)
				return;

			switch (trigger)
			{
				case TriggerType.OnTriggerEnter:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerEnter>(source);
					break;
				case TriggerType.OnTriggerStay:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerStay>(source);
					break;
				case TriggerType.OnTriggerExit:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerExit>(source);
					break;
			}
		}

		private void AddCallback()
		{
			if (cachedProxy == null)
				return;

			switch (trigger)
			{
				case TriggerType.OnTriggerEnter:
					cachedProxy.AddTriggerEventCallback(TriggerEnter);
					break;
				case TriggerType.OnTriggerStay:
					cachedProxy.AddTriggerEventCallback(TriggerStay);
					break;
				case TriggerType.OnTriggerExit:
					cachedProxy.AddTriggerEventCallback(TriggerExit);
					break;
			}
		}

		private void RemoveCallback()
		{
			if (cachedProxy == null)
				return;

			switch (trigger)
			{
				case TriggerType.OnTriggerEnter:
					cachedProxy.RemoveTriggerEventCallback(TriggerEnter);
					break;
				case TriggerType.OnTriggerStay:
					cachedProxy.RemoveTriggerEventCallback(TriggerStay);
					break;
				case TriggerType.OnTriggerExit:
					cachedProxy.RemoveTriggerEventCallback(TriggerExit);
					break;
			}
		}

		private void StoreCollisionInfo(Collider collisionInfo)
		{
			storeCollider.Value = collisionInfo.gameObject;
		}

		public override void DoTriggerEnter(Collider other)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				TriggerEnter(other);
		}

		public override void DoTriggerStay(Collider other)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				TriggerStay(other);
		}

		public override void DoTriggerExit(Collider other)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				TriggerExit(other);
		}


		private void TriggerEnter(Collider other)
		{
			if (trigger == TriggerType.OnTriggerEnter)
			{
				if (ContainsTag(other.tag))
                {
					return;
                }
				else
                {
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		private void TriggerStay(Collider other)
		{
			if (trigger == TriggerType.OnTriggerStay)
			{
				if (ContainsTag(other.tag))
				{
					return;
				}
				else
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		private void TriggerExit(Collider other)
		{
			if (trigger == TriggerType.OnTriggerExit)
			{
				if (ContainsTag(other.tag))
				{
					return;
				}
				else
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}


		bool ContainsTag(string tag)
		{
			for (int i = 0; i < ignoreTags.Length; i++)
				if (ignoreTags[i] == tag)
					return true;

			return false;
		}


		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysicsSetup(Fsm.GetOwnerDefaultTarget(gameObject));
		}
	}
}