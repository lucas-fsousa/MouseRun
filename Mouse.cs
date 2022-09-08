using PublicUtility.Nms.Enums;
using PublicUtility.Nms.Structs;
using PublicUtility.MouseRun.Windows;
using PublicUtility.MouseRun.Linux;

namespace PublicUtility.MouseRun {

  public static class Mouse {

    private static readonly string _platformMessageError = "Platform not yet supported by the application. Wait for new updates.";
    private static readonly string _notImplementMessageError = "platform in the process of mapping.";

    public static void RollDown(uint clicks) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.RollDown(clicks);
        return;
      }

      if(OperatingSystem.IsLinux()) { 
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void RollUp(uint clicks) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.RollUp(clicks);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void LeftClick(bool doubleClick = false) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.LeftClick(doubleClick);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void RightClick(bool doubleClick = false) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.RightClick(doubleClick);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void Drag(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed = MouseSpeed.X1) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.Drag(start, end, speed);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static PointIntoScreen GetPosition() {
      if(OperatingSystem.IsWindows())
        return WinMouseHandle.GetPosition();
      
      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void MoveToAndClick(this PointIntoScreen point, MouseSpeed mouseSpeed = MouseSpeed.X2, bool doubleClick = false, bool leftMouseButton = true) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.MoveToAndClick(point, mouseSpeed ,doubleClick, leftMouseButton);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void MoveTo(this PointIntoScreen point, MouseSpeed mouseSpeed = MouseSpeed.X2) {
      if(OperatingSystem.IsWindows()) {
        WinMouseHandle.MoveTo(point, mouseSpeed);
        return;
      }

      if(OperatingSystem.IsLinux()) {
        throw new NotImplementedException(_notImplementMessageError);
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

  }

}
