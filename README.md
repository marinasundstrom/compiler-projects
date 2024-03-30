# Compiler projects

A couple of started projects for prototyping compilers. 

The idea was to build a compiler based with a modern compiler architecture - with Compiler-as-a-Service. Having an Abstract Syntax Tree (AST) that looks, feels, and works, as the Roslyn API from C#.

## In this repository

This repository contains two projects that attempt to do the same:

* "Raven" (2022-ish) - just a random but cool codename
* MyLang (2023) - my attempt perfecting practices

The "Raven" compiler has the beginning of a parser. 

I believe that expression parsing has been commented out due to not having defined all the nodes yet.

MyLang doesn't yet have a parser.

Another difference is that MyLang (which is newer) has some unit tests for testing the AST.

## The language

I wanted to make a language that is easy to parse. But not relying on curly braces like a C-languages.

Make up as you go.

## Architecture

### Abstract Syntax Tree (AST)

An immutable AST that represents the syntactical structure (syntax) of a program. 

It keeps track of nodes an their relation to each other.

Changes to an existing tree tree result in a new tree.

It wraps the internal tree.

#### Syntax Nodes

Represents the syntactic constructs in a language - statements, expressions, and type declarations. 

May consist of tokens and relationship with other syntax nodes.

For example, pseudo-definition of an "If-statement" syntax node:

```
"if" [Token]
then [ExpressionSyntax]
"else" [Token]
...
```

#### Tokens

Represent identifiers, keywords, and operators.

They are equal by value. And all, except identifiers, are a fixed set within the language.

May have leading or trailing trivia.

#### Trivia

Represent whitespaces and comments that don't affect the meaning of the program.

They are either leading or trailing of tokens.

### Internal Tree

The internal tree consists of nodes can be re-used for efficiency. Never exposed to the outside.

Thus, nodes are passed by reference.

They are devoid of any data signifying relationship to another node. That is the responsibility of the outer tree.

### Semantic model

TBD. There is no semantic analysis yet.

## Design

### Parser

#### Parsing expressions

For parsing expressions we have an [Operator-precedence parser](https://en.wikipedia.org/wiki/Operator-precedence_parser), which is a  [shift-reduce parser](https://en.wikipedia.org/wiki/Shift-reduce_parser). 

The algorithm is explained in the Wikipedia article. (Linked)