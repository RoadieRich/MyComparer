using System;
using System.Collections.Generic;

namespace MyComparer
{
    /// <summary>
    /// A custom comparer for objects of type <typeparamref name="TObject"/>.
    /// </summary>
    /// <typeparam name="TObject">The type of object to be compared</typeparam>
    public class MyEqualityComparer<TObject> : IEqualityComparer<TObject>
    {
        private EqualsDelegate _equals;
        private GetHashCodeDelegate _getHashCode;
    
        private MyEqualityComparer(EqualsDelegate equals, GetHashCodeDelegate getHashCode) 
        {
            _equals = equals;
            _getHashCode = getHashCode;
        }
    
        /// <summary>
        /// Create a new <see cref="MyEqualityComparer{TObject}"/> object using the passed in <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to be compared</typeparam>
        /// <param name="selector">A selector function</param>
        /// <returns>A <see cref="MyComparer{TObject}"/> object</returns>
        public static MyEqualityComparer<TObject> On<TProperty>(Selector<TProperty> selector) where TProperty : IEquatable<TProperty>
        {
            return new MyEqualityComparer<TObject>((x, y) => selector(x).Equals(selector(y)), obj =>
            {
                unchecked { return (int)2166136261 * 16777619 + selector(obj).GetHashCode();}
            });
        }
    
        /// <summary>
        /// Compounds additional selectors
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to be compared</typeparam>
        /// <param name="selector">A selector function</param>
        /// <returns>A <see cref="MyComparer{TObject}"/> object</returns>
        public MyEqualityComparer<TObject> And<TProperty>(Selector<TProperty> selector) where TProperty : IEquatable<TProperty>
        {
            var oldEquals = (EqualsDelegate)_equals.Clone();
            var oldGetHashCode = (GetHashCodeDelegate)_getHashCode.Clone();
    
            _equals = (x, y) => oldEquals(x, y) && selector(x).Equals(selector(y));
            _getHashCode = x => { unchecked { return oldGetHashCode(x) * 16777619 + selector(x).GetHashCode(); } };
    
            return this;
        }
        
        bool IEqualityComparer<TObject>.Equals(TObject? x, TObject? y)
        {
            return _equals(x, y);
        }
    
        int IEqualityComparer<TObject>.GetHashCode(TObject obj)
        {
            return _getHashCode(obj);
        }
    
        private delegate bool EqualsDelegate(TObject? x, TObject? y);
        private delegate int GetHashCodeDelegate(TObject obj);
        public delegate TProperty Selector<TProperty>(TObject? obj);
    }
}
