using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace OnlyNumbers
{
    [MemoryDiagnoser]
    public class OnlyNumberTests
    {
        private static readonly List<User> FakeUsers = new Faker<User>()
            .UseSeed(420)
            .RuleFor(r => r.Login, faker => faker.Person.UserName)
            .RuleFor(r => r.Password, faker => faker.Internet.Password())
            .Generate(10);

        private static readonly Regex OnlyNumberPattern =
            new(@"\d+", RegexOptions.IgnoreCase
                        | RegexOptions.Compiled,
                TimeSpan.FromMilliseconds(250));

        [Benchmark]
        public void LinqCharIsDigitNewString()
        {
            for (var i = 0; i < FakeUsers.Count; i++)
            {
                var numbers = new string(FakeUsers[i].Password.Where(char.IsDigit).ToArray());
            }
        }

        [Benchmark]
        public void LinqCharIsDigitStringJoin()
        {
            for (var i = 0; i < FakeUsers.Count; i++)
            {
                var numbers = string.Join("", FakeUsers[i].Password.Where(char.IsDigit).ToArray());
            }
        }

        [Benchmark]
        public void RegexMatchOnlyNumberPattern()
        {
            for (var i = 0; i < FakeUsers.Count; i++)
            {
                var number = OnlyNumberPattern.Match(FakeUsers[i].Password).Value;
            }
        }

[Benchmark]
public void IfValidator()
{
    for (var i = 0; i < FakeUsers.Count; i++)
    {
        var digitsOnly = string.Empty;
        foreach (var c in FakeUsers[i].Password)
        {
            if (c is >= '0' and <= '9') 
                digitsOnly += c;
        }
    }
}
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}