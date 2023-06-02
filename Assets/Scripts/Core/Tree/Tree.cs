using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireLike.Core.Trees
{
    public class Tree<T>
    {
        private Stack<Node<T>> m_Stack;

        public List<Node<T>> Nodes { get; }

        public Tree()
        {
            m_Stack = new Stack<Node<T>>();
            Nodes = new List<Node<T>>();
        }

        public Tree<T> Begin(T value)
        {
            if (m_Stack.Count == 0)
            {
                var node = new Node<T>(value);
                Nodes.Add(node);
                m_Stack.Push(node);
            }
            else
            {
                var node = m_Stack.Peek().Add(value);
                m_Stack.Push(node);
            }

            return this;
        }

        public Tree<T> Add(T value)
        {
            m_Stack.Peek().Add(value);
            return this;
        }

        public Tree<T> End()
        {
            m_Stack.Pop();
            return this;
        }
    }
}