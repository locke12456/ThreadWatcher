#include "Node.h"

void _node_insert(Node* n1, Node* n2)
{
    n2->next = n1->next;
    n1->next = n2;
}

void _node_remove(Node* n1)
{
    n1->next = n1->next->next;
}

Node * NewNode( int data )
{
	Node * node = (Node *) malloc ( sizeof(Node) );
	node->next = nullptr;
	node->data = data;
	return node;
}

void ReleaseNode( Node * node )
{
	_node_remove( node );
	free( node );
}