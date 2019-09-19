using System;
using System.Collections.Generic;
using CoreDemoApp.Domain.DDD;

namespace CoreDemoApp.Domain
{
  public class Person : AggregateRoot
  {

    public Person()
    {
    }

    public string Name { get; set; }
    public string WorkPlace { get; set; }
    public Age Age { get; set; }


    public static List<Person> CreatePersonData(int count, Random rnd)
    {
      var persons = new List<Person>();

      for (int i = 0; i < count; i++)
      {
        var newPerson = new Person()
        {
          Id = i,
          Name = "test " + i,
          Age = Domain.Age.Create(rnd.Next()).Value,
        };

        persons.Add(newPerson);
      }

      return persons;
    }
  }
}