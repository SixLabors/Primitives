// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SixLabors.Primitives
{
  /// <summary>
  /// Represents an ordered pair of integer x- and y-coordinates that defines a point in
  /// a two-dimensional plane.
  /// </summary>
  /// <remarks>
  /// This struct is fully mutable. This is done (against the guidelines) for the sake of performance,
  /// as it avoids the need to create new values for modification operations.
  /// </remarks>
  public struct Point : IEquatable<Point>
  {
    /// <summary>
    /// Represents a <see cref="T:SixLabors.Primitives.Point" /> that has X and Y values set to zero.
    /// </summary>
    public static readonly Point Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SixLabors.Primitives.Point" /> struct.
    /// </summary>
    /// <param name="value">The horizontal and vertical position of the point.</param>
    public Point(int value)
    {
        this = new Point
        {
            X = LowInt16(value),
            Y = HighInt16(value)
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SixLabors.Primitives.Point" /> struct.
    /// </summary>
    /// <param name="x">The horizontal position of the point.</param>
    /// <param name="y">The vertical position of the point.</param>
    public Point(int x, int y)
    {
        this = new Point
        {
            X = x,
            Y = y
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SixLabors.Primitives.Point" /> struct from the given <see cref="SixLabors.Primitives.Size" />.
    /// </summary>
    /// <param name="size">The size</param>
    public Point(Size size)
    {
      this.X = size.Width;
      this.Y = size.Height;
    }

    /// <summary>
    /// Gets or sets the x-coordinate of this <see cref="SixLabors.Primitives.Point" />.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the y-coordinate of this <see cref="SixLabors.Primitives.Point" />.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="SixLabors.Primitives.Point" /> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsEmpty
    {
        get
        {
            return this.Equals(Empty);
        }
    }

    /// <summary>
    /// Creates a <see cref="T:SixLabors.Primitives.PointF" /> with the coordinates of the specified <see cref="SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="point">The point</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator PointF(Point point)
    {
      return new PointF(point.X, point.Y);
    }

    /// <summary>
    /// Creates a <see cref="System.Numerics.Vector2" /> with the coordinates of the specified <see cref="SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="point">The point</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector2(Point point)
    {
      return new Vector2(point.X, point.Y);
    }

    /// <summary>
    /// Creates a <see cref="T:SixLabors.Primitives.Size" /> with the coordinates of the specified <see cref="T:SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="point">The point</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Size(Point point)
    {
      return new Size(point.X, point.Y);
    }

    /// <summary>
    /// Negates the given point by multiplying all values by -1.
    /// </summary>
    /// <param name="value">The source point.</param>
    /// <returns>The negated point.</returns>
    public static Point operator -(Point value)
    {
      return new Point(-value.X, -value.Y);
    }

    /// <summary>
    /// Translates a <see cref="T:SixLabors.Primitives.Point" /> by a given <see cref="T:SixLabors.Primitives.Size" />.
    /// </summary>
    /// <param name="point">The point on the left hand of the operand.</param>
    /// <param name="size">The size on the right hand of the operand.</param>
    /// <returns>
    /// The <see cref="T:SixLabors.Primitives.Point" />
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator +(Point point, Size size)
    {
      return Add(point, size);
    }

    /// <summary>
    /// Translates a <see cref="T:SixLabors.Primitives.Point" /> by the negative of a given <see cref="T:SixLabors.Primitives.Size" />.
    /// </summary>
    /// <param name="point">The point on the left hand of the operand.</param>
    /// <param name="size">The size on the right hand of the operand.</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator -(Point point, Size size)
    {
      return Subtract(point, size);
    }

    /// <summary>
    /// Multiplies <see cref="T:SixLabors.Primitives.Point" /> by a <see cref="T:System.Int32" /> producing <see cref="T:SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="left">Multiplier of type <see cref="T:System.Int32" />.</param>
    /// <param name="right">Multiplicand of type <see cref="T:SixLabors.Primitives.Point" />.</param>
    /// <returns>Product of type <see cref="T:SixLabors.Primitives.Point" />.</returns>
    public static Point operator *(int left, Point right)
    {
      return Multiply(right, left);
    }

    /// <summary>
    /// Multiplies <see cref="T:SixLabors.Primitives.Point" /> by a <see cref="T:System.Int32" /> producing <see cref="T:SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="left">Multiplicand of type <see cref="T:SixLabors.Primitives.Point" />.</param>
    /// <param name="right">Multiplier of type <see cref="T:System.Int32" />.</param>
    /// <returns>Product of type <see cref="T:SixLabors.Primitives.Point" />.</returns>
    public static Point operator *(Point left, int right)
    {
      return Multiply(left, right);
    }

    /// <summary>
    /// Divides <see cref="T:SixLabors.Primitives.Point" /> by a <see cref="T:System.Int32" /> producing <see cref="T:SixLabors.Primitives.Point" />.
    /// </summary>
    /// <param name="left">Dividend of type <see cref="T:SixLabors.Primitives.Point" />.</param>
    /// <param name="right">Divisor of type <see cref="T:System.Int32" />.</param>
    /// <returns>Result of type <see cref="T:SixLabors.Primitives.Point" />.</returns>
    public static Point operator /(Point left, int right)
    {
      return new Point(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Compares two <see cref="T:SixLabors.Primitives.Point" /> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="T:SixLabors.Primitives.Point" /> on the left side of the operand.</param>
    /// <param name="right">The <see cref="T:SixLabors.Primitives.Point" /> on the right side of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right" /> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Point left, Point right)
    {
      return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="T:SixLabors.Primitives.Point" /> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="T:SixLabors.Primitives.Point" /> on the left side of the operand.</param>
    /// <param name="right">The <see cref="T:SixLabors.Primitives.Point" /> on the right side of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right" /> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Point left, Point right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    /// Translates a <see cref="T:SixLabors.Primitives.Point" /> by the negative of a given <see cref="T:SixLabors.Primitives.Size" />.
    /// </summary>
    /// <param name="point">The point on the left hand of the operand.</param>
    /// <param name="size">The size on the right hand of the operand.</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Add(Point point, Size size)
    {
      return new Point(point.X + size.Width, point.Y + size.Height);
    }

    /// <summary>
    /// Translates a <see cref="T:SixLabors.Primitives.Point" /> by the negative of a given value
    /// </summary>
    /// <param name="point">The point on the left hand of the operand.</param>
    /// <param name="value">The value on the right hand of the operand.</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Multiply(Point point, int value)
    {
      return new Point(point.X * value, point.Y * value);
    }

    /// <summary>
    /// Translates a <see cref="T:SixLabors.Primitives.Point" /> by the negative of a given <see cref="T:SixLabors.Primitives.Size" />.
    /// </summary>
    /// <param name="point">The point on the left hand of the operand.</param>
    /// <param name="size">The size on the right hand of the operand.</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Subtract(Point point, Size size)
    {
      return new Point(point.X - size.Width, point.Y - size.Height);
    }

    /// <summary>
    /// Converts a <see cref="T:SixLabors.Primitives.PointF" /> to a <see cref="T:SixLabors.Primitives.Point" /> by performing a ceiling operation on all the coordinates.
    /// </summary>
    /// <param name="point">The point</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Ceiling(PointF point)
    {
      return new Point((int)Math.Ceiling(point.X), (int)Math.Ceiling(point.Y));
    }

    /// <summary>
    /// Converts a <see cref="T:SixLabors.Primitives.PointF" /> to a <see cref="T:SixLabors.Primitives.Point" /> by performing a round operation on all the coordinates.
    /// </summary>
    /// <param name="point">The point</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Round(PointF point)
    {
      return new Point((int)Math.Round(point.X), (int)Math.Round(point.Y));
    }

    /// <summary>Transforms a point by the given matrix.</summary>
    /// <param name="position">The source point.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>A transformed point.</returns>
    public static PointF Transform(Point position, Matrix3x2 matrix)
    {
      return Vector2.Transform(position, matrix);
    }

    /// <summary>
    /// Converts a <see cref="T:SixLabors.Primitives.PointF" /> to a <see cref="T:SixLabors.Primitives.Point" /> by performing a truncate operation on all the coordinates.
    /// </summary>
    /// <param name="point">The point</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Truncate(PointF point)
    {
      return new Point((int)point.X, (int)point.Y);
    }

    /// <summary>
    /// Converts a <see cref="T:System.Numerics.Vector2" /> to a <see cref="T:SixLabors.Primitives.Point" /> by performing a round operation on all the coordinates.
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <returns>The <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Round(Vector2 vector)
    {
      return new Point((int)Math.Round(vector.X), (int)Math.Round(vector.Y));
    }

    /// <summary>Rotates a point around the given rotation matrix.</summary>
    /// <param name="point">The point to rotate</param>
    /// <param name="rotation">Rotation matrix used</param>
    /// <returns>The rotated <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Rotate(Point point, Matrix3x2 rotation)
    {
      return Round(Vector2.Transform(new Vector2(point.X, point.Y), rotation));
    }

    /// <summary>Skews a point using the given skew matrix.</summary>
    /// <param name="point">The point to rotate</param>
    /// <param name="skew">Rotation matrix used</param>
    /// <returns>The rotated <see cref="T:SixLabors.Primitives.Point" /></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Skew(Point point, Matrix3x2 skew)
    {
      return Round(Vector2.Transform(new Vector2(point.X, point.Y), skew));
    }

    /// <summary>
    /// Translates this <see cref="T:SixLabors.Primitives.Point" /> by the specified amount.
    /// </summary>
    /// <param name="dx">The amount to offset the x-coordinate.</param>
    /// <param name="dy">The amount to offset the y-coordinate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Offset(int dx, int dy)
    {
      this.X = this.X + dx;
      this.Y = this.Y + dy;
    }

    /// <summary>
    /// Translates this <see cref="T:SixLabors.Primitives.Point" /> by the specified amount.
    /// </summary>
    /// <param name="point">The <see cref="T:SixLabors.Primitives.Point" /> used offset this <see cref="T:SixLabors.Primitives.Point" />.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Offset(Point point)
    {
      this.Offset(point.X, point.Y);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.GetHashCode(this);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return $"Point [ X={this.X}, Y={this.Y} ]";
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return obj is Point && this.Equals((Point)obj);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Point other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    private static short HighInt16(int n)
    {
      return (short)(n >> 16 & ushort.MaxValue);
    }

    private static short LowInt16(int n)
    {
      return (short)(n & ushort.MaxValue);
    }

    private int GetHashCode(Point point)
    {
      int num = point.X;
      int hashCode1 = num.GetHashCode();
      num = point.Y;
      int hashCode2 = num.GetHashCode();
      return HashHelpers.Combine(hashCode1, hashCode2);
    }
  }
}