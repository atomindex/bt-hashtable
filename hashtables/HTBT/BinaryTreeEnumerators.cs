using System;
using System.Collections;
using System.Collections.Generic;

namespace hashtables.HTBT {

    //Обход дерева слева направо, элементы добавляются в список при обратном ходе рекурсии
    //В порядке левый потомок, родитель, правый потомок
    public class BinaryTreeInOrderEnumerator<T> : IEnumerator<T> where T : IComparable {
        
        private BinaryTreeNode<T> current;
        private BinaryTree<T> tree;
        internal Queue<BinaryTreeNode<T>> traverseQueue;

        public BinaryTreeInOrderEnumerator(BinaryTree<T> tree) {
            this.tree = tree;

            traverseQueue = new Queue<BinaryTreeNode<T>>();
            visitNode(tree.Root);
        }

        private void visitNode(BinaryTreeNode<T> node) {
            if (node == null)
                return;
            else {
                visitNode(node.LeftChild);
                traverseQueue.Enqueue(node);
                visitNode(node.RightChild);
            }
        }

        public T Current {
            get { return current.Value; }
        }

        object IEnumerator.Current {
            get { return Current; }
        }

        public void Dispose() {
            current = null;
            tree = null;
        }

        public void Reset() {
            current = null;
        }

        public bool MoveNext() {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return current != null;
        }
    }



    //Обхд дерева слева напрво, элементы добавляются в список при обратном ходе рекурсии
    public class BinaryTreePostOrderEnumerator<T> : IEnumerator<T> where T : IComparable {
        
        private BinaryTreeNode<T> current;
        private BinaryTree<T> tree;
        internal Queue<BinaryTreeNode<T>> traverseQueue;

        public BinaryTreePostOrderEnumerator(BinaryTree<T> tree) {
            this.tree = tree;

            traverseQueue = new Queue<BinaryTreeNode<T>>();
            visitNode(tree.Root);
        }

        private void visitNode(BinaryTreeNode<T> node) {
            if (node == null)
                return;
            else {
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
                traverseQueue.Enqueue(node);
            }
        }

        public T Current {
            get { return current.Value; }
        }

        object IEnumerator.Current {
            get { return Current; }
        }

        public void Dispose() {
            current = null;
            tree = null;
        }

        public void Reset() {
            current = null;
        }

        public bool MoveNext() {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return current != null;
        }
    }



    //Обход дерева слева направо, элементы добавляются в список при прямом ходе рекурсии 
    public class BinaryTreePreOrderEnumerator<T> : IEnumerator<T> where T : IComparable {
        
        private BinaryTreeNode<T> current;
        private BinaryTree<T> tree;
        internal Queue<BinaryTreeNode<T>> traverseQueue;

        public BinaryTreePreOrderEnumerator(BinaryTree<T> tree) {
            this.tree = tree;

            traverseQueue = new Queue<BinaryTreeNode<T>>();
            visitNode(tree.Root);
        }

        private void visitNode(BinaryTreeNode<T> node) {
            if (node == null)
                return;
            else {
                traverseQueue.Enqueue(node);
                visitNode(node.LeftChild);
                visitNode(node.RightChild);
            }
        }

        public T Current {
            get { return current.Value; }
        }

        object IEnumerator.Current {
            get { return Current; }
        }

        public void Dispose() {
            current = null;
            tree = null;
        }

        public void Reset() {
            current = null;
        }

        public bool MoveNext() {
            if (traverseQueue.Count > 0)
                current = traverseQueue.Dequeue();
            else
                current = null;

            return current != null;
        }
    }

}