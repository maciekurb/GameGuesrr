using System;
using Bogus;

namespace GameGuessr.Tests;


public class NonPublicFaker<T> : Faker<T> where T : class 
{
    public NonPublicFaker<T> UseNonPublicFakerConstructor()
    {
        return base.CustomInstantiator(f => Activator.CreateInstance(typeof(T), nonPublic: true) as T)
            as NonPublicFaker<T>;
    }

    public NonPublicFaker<T> RuleForPrivate<TProperty>(string propertyName, Func<Faker, TProperty> setter)
    {
        base.RuleFor(propertyName, setter);
        return this;
    }
}
