#include "philosopher.h"
extern int * lock_log;
void _ate( boost::mutex * mutex_left , boost::mutex * mutex_right )
{
	(*lock_log)++;
	mutex_left->lock();
	mutex_right->lock();
	(*lock_log)++;
}
void _think( boost::mutex * mutex_left , boost::mutex * mutex_right )
{
	(*lock_log)++;
	mutex_left->unlock();
	mutex_right->unlock();
	(*lock_log)++;
}

void philosopher(int id ,  boost::mutex * mutex_left , boost::mutex * mutex_right )
{
	while (true)
	{
		printf ("[%d] pickup .. \n" , id );
		_ate( mutex_left , mutex_right );
		printf ("[%d] eating .. \n" , id );
		int r = rand() % 100;
		boost::this_thread::sleep( boost::posix_time::milliseconds(r) );
		_think( mutex_left , mutex_right );
		printf ("[%d] thinking .. \n" , id );
	}
}