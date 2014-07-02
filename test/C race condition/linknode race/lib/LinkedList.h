#ifndef _LINKEDLIST_
#define _LINKEDLIST_
#include "Node.h"
typedef struct LinkedList
{
	Node * head , * end , * current ;
};
extern LinkedList * LinkedList_GetList( );
extern void LinkedList_SetHead(Node * node);
extern Node * LinkedList_GetHead();
extern void LinkedList_AddHead(Node * node);
extern void LinkedList_RemoveNode(Node * node);
extern void LinkedList_Insert(Node * node , Node * new_node);
extern void LinkedList_AddNode(Node * node);
extern void LinkedList_SetTail( Node * node );
extern Node * LinkedList_GetTail();
extern void InitLinkedPointers();
#endif