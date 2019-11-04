using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain.Model
{
  public class Age : ValueObject<Age>
  {
    public Age(int value)
    {
      Value = value;
    }

    public int Value { get; }

    public static Result<Age> Create(Maybe<int> ageOrNothing)
    {
      return ageOrNothing.ToResult("Argument is null")
        .Ensure(value => value >= 0, "Age must be a positive number")
        .Ensure(value => value < 150, "Maximum age is 150, value must be lower")
        .Map(value => new Age(value));
    }

    protected override bool EqualsCore(Age other)
    {
      return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
      return Value.GetHashCode();
    }

    public static implicit operator int(Age age)
    {
      return age.Value;
    }

    public static explicit operator Age(int age)
    {
      return Create(age).Value;
    }
  }
}