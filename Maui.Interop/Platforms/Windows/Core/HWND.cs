namespace Maui.Interop.Platforms.Windows.Core;

public readonly partial struct HWND : IEquatable<HWND>
{
    internal readonly IntPtr Value;

    internal HWND(IntPtr value) => this.Value = value;

    internal static HWND Null => default;

    internal bool IsNull => Value == default;

    public static implicit operator IntPtr(HWND value) => value.Value;

    public static explicit operator HWND(IntPtr value) => new HWND(value);

    public static bool operator ==(HWND left, HWND right) => left.Value == right.Value;

    public static bool operator !=(HWND left, HWND right) => !(left == right);

    public bool Equals(HWND other) => this.Value == other.Value;

    public override bool Equals(object obj) => obj is HWND other && this.Equals(other);

    public override int GetHashCode() => this.Value.GetHashCode();
}
