#ifndef _NODE_
#define _NODE_
#ifndef _WATCHAPI_
	#include <malloc.h>
#else
	#include ""
#endif
#include <memory.h>
#include <conio.h>
#include <process.h>
typedef struct Node
{
	Node * next;
	int data;
};

extern void _node_insert(Node* n1, Node* n2);
extern void _node_remove(Node* n1);
extern Node * NewNode ( int data );
#endif