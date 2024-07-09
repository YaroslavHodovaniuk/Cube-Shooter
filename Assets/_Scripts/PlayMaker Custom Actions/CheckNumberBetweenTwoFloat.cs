// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
//Made by KirbyRawr from www.overflowingstudios.com, enjoy it :P
using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("This custom action check if a number is between two")]
	public class CheckNumberBetweenTwoFloat : FsmStateAction
	{
		[RequiredField]
		public FsmFloat number0;

		public FsmFloat numberBetween;

		public FsmFloat number1;

		public FsmEvent yes;

		public FsmEvent no;

		public bool everyFrame;

		public override void Reset()
		{
			number0 = 0;
			numberBetween = 0;
			number1 = 0;
			everyFrame = false;
			yes = null;
		}

		public override void OnEnter()
		{
			DoCompare();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

		void DoCompare()
		{

			if (number0.Value < numberBetween.Value && numberBetween.Value < number1.Value)
			{
				Fsm.Event(yes);
			}

			else
			{
				Fsm.Event(no);
			}

		}
	}
}