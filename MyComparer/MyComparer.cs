using System;
using System.Collections.Generic;

namespace MyComparer
{
    /// <summary>
    /// A custom comparer for objects of type <typeparamref name="TObject"/>.
    /// </summary>
    /// <typeparam name="TObject">The type of object to be compared</typeparam>
    public class MyComparer<TObject> : IEqualityComparer<TObject>
    {
        private Func<TObject, TObject, bool> _equals;
        private Func<TObject, int> _getHashCode;

        private MyComparer() { }

        /// <summary>
        /// Create a new <see cref="MyComparer{TObject}"/> object using the passed in <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to be compared</typeparam>
        /// <param name="selector">A selector function</param>
        /// <returns>A <see cref="MyComparer{TObject}"/> object</returns>
        public static MyComparer<TObject> On<TProperty>(Func<TObject, TProperty> selector) where TProperty : IEquatable<TProperty>
        {
            return new MyComparer<TObject>
            {
                _equals = (x, y) => selector(x).Equals(selector(y)),
                _getHashCode = x => { unchecked { return (int)2166136261 * 16777619 + selector(x).GetHashCode(); } }
            };
        }

        /// <summary>
        /// Compounds additional selectors
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to be compared</typeparam>
        /// <param name="selector">A selector function</param>
        /// <returns>A <see cref="MyComparer{TObject}"/> object</returns>
        public MyComparer<TObject> And<TProperty>(Func<TObject, TProperty> selector)
        {
            var oldEquals = (Func<TObject, TObject, bool>)_equals.Clone();
            var oldGetHashCode = (Func<TObject, int>)_getHashCode.Clone();

            _equals = (x, y) => oldEquals(x, y) && selector(x).Equals(selector(y));
            _getHashCode = x => { unchecked { return oldGetHashCode(x) * 16777619 + selector(x).GetHashCode(); } };

            return this;
        }

        bool IEqualityComparer<TObject>.Equals(TObject x, TObject y)
        {
            return _equals(x, y);
        }

        int IEqualityComparer<TObject>.GetHashCode(TObject obj)
        {
            return _getHashCode(obj);
        }
    }

}