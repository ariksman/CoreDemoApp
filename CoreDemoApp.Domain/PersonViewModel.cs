using System;
using System.Collections.Generic;

namespace CoreDemoApp.Domain
{
  public class Person
  {

    public Person()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string WorkPlace { get; set; }
    public int Age { get; set; }


    public static List<Person> CreatePersonData(int count, Random rnd)
    {
      var persons = new List<Person>();

      for (int i = 0; i < count; i++)
      {
        var newPerson = new Person()
        {
          Id = i,
          Name = "test " + i,
          Age = rnd.Next(),
        };

        persons.Add(newPerson);
      }

      return persons;
    }
  }

}