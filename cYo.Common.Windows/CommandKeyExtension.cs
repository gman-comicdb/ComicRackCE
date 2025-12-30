namespace cYo.Common.Windows;

public static class CommandKeyExtension
{
    public static bool IsMouseButton(this CommandKey key)
    {
        key &= (CommandKey)65535;
        return key is not CommandKey.MouseLeft and not CommandKey.MouseDoubleLeft and not CommandKey.MouseMiddle and not CommandKey.MouseDoubleMiddle and not CommandKey.MouseRight and not CommandKey.MouseDoubleRight and not CommandKey.MouseButton4 and not CommandKey.MouseDoubleButton4 and not CommandKey.MouseButton5 and not CommandKey.MouseDoubleButton5 and not CommandKey.MouseWheelUp and not CommandKey.MouseWheelDown and not CommandKey.MouseTiltRight and not CommandKey.MouseTiltLeft and not CommandKey.TouchTap and not CommandKey.TouchDoubleTap and not CommandKey.TouchPressAndTap
            ? key == CommandKey.TouchTwoFingerTap
            : true;
    }
}
