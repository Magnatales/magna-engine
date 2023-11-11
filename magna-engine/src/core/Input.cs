using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Core;

  public static class Input
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsKeyPressed(KeyboardKey key) => (bool) Raylib.IsKeyPressed(key);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey)" />
    public static bool IsKeyDown(KeyboardKey key) => (bool) Raylib.IsKeyDown(key);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsKeyReleased(Raylib_cs.KeyboardKey)" />
    public static bool IsKeyReleased(KeyboardKey key) => (bool) Raylib.IsKeyReleased(key);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsKeyUp(Raylib_cs.KeyboardKey)" />
    public static bool IsKeyUp(KeyboardKey key) => (bool) Raylib.IsKeyUp(key);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetKeyPressed" />
    public static int GetKeyPressed() => Raylib.GetKeyPressed();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetCharPressed" />
    public static int GetCharPressed() => Raylib.GetCharPressed();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetExitKey(Raylib_cs.KeyboardKey)" />
    public static void SetExitKey(KeyboardKey key) => Raylib.SetExitKey(key);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton)" />
    public static bool IsMouseButtonPressed(MouseButton button) => (bool) Raylib.IsMouseButtonPressed(button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsMouseButtonDown(Raylib_cs.MouseButton)" />
    public static bool IsMouseButtonDown(MouseButton button) => (bool) Raylib.IsMouseButtonDown(button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsMouseButtonReleased(Raylib_cs.MouseButton)" />
    public static bool IsMouseButtonReleased(MouseButton button) => (bool) Raylib.IsMouseButtonReleased(button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsMouseButtonUp(Raylib_cs.MouseButton)" />
    public static bool IsMouseButtonUp(MouseButton button) => (bool) Raylib.IsMouseButtonUp(button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMouseX" />
    public static int GetMouseX() => Raylib.GetMouseX();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMouseY" />
    public static int GetMouseY() => Raylib.GetMouseY();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMousePosition" />
    public static Vector2 GetMousePosition() => Raylib.GetMousePosition();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMouseDelta" />
    public static Vector2 GetMouseDelta() => Raylib.GetMouseDelta();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMouseWheelMove" />
    public static float GetMouseWheelMove() => Raylib.GetMouseWheelMove();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetMouseWheelMoveV" />
    public static Vector2 GetMouseWheelMoveV() => Raylib.GetMouseWheelMoveV();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetMouseCursor(Raylib_cs.MouseCursor)" />
    public static void SetMouseCursor(MouseCursor cursor) => Raylib.SetMouseCursor(cursor);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetMousePosition(System.Int32,System.Int32)" />
    public static void SetMousePosition(int x, int y) => Raylib.SetMousePosition(x, y);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetMouseOffset(System.Int32,System.Int32)" />
    public static void SetMouseOffset(int x, int y) => Raylib.SetMouseOffset(x, y);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetMouseScale(System.Single,System.Single)" />
    public static void SetMouseScale(float scaleX, float scaleY) => Raylib.SetMouseScale(scaleX, scaleY);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGamepadAvailable(System.Int32)" />
    public static bool IsGamepadAvailable(int gamepad) => (bool) Raylib.IsGamepadAvailable(gamepad);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGamepadName_(System.Int32)" />
    public static string GetGamepadName(int gamepad) => Raylib.GetGamepadName_(gamepad);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGamepadButtonPressed(System.Int32,Raylib_cs.GamepadButton)" />
    public static bool IsGamepadButtonPressed(int gamepad, GamepadButton button) => (bool) Raylib.IsGamepadButtonPressed(gamepad, button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGamepadButtonDown(System.Int32,Raylib_cs.GamepadButton)" />
    public static bool IsGamepadButtonDown(int gamepad, GamepadButton button) => (bool) Raylib.IsGamepadButtonDown(gamepad, button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGamepadButtonReleased(System.Int32,Raylib_cs.GamepadButton)" />
    public static bool IsGamepadButtonReleased(int gamepad, GamepadButton button) => (bool) Raylib.IsGamepadButtonReleased(gamepad, button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGamepadButtonUp(System.Int32,Raylib_cs.GamepadButton)" />
    public static bool IsGamepadButtonUp(int gamepad, GamepadButton button) => (bool) Raylib.IsGamepadButtonUp(gamepad, button);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGamepadButtonPressed" />
    public static int GetGamepadButtonPressed() => Raylib.GetGamepadButtonPressed();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGamepadAxisCount(System.Int32)" />
    public static int GetGamepadAxisCount(int gamepad) => Raylib.GetGamepadAxisCount(gamepad);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGamepadAxisMovement(System.Int32,Raylib_cs.GamepadAxis)" />
    public static float GetGamepadAxisMovement(int gamepad, GamepadAxis axis) => Raylib.GetGamepadAxisMovement(gamepad, axis);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetGamepadMappings(System.String)" />
    public static void SetGamepadMappings(string mappings) => Raylib.SetGamepadMappings(mappings);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetTouchX" />
    public static int GetTouchX() => Raylib.GetTouchX();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetTouchY" />
    public static int GetTouchY() => Raylib.GetTouchY();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetTouchPosition(System.Int32)" />
    public static Vector2 GetTouchPosition(int index) => Raylib.GetTouchPosition(index);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetTouchPointId(System.Int32)" />
    public static int GetTouchPointId(int index) => Raylib.GetTouchPointId(index);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetTouchPointCount" />
    public static int GetTouchPointCount() => Raylib.GetTouchPointCount();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.SetGesturesEnabled(Raylib_cs.Gesture)" />
    public static void SetGesturesEnabled(Gesture flags) => Raylib.SetGesturesEnabled(flags);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsGestureDetected(Raylib_cs.Gesture)" />
    public static bool IsGestureDetected(Gesture gesture) => (bool) Raylib.IsGestureDetected(gesture);

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGestureDetected" />
    public static Gesture GetGestureDetected() => Raylib.GetGestureDetected();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGestureHoldDuration" />
    public static float GetGestureHoldDuration() => Raylib.GetGestureHoldDuration();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGestureDragVector" />
    public static Vector2 GetGestureDragVector() => Raylib.GetGestureDragVector();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGestureDragAngle" />
    public static float GetGestureDragAngle() => Raylib.GetGestureDragAngle();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGesturePinchVector" />
    public static Vector2 GetGesturePinchVector() => Raylib.GetGesturePinchVector();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.GetGesturePinchAngle" />
    public static float GetGesturePinchAngle() => Raylib.GetGesturePinchAngle();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.ShowCursor" />
    public static void ShowCursor() => Raylib.ShowCursor();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.HideCursor" />
    public static void HideCursor() => Raylib.HideCursor();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsCursorHidden" />
    public static bool IsCursorHidden() => (bool) Raylib.IsCursorHidden();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.EnableCursor" />
    public static void EnableCursor() => Raylib.EnableCursor();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.DisableCursor" />
    public static void DisableCursor() => Raylib.DisableCursor();

    /// <inheritdoc cref="M:Raylib_cs.Raylib.IsCursorOnScreen" />
    public static bool IsCursorOnScreen() => (bool) Raylib.IsCursorOnScreen();
  }