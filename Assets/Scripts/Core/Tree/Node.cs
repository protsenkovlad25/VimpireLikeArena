using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Trees
{
    public class Node<T>
    {
        public readonly T Value;

        public List<Node<T>> Children;

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }

        public Node<T> Add(T value)
        {
            var node = new Node<T>(value);
            Children.Add(node);
            return node;
        }
    }
}