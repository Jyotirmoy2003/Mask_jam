using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool mask;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public static Action INPAC_ONMASKBUTTONPRESSED;
		public static Action INPAC_ONBACKBUTTONPRESSED;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}
		public void OnBack(InputValue value)
		{
			INPAC_ONBACKBUTTONPRESSED?.Invoke();
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnMask(InputValue value)
		{
			MaskInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void MaskInput(bool newMaskState)
		{
			mask = newMaskState;
			INPAC_ONMASKBUTTONPRESSED?.Invoke();
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			if(GameManager.Instance.isGamePaused || GameManager.Instance.isPlayerDied)
			{
				Cursor.lockState = CursorLockMode.None;
				return;
			}
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}