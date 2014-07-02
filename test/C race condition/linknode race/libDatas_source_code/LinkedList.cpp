#include "LinkedList.h"

LinkedList * list = nullptr;

void LinkedList_SetTail( Node * node )
{
	list->end = node;
}
LinkedList * LinkedList_GetList()
{
	return list;
}
void LinkedList_SetHead(Node * node)
{
	list->head = node;
}
Node * LinkedList_GetHead()
{
	return list->head;
}
void LinkedList_AddHead(Node * node)
{
	_node_insert( list->head , node);
}

void LinkedList_AddNode(Node * node)
{
	_node_insert( list->end , node);
	LinkedList_SetTail( node );
}

void LinkedList_RemoveNode(Node * node)
{
	Node ** next = &list->head;
	if( node == (*next) )
	{
		LinkedList_SetHead( node->next );
		return;
	}
	while( (*next)->next != node && ((*next)->next != nullptr) && (next = &(*next)->next) ); 
	if( (*next)->next == node )
	{
		if( node == list->end )
		{
			LinkedList_SetTail( (*next) );
		}
		(*next)->next = node->next;
	}
}

void LinkedList_Insert(Node * node , Node * new_node)
{
	_node_insert( node , new_node );
}

void InitLinkedPointers()
{
	list = (LinkedList *)( malloc( sizeof(LinkedList) ) );
	memset( list , (int)nullptr , sizeof( LinkedList ) );
}
