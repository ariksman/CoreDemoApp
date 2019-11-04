using System;
using System.Collections.Generic;
using System.Data;
using CoreDemoApp.Core.DDD;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain.Model
{
  public class Person : AggregateRoot
  {
    public string Name { get; private set; }
    public WorkPlace WorkPlace { get; private set; }
    public Age Age { get; private set; }

    public Person(string name, int age)
    {
      Name = name;
      Age.Create(age)
        .OnSuccessTry(value => Age = value);
    }

    public static List<Person> CreatePersonData(int count, Random rnd)
    {
      var persons = new List<Person>();

      for (int i = 0; i < count; i++)
      {
        var newPerson = new Person("test "+i, Age.Create(rnd.Next()).Value);
        persons.Add(newPerson);
      }

      return persons;
    }
  }
}