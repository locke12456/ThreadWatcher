#include "writer.h"
void getFileName(char * cfile ){
	const int BUFSIZE = 1024;
	std::string filename(cfile);
	std::wstring stemp = std::wstring(filename.begin(), filename.end());
	LPCWSTR file = stemp.c_str();
	TCHAR** lppPart = {NULL};
	TCHAR   drive[BUFSIZE]	= TEXT("")  ,
		dir[BUFSIZE]	= TEXT("")	, wfilename[BUFSIZE] = TEXT("")  ,  ext[BUFSIZE] = TEXT(""); 
	_wsplitpath(	file,	drive,
		dir,	wfilename,
		ext		);
	stemp = std::wstring(wfilename).append(ext);
	filename = std::string(target_dir);
	filename += (( stemp != L"" ) ? std::string(stemp.begin(),stemp.end()) : "" );
	memcpy( cfile , (void *)filename.c_str() , filename.size());
	cfile [ filename.size() ] = '\0';
}
void _write_file(DataStream * data)
{
	FILE * file;
	getFileName(data->filename);
	if (( file = fopen( data->filename , "w"))!= NULL )
	{
		//fwrite(data->_buf , 1 , data->length , file); 
		while(data->index < data->length)
			fputc( data->_buf[data->index++ ] , file );
	}
	fclose(file);
}
void _writer_worker(void * p)
{
	while (loop_flag_writer)
	{
		DataStream * data = dequque();
		if(data != nullptr)
		{
			_write_file(data);
			free(data);
		}
	}
}