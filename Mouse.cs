using PublicUtility.Nms.Enums;
using PublicUtility.Nms.Structs;

namespace PublicUtility.MouseRun {

  public static class Mouse {
    private static readonly string _platformMessageError = "Platform not yet supported by the application. Wait for new updates.";

    public static void RollDown(uint clicks) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.RollDown(clicks);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void RollUp(uint clicks) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.RollUp(clicks);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void LeftClick(bool doubleClick = false) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.LeftClick(doubleClick);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void RightClick(bool doubleClick = false) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.RightClick(doubleClick);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void Drag(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed = MouseSpeed.X1) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.Drag(start, end, speed);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static PointIntoScreen GetPosition() {
      if(OperatingSystem.IsWindows())
        return Windows.MouseHandle.GetPosition();


      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void MoveToAndClick(this PointIntoScreen point, MouseSpeed mouseSpeed = MouseSpeed.X2, bool doubleClick = false, bool leftMouseButton = true) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.MoveToAndClick(point, mouseSpeed ,doubleClick, leftMouseButton);
        return;
      }
        
      throw new PlatformNotSupportedException(_platformMessageError);
    }

    public static void MoveTo(this PointIntoScreen point, MouseSpeed mouseSpeed = MouseSpeed.X2) {
      if(OperatingSystem.IsWindows()) {
        Windows.MouseHandle.MoveTo(point, mouseSpeed);
        return;
      }

      throw new PlatformNotSupportedException(_platformMessageError);
    }

  }

}
