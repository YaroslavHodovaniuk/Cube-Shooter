// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Flips the value of a Bool Variable. True becomes False, False becomes True.")]
	public class BoolFlipEveryframe : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to flip. True becomes False, False becomes True.")]
		public FsmBool boolVariable;

		public override void Reset()
		{
			boolVariable = null;
		}

		public override void OnUpdate()
		{
			boolVariable.Value = !boolVariable.Value;
		}
	}
}