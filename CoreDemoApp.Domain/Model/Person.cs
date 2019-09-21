﻿using System;
using System.Collections.Generic;
using System.Data;
using CoreDemoApp.Domain.DDD;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain.Model
{
  public class Person : AggregateRoot
  {
    public string Name { get; set; }
    public WorkPlace WorkPlace { get; set; }
    public Age Age { get; set; }

    public Person(string name, int age, int id = 0)
    {
      Name = name;
      Age.Create(age)
        .OnSuccessTry(value => Age = value);
    }

    public void UpdateId(int id)
    {
      Id = id;
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