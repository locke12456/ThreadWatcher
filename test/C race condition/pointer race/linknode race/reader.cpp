#include "reader.h"

void _filename( char * dir , char * name , char * buffer)
{
	std::string filename = std::string(dir);
	filename += "\\";
	filename += name;
	memcpy( buffer , (void *)filename.c_str() , filename.size());
	buffer [ filename.size() ] = '\0';
}
DataStream * _read_file(char * filename)
{
	struct stat st;
	char buffer[NAME_SIZE];
	_filename("temp" , filename , buffer );
	FILE* file = fopen( buffer , "r" );
	if( file == NULL)return nullptr;
	stat(buffer, &st);
	DataStream * data = (DataStream *) malloc ( sizeof ( DataStream ));
	memcpy( data->filename , (void *)filename , NAME_SIZE );
	data->length = st.st_size;
	data->index = 0;
	fread(data->_buf, data->length , 1, file);
	fclose(file);
	return data;
}

void _reader_worker(void * p)
{

	while(loop_flag_reader)
	{
		DIR *dir;
		struct dirent *ent;
		if ((dir = opendir ("temp")) != NULL) {
			/* print all the files and directories within directory */
			while ((ent = readdir (dir)) != NULL) {
				DataStream * data = _read_file( ent->d_name );
				if(data != nullptr)
					enqueue(data);

				//printf ("%s\n", ent->d_name);
			}
			closedir (dir);
		}

		break;
	}
}