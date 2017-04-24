using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBehaviourTree {
  public abstract class Node {
    public abstract Node Next();
    public virtual void Execute() { }
  }

  public class ActionNode : Node {
    public delegate void Callback();
    public event Callback action = delegate { };
    public ActionNode(Callback action) {
      this.action = action;
    }
    public override void Execute() {
      action();
    }

    public override Node Next() {
      return this;
    }
  }

  public class SelectorNode : Node {
    private List<Node> children;
    public SelectorNode(params Node[] children) {
      this.children = new List<Node>(children);
    }

    public override Node Next() {
      foreach (var child in children) {
        var next = child.Next();
        if (next != null) return next;
      }
      return null;
    }
  }

  public class SequenceNode : Node {
    private int currentIndex = 0;
    private List<Node> children;
    public SequenceNode(params Node[] children) {
      this.children = new List<Node>(children);
    }

    public override Node Next() {
      while (currentIndex < children.Count) {
        var next = children[currentIndex].Next();
        currentIndex++;
        if (next != null) return next;
      }
      currentIndex = 0;
      return null;
    }
  }

  public class DecoratorNode : Node {
    private Node child;
    public delegate bool Predicate();
    private event Predicate predicate;

    public DecoratorNode(Node child, Predicate predicate) {
      this.child = child;
      this.predicate = predicate;
    }
    public override Node Next() {
      if (predicate()) return child.Next();
      return null;
    }
  }

  public class BehaviourTree {
    private Node root;
    public BehaviourTree(Node root) {
      this.root = root;
    }
    public void Update() {
      Node nextAction = null;
      while (nextAction == null) {
        nextAction = root.Next();
      }
      nextAction.Execute();
    }
  }

}
