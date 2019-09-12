# MyComparer
Class to allow you to compare objects based on custom selectors.

Given a class `Foo` that you can't edit:

    public class Foo
    {
        public int Bar { get; set; }
        public float Baz { get; set; }
    }

It lets you say

    Foo foo1 = GetFoo1();
    Foo foo2 = GetFoo2();
    
    IEqualityComparer<Foo> comparer = MyComparer<Foo>.On(f => f.Bar).And(f => f.Baz);
    comparer.Equals(foo1, foo2); //returns true iff Bar is equal on both objects *and* Baz is equal on both objects
    
Or:

    List<Foo> foos = GetFoos();
    var uniqueFoos = foos.ToHashset(MyComparer<Foo>.On(f => f.Bar).And(f => f.Baz));
