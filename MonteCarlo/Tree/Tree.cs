using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class Tree
    {
        Node root;

        public Tree()
        {
            root = new Node();
        }

        public Tree(Node root)
        {
            this.root = root;
        }

        public Node getRoot()
        {
            return root;
        }

        public void setRoot(Node root)
        {
            this.root = root;
        }

        public void addChild(Node parent, Node child)
        {
            parent.getChildArray().Add(child);
        }
    }
}
